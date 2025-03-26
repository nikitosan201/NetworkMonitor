using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NetworkMonitor
{
    public partial class LogsForm : Form
    {
        

        public LogsForm()
        {
            InitializeComponent();

            

            // Загрузка устройств в выпадающий список
            LoadDevices();
            // Загрузка логов
            LoadLogs();
        }

        private void LoadDevices()
        {
            string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-запрос для получения устройств
                    string query = "SELECT id, name FROM devices";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader()) 
                        {
                            deviceComboBox.Items.Add("Все устройства");
                            while (reader.Read()) 
                            {
                                deviceComboBox.Items.Add(new
                                {
                                    Id = reader.GetInt32("id"),
                                    Name = reader["name"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке устройств: {ex.Message}");
                }
            }
        }

        private void LoadLogs()
        {
            string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-запрос для получения логов
                    string query = @"
                    SELECT logs.created_at, devices.name AS device_name, logs.log_level, logs.message
                    FROM logs
                    JOIN devices ON logs.device_id = devices.id
                    WHERE (@deviceId IS NULL OR devices.id = @deviceId)
                    AND (@logLevel IS NULL OR logs.log_level = @logLevel)
                    AND (logs.created_at BETWEEN @startDate AND @endDate)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Параметры фильтров
                        int? deviceId = deviceComboBox.SelectedIndex > 0 ? (int?)deviceComboBox.SelectedValue : null;
                        string logLevel = logLevelComboBox.SelectedIndex > 0 ? logLevelComboBox.SelectedItem.ToString() : null;
                        DateTime startDate = startDatePicker.Value.Date;
                        DateTime endDate = endDatePicker.Value.Date.AddDays(1).AddSeconds(-1);

                        command.Parameters.AddWithValue("@deviceId", deviceId);
                        command.Parameters.AddWithValue("@logLevel", logLevel);
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);

                        using (MySqlDataReader reader = command.ExecuteReader()) 
                        {
                            logsGrid.Rows.Clear();

                            while (reader.Read())
                            {
                                string createdAt = reader["created_at"].ToString();
                                string deviceName = reader["device_name"].ToString();
                                string logLevelValue = reader["log_level"].ToString();
                                string message = reader["message"].ToString();

                                logsGrid.Rows.Add(createdAt, deviceName, logLevelValue, message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке логов: {ex.Message}");
                }
            }
        }

        private void applyFiltersButton_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // Заголовки столбцов
                        writer.WriteLine("Время;Устройство;Уровень лога;Сообщение");

                        // Данные
                        foreach (DataGridViewRow row in logsGrid.Rows)
                        {
                            writer.WriteLine($"{row.Cells["created_at"].Value};{row.Cells["device_name"].Value};{row.Cells["log_level"].Value};{row.Cells["message"].Value}");
                        }
                    }
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Вы уверены, что хотите очистить все логи?", "Подтверждение", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // SQL-запрос для очистки логов
                        string query = "DELETE FROM logs";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Обновляем таблицу логов
                        LoadLogs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при очистке логов: {ex.Message}");
                    }
                }
            }
        }

        private void applyIntervalButton_Click(object sender, EventArgs e)
        {
            // Проверяем, что введено число
            if (int.TryParse(intervalTextBox.Text, out int interval))
            {
                // Проверяем, что интервал не меньше минимального значения (например, 1000 мс)
                if (interval >= 1000)
                {
                    // Получаем экземпляр MainForm и вызываем метод изменения интервала
                    MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                    if (mainForm != null)
                    {
                        mainForm.ChangeLogFrequency(interval);
                        MessageBox.Show("Интервал обновления логов изменен.");
                    }
                }
                else
                {
                    MessageBox.Show("Интервал должен быть не менее 1000 мс (1 секунда).");
                }
            }
            else
            {
                MessageBox.Show("Введите корректное число.");
            }
        }

        private void intervalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод
            }
        }
    }
}