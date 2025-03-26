using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace NetworkMonitor
{
    public partial class AddDeviceForm : Form
    {
        public AddDeviceForm()
        {
            InitializeComponent();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            // Получаем данные из полей
            string name = nameTextBox.Text;
            string ip = ipTextBox.Text;
            string description = descriptionTextBox.Text;

            // Проверяем, что поля не пустые
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("Название и IP-адрес обязательны для заполнения.");
                return;
            }

            // Валидация IP-адреса
            if (!ValidateIPAddress(ip))
            {
                MessageBox.Show("Некорректный IP-адрес.");
                return;
            }

            // Сохраняем устройство в базу данных
            SaveDeviceToDatabase(name, ip, description);

            // Закрываем форму
            this.Close();
        }

        private void SaveDeviceToDatabase(string name, string ip, string description)
        {
            string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Начинаем транзакцию
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // SQL-запрос для добавления устройства
                            string insertDeviceQuery = "INSERT INTO devices (name, ip_address, description) VALUES (@name, @ip, @description)";
                            using (MySqlCommand insertDeviceCommand = new MySqlCommand(insertDeviceQuery, connection, transaction))
                            {
                                insertDeviceCommand.Parameters.AddWithValue("@name", name);
                                insertDeviceCommand.Parameters.AddWithValue("@ip", ip);
                                insertDeviceCommand.Parameters.AddWithValue("@description", description);
                                insertDeviceCommand.ExecuteNonQuery();
                            }

                            // Получаем ID добавленного устройства
                            string lastInsertIdQuery = "SELECT LAST_INSERT_ID()";
                            using (MySqlCommand lastInsertIdCommand = new MySqlCommand(lastInsertIdQuery, connection, transaction))
                            {
                                int deviceId = Convert.ToInt32(lastInsertIdCommand.ExecuteScalar());

                                // SQL-запрос для добавления статуса
                                string insertStatusQuery = "INSERT INTO status (device_id, status, last_checked) VALUES (@deviceId, 'offline', NOW())";
                                using (MySqlCommand insertStatusCommand = new MySqlCommand(insertStatusQuery, connection, transaction))
                                {
                                    insertStatusCommand.Parameters.AddWithValue("@deviceId", deviceId);
                                    insertStatusCommand.ExecuteNonQuery();
                                }
                            }

                            // Фиксируем транзакцию
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Откатываем транзакцию в случае ошибки
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при сохранении устройства: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                }
            }
        }
        private bool ValidateIPAddress(string ip)
        {
            // Регулярное выражение для проверки IPv4
            string pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
            return Regex.IsMatch(ip, pattern);
        }
    }
}


