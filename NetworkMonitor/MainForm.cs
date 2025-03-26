using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;
using System.Timers;
using System.Diagnostics;

namespace NetworkMonitor
{

    public partial class MainForm : Form
    {

        private System.Timers.Timer statusCheckTimer;

        public MainForm()
        {
            
            InitializeComponent();

            statusCheckTimer = new System.Timers.Timer(60000); // Проверка каждую минуту 60000
           statusCheckTimer.Elapsed += StatusCheckTimer_Elapsed;
            statusCheckTimer.AutoReset = true;
           statusCheckTimer.Enabled = true;


            List<string> offlineDevices = UpdateDeviceStatuses();
            if (offlineDevices.Count > 0)
            {
                string offlineDevicesMessage = "Неработающие устройства:\n" + string.Join("\n", offlineDevices);
                MessageBox.Show(offlineDevicesMessage, "Уведомление о сбоях", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Обновление Label
            UpdateStatusLabel(offlineDevices);
        
            this.Text = "Мониторинг сетевого оборудования";
            this.Size = new Size(800, 600);

            devicesGrid.Columns.Add("id", "ID"); 
            devicesGrid.Columns.Add("name", "Название");
            devicesGrid.Columns.Add("description", "Описание");
            devicesGrid.Columns.Add("ip_address", "IP-адрес"); 
            devicesGrid.Columns.Add("status", "Статус");
            devicesGrid.Columns.Add("created_at", "Дата добавления"); 

            // Подключение обработчиков событий
            addDeviceButton.Click += AddDeviceButton_Click;
            viewLogsButton.Click += ViewLogsButton_Click;

            // Заполнение тестовыми данными
            LoadDevices();
        }
        public void ChangeLogFrequency(int interval)
        {
            statusCheckTimer.Interval = interval;
            statusCheckTimer.Stop();
            statusCheckTimer.Start();
        }
        private void StatusCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateDeviceStatuses();
        }
        private void LoadDevices()
        {
            LoadDevicesFromDatabase(); // Загружаем устройства из базы данных

            // Цветовая индикация статуса
            foreach (DataGridViewRow row in devicesGrid.Rows)
            {
                if (row.Cells["Status"].Value?.ToString() == "online")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }
        private void UpdateStatusLabel(List<string> offlineDevices)
        {
            if (offlineDevices.Count > 0)
            {
                notificationsLabel.Text = "Неработающие устройства: " + string.Join(", ", offlineDevices);
                notificationsLabel.ForeColor = Color.Red; // Красный цвет для привлечения внимания
            }
            else
            {
                notificationsLabel.Text = "Все устройства работают";
                notificationsLabel.ForeColor = Color.Green; // Зеленый цвет для положительного статуса
            }
        }


        private void ViewLogsButton_Click(object sender, EventArgs e)
        {
            LogsForm logsForm = new LogsForm();

            // Открываем форму логов
            logsForm.ShowDialog();
        }
        private void LoadDevicesFromDatabase()
        {
            string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-запрос для получения устройств и их статусов
                    string query = @"
                SELECT devices.id, devices.name,devices.description, devices.ip_address, status.status, devices.created_at
                FROM devices
                JOIN status ON devices.id = status.device_id";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            devicesGrid.Rows.Clear();

                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                string name = reader["name"].ToString();
                                string description = reader["description"].ToString();
                                string ip = reader["ip_address"].ToString();
                                string status = reader["status"].ToString();
                                string createdAt = reader["created_at"].ToString();

                                // Добавляем строку в DataGridView
                                int rowIndex = devicesGrid.Rows.Add(id, name,description , ip, status, createdAt);

                                // Устанавливаем цвет строки в зависимости от статуса
                                if (status == "online")
                                {
                                    devicesGrid.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                                }
                                else
                                {
                                    devicesGrid.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                                }
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
        private void AddDeviceButton_Click(object sender, EventArgs e)
        {
            AddDeviceForm addDeviceForm = new AddDeviceForm();
            addDeviceForm.ShowDialog(); // Открываем форму добавления устройства
            LoadDevices(); // Обновляем список устройств после закрытия формы
        }
       
        private List<string> UpdateDeviceStatuses()
        {
            string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
            List<string> offlineDevices = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT id, ip_address, name FROM devices";
                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int deviceId = reader.GetInt32("id");
                                string ip = reader.GetString("ip_address");
                                string name = reader.GetString("name");

                                LogPingResults(deviceId, ip);
                                string pingStats = GetFullPingStatistics(ip);
                                string status = pingStats.Contains("потеряно = 0") ? "online" : "offline";
                                UpdateDeviceStatus(deviceId, status);

                                if (status == "offline")
                                {
                                    offlineDevices.Add(name);
                                }

                                string logLevel = status == "online" ? "info" : "error";
                                LogMessage(deviceId, logLevel, pingStats);
                                
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении статусов: {ex.Message}");
                }
            }

            return offlineDevices;
        }

       
        private string GetFullPingStatistics(string ip)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("ping", $"-n 4 -w 1000 {ip}")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.GetEncoding(866),
                    StandardErrorEncoding = Encoding.GetEncoding(866)
                };

                using (Process process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit(4000);

                    // Обрабатываем все возможные варианты вывода
                    if (!string.IsNullOrEmpty(error))
                    {
                        return $"Ping {ip}: {error.Trim()}";
                    }

                    // Ищем стандартный формат статистики
                    string[] lines = output.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].StartsWith("Статистика Ping для"))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(lines[i].Trim());

                            // Следующая строка после заголовка
                            if (i + 1 < lines.Length)
                            {
                                sb.AppendLine(lines[i + 1].Trim());
                            }

                            // Строка с процентами
                            if (i + 2 < lines.Length && lines[i + 2].Contains("% потерь"))
                            {
                                sb.AppendLine(lines[i + 2].Trim());
                            }

                            return sb.ToString();
                        }
                    }

                    // Если не нашли стандартный формат, возвращаем весь вывод
                    return output.Trim();
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка выполнения ping: {ex.Message}";
            }
        }

        private void LogPingResults(int deviceId, string ip)
        {
            string pingStats = GetFullPingStatistics(ip);
            string status = pingStats.Contains("потеряно = 0") ? "online" : "offline";
            string logLevel = status == "online" ? "info" : "error";

            // Обновляем статус в базе
            UpdateDeviceStatus(deviceId, status);

            // Записываем полную статистику в лог
            LogMessage(deviceId, logLevel, pingStats);
        }
        private void LogMessage(int deviceId, string logLevel, string message)
        {
            string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-запрос для добавления лога
                    string insertQuery = "INSERT INTO logs (device_id, log_level, message) VALUES (@deviceId, @logLevel, @message)";
                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@deviceId", deviceId);
                        insertCommand.Parameters.AddWithValue("@logLevel", logLevel);
                        insertCommand.Parameters.AddWithValue("@message", message);
                        insertCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при записи лога: {ex.Message}");
                }
            }
        }

        private void UpdateDeviceStatus(int deviceId, string status)
        {
            string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-запрос для обновления статуса
                    string updateQuery = "UPDATE status SET status = @status, last_checked = NOW() WHERE device_id = @deviceId";
                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@status", status);
                        updateCommand.Parameters.AddWithValue("@deviceId", deviceId);
                        updateCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении статуса: {ex.Message}");
                }
            }
        }

        private void refreshButton_Click_1(object sender, EventArgs e)
        {
            UpdateDeviceStatuses();
            LoadDevicesFromDatabase();
            List<string> offlineDevices = UpdateDeviceStatuses();
            UpdateStatusLabel(offlineDevices);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (devicesGrid.SelectedRows.Count > 0)
            {
                // Получаем выбранное устройство
                var selectedRow = devicesGrid.SelectedRows[0];
                int deviceId = Convert.ToInt32(selectedRow.Cells["id"].Value); 
                string name = selectedRow.Cells["name"].Value.ToString();
                string ip = selectedRow.Cells["ip_address"].Value.ToString(); 
                string description = selectedRow.Cells["description"].Value.ToString();

                // Открываем форму редактирования
                EditDeviceForm editDeviceForm = new EditDeviceForm(deviceId, name, ip, description);
                editDeviceForm.ShowDialog();

                // Обновляем данные после закрытия формы
                LoadDevicesFromDatabase();
            }
            else
            {
                MessageBox.Show("Выберите устройство для редактирования.");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (devicesGrid.SelectedRows.Count > 0)
            {
                // Получаем выбранное устройство
                var selectedRow = devicesGrid.SelectedRows[0];
                int deviceId = Convert.ToInt32(selectedRow.Cells["id"].Value);

                // Подтверждение удаления
                var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить это устройство?", "Подтверждение", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteDeviceFromDatabase(deviceId);
                    LoadDevicesFromDatabase(); // Обновляем данные в DataGridView
                }
            }
            else
            {
                MessageBox.Show("Выберите устройство для удаления.");
            }
        }
        private void DeleteDeviceFromDatabase(int deviceId)
        {
            string connectionString = "server=localhost;user=root;database=hard_mon;port=3306;password=1234";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Удаляем статус устройства
                    string deleteStatusQuery = "DELETE FROM status WHERE device_id = @deviceId";
                    using (MySqlCommand deleteStatusCommand = new MySqlCommand(deleteStatusQuery, connection))
                    {
                        deleteStatusCommand.Parameters.AddWithValue("@deviceId", deviceId);
                        deleteStatusCommand.ExecuteNonQuery();
                    }

                    // Удаляем устройство
                    string deleteDeviceQuery = "DELETE FROM devices WHERE id = @deviceId";
                    using (MySqlCommand deleteDeviceCommand = new MySqlCommand(deleteDeviceQuery, connection))
                    {
                        deleteDeviceCommand.Parameters.AddWithValue("@deviceId", deviceId);
                        deleteDeviceCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Устройство успешно удалено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении устройства: {ex.Message}");
                }
            }
        }
    }
}