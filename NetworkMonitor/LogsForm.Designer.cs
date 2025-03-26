using System.Windows.Forms;

namespace NetworkMonitor
{
    partial class LogsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.deviceComboBox = new System.Windows.Forms.ComboBox();
            this.logLevelComboBox = new System.Windows.Forms.ComboBox();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.logsGrid = new System.Windows.Forms.DataGridView();
            this.exportButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.applyFiltersButton = new System.Windows.Forms.Button();
            this.intervalTextBox = new System.Windows.Forms.TextBox();
            this.applyIntervalButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.created_at = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.log_level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.logsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // deviceComboBox
            // 
            this.deviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceComboBox.FormattingEnabled = true;
            this.deviceComboBox.Location = new System.Drawing.Point(14, 12);
            this.deviceComboBox.Name = "deviceComboBox";
            this.deviceComboBox.Size = new System.Drawing.Size(154, 21);
            this.deviceComboBox.TabIndex = 0;
            // 
            // logLevelComboBox
            // 
            this.logLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logLevelComboBox.FormattingEnabled = true;
            this.logLevelComboBox.Items.AddRange(new object[] {
            "Все",
            "info",
            "warning",
            "error"});
            this.logLevelComboBox.Location = new System.Drawing.Point(174, 12);
            this.logLevelComboBox.Name = "logLevelComboBox";
            this.logLevelComboBox.Size = new System.Drawing.Size(162, 21);
            this.logLevelComboBox.TabIndex = 1;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDatePicker.Location = new System.Drawing.Point(729, 13);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 20);
            this.startDatePicker.TabIndex = 2;
            // 
            // endDatePicker
            // 
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDatePicker.Location = new System.Drawing.Point(935, 13);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(200, 20);
            this.endDatePicker.TabIndex = 3;
            // 
            // logsGrid
            // 
            this.logsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.created_at,
            this.device_name,
            this.log_level,
            this.message});
            this.logsGrid.Location = new System.Drawing.Point(14, 39);
            this.logsGrid.Name = "logsGrid";
            this.logsGrid.Size = new System.Drawing.Size(1121, 336);
            this.logsGrid.TabIndex = 4;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(132, 415);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(104, 23);
            this.exportButton.TabIndex = 5;
            this.exportButton.Text = "Экспорт в CSV";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(242, 415);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(108, 23);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "Очистить логи";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // applyFiltersButton
            // 
            this.applyFiltersButton.Location = new System.Drawing.Point(12, 415);
            this.applyFiltersButton.Name = "applyFiltersButton";
            this.applyFiltersButton.Size = new System.Drawing.Size(114, 23);
            this.applyFiltersButton.TabIndex = 7;
            this.applyFiltersButton.Text = "Применить";
            this.applyFiltersButton.UseVisualStyleBackColor = true;
            this.applyFiltersButton.Click += new System.EventHandler(this.applyFiltersButton_Click);
            // 
            // intervalTextBox
            // 
            this.intervalTextBox.Location = new System.Drawing.Point(1035, 417);
            this.intervalTextBox.MaxLength = 6;
            this.intervalTextBox.Name = "intervalTextBox";
            this.intervalTextBox.Size = new System.Drawing.Size(100, 20);
            this.intervalTextBox.TabIndex = 8;
            this.intervalTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.intervalTextBox_KeyPress);
            // 
            // applyIntervalButton
            // 
            this.applyIntervalButton.Location = new System.Drawing.Point(895, 414);
            this.applyIntervalButton.Name = "applyIntervalButton";
            this.applyIntervalButton.Size = new System.Drawing.Size(134, 23);
            this.applyIntervalButton.TabIndex = 9;
            this.applyIntervalButton.Text = "Применить интервал";
            this.applyIntervalButton.UseVisualStyleBackColor = true;
            this.applyIntervalButton.Click += new System.EventHandler(this.applyIntervalButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1030, 401);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Интервал лога (мс)";
            // 
            // created_at
            // 
            this.created_at.HeaderText = "Время";
            this.created_at.Name = "created_at";
            this.created_at.Width = 200;
            // 
            // device_name
            // 
            this.device_name.HeaderText = "Устройство";
            this.device_name.Name = "device_name";
            this.device_name.Width = 130;
            // 
            // log_level
            // 
            this.log_level.HeaderText = "Уровень лога";
            this.log_level.Name = "log_level";
            this.log_level.Width = 130;
            // 
            // message
            // 
            this.message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.message.HeaderText = "Сообщение";
            this.message.Name = "message";
            // 
            // LogsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.applyIntervalButton);
            this.Controls.Add(this.intervalTextBox);
            this.Controls.Add(this.applyFiltersButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.logsGrid);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.logLevelComboBox);
            this.Controls.Add(this.deviceComboBox);
            this.Name = "LogsForm";
            this.Text = "LogsForm";
            ((System.ComponentModel.ISupportInitialize)(this.logsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox deviceComboBox;
        private System.Windows.Forms.ComboBox logLevelComboBox;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.DataGridView logsGrid;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button clearButton;
        private Button applyFiltersButton;
        private TextBox intervalTextBox;
        private Button applyIntervalButton;
        private Label label1;
        private DataGridViewTextBoxColumn created_at;
        private DataGridViewTextBoxColumn device_name;
        private DataGridViewTextBoxColumn log_level;
        private DataGridViewTextBoxColumn message;
    }
}