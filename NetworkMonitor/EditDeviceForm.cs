using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NetworkMonitor
{
    public partial class EditDeviceForm : Form
    {
        public int deviceId { get; private set; }

        public EditDeviceForm(int deviceId, string name, string ip, string description)
        {
            InitializeComponent();
            this.deviceId = deviceId;
            nameTextBox.Text = name;
            ipTextBox.Text = ip;
            descriptionTextBox.Text = description;

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Получаем данные из полей
            string name = nameTextBox.Text;
            string ip = ipTextBox.Text;
            string description = descriptionTextBox.Text;

            // Валидация IP-адреса
            if (!ValidateIPAddress(ip))
            {
                MessageBox.Show("Некорректный IP-адрес.");
                return;
            }

            // Обновляем устройство в базе данных
            UpdateDeviceInDatabase(deviceId, name, ip, description);

            // Закрываем форму
            this.Close();
        }

        private void UpdateDeviceInDatabase(int deviceId, string name, string ip, string description)
        {
            string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-запрос для обновления устройства
                    string updateQuery = "UPDATE devices SET name = @name, ip_address = @ip, description = @description WHERE id = @deviceId";
                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@name", name);
                        updateCommand.Parameters.AddWithValue("@ip", ip);
                        updateCommand.Parameters.AddWithValue("@description", description);
                        updateCommand.Parameters.AddWithValue("@deviceId", deviceId);
                        updateCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Устройство успешно обновлено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении устройства: {ex.Message}");
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
