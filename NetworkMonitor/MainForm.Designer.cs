namespace NetworkMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.devicesGrid = new System.Windows.Forms.DataGridView();
            this.addDeviceButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.viewLogsButton = new System.Windows.Forms.Button();
            this.notificationsLabel = new System.Windows.Forms.Label();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.devicesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // devicesGrid
            // 
            this.devicesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.devicesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.devicesGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.devicesGrid.Location = new System.Drawing.Point(0, 0);
            this.devicesGrid.Name = "devicesGrid";
            this.devicesGrid.Size = new System.Drawing.Size(784, 422);
            this.devicesGrid.TabIndex = 0;
            // 
            // addDeviceButton
            // 
            this.addDeviceButton.Location = new System.Drawing.Point(12, 455);
            this.addDeviceButton.Name = "addDeviceButton";
            this.addDeviceButton.Size = new System.Drawing.Size(100, 45);
            this.addDeviceButton.TabIndex = 1;
            this.addDeviceButton.Text = "Добавить устройство";
            this.addDeviceButton.UseVisualStyleBackColor = true;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(566, 455);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(100, 45);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Обновить статус";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click_1);
            // 
            // viewLogsButton
            // 
            this.viewLogsButton.Location = new System.Drawing.Point(672, 455);
            this.viewLogsButton.Name = "viewLogsButton";
            this.viewLogsButton.Size = new System.Drawing.Size(100, 45);
            this.viewLogsButton.TabIndex = 3;
            this.viewLogsButton.Text = "Просмотреть логи";
            this.viewLogsButton.UseVisualStyleBackColor = true;
            // 
            // notificationsLabel
            // 
            this.notificationsLabel.AutoSize = true;
            this.notificationsLabel.Location = new System.Drawing.Point(296, 520);
            this.notificationsLabel.Name = "notificationsLabel";
            this.notificationsLabel.Size = new System.Drawing.Size(206, 13);
            this.notificationsLabel.TabIndex = 4;
            this.notificationsLabel.Text = "Уведомления: Нет новых уведомлений";
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(118, 455);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(100, 45);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Редактировать";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(224, 455);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 45);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.notificationsLabel);
            this.Controls.Add(this.viewLogsButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addDeviceButton);
            this.Controls.Add(this.devicesGrid);
            this.Name = "MainForm";
            this.Text = "Мониторинг сетевого оборудования";
            ((System.ComponentModel.ISupportInitialize)(this.devicesGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView devicesGrid;
        private System.Windows.Forms.Button addDeviceButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button viewLogsButton;
        private System.Windows.Forms.Label notificationsLabel;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
    }
}

