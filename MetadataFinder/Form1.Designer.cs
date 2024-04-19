namespace MetadataFinder
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            OpenFileButton = new Button();
            ExportTXTButton = new Button();
            ExportWordButton = new Button();
            OutputBox = new TextBox();
            folderBrowserDialog = new FolderBrowserDialog();
            FocalSelector = new ComboBox();
            ReadButton = new Button();
            saveFileDialog = new SaveFileDialog();
            ColumNumPickup = new NumericUpDown();
            label1 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            FocalNumericSelector = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)ColumNumPickup).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FocalNumericSelector).BeginInit();
            SuspendLayout();
            // 
            // OpenFileButton
            // 
            OpenFileButton.Location = new Point(15, 15);
            OpenFileButton.Margin = new Padding(4);
            OpenFileButton.Name = "OpenFileButton";
            OpenFileButton.Size = new Size(437, 29);
            OpenFileButton.TabIndex = 0;
            OpenFileButton.Text = "開啟資料夾...";
            OpenFileButton.UseVisualStyleBackColor = true;
            OpenFileButton.Click += OpenFileButton_Click;
            // 
            // ExportTXTButton
            // 
            ExportTXTButton.Enabled = false;
            ExportTXTButton.Location = new Point(7, 23);
            ExportTXTButton.Margin = new Padding(4);
            ExportTXTButton.Name = "ExportTXTButton";
            ExportTXTButton.Size = new Size(99, 29);
            ExportTXTButton.TabIndex = 1;
            ExportTXTButton.Text = "轉出文字檔";
            ExportTXTButton.UseVisualStyleBackColor = true;
            ExportTXTButton.Click += ExportTXTButton_Click;
            // 
            // ExportWordButton
            // 
            ExportWordButton.Enabled = false;
            ExportWordButton.Location = new Point(7, 24);
            ExportWordButton.Margin = new Padding(4);
            ExportWordButton.Name = "ExportWordButton";
            ExportWordButton.Size = new Size(135, 29);
            ExportWordButton.TabIndex = 2;
            ExportWordButton.Text = "轉出Word檔表格";
            ExportWordButton.UseVisualStyleBackColor = true;
            ExportWordButton.Click += ExportWordButton_Click;
            // 
            // OutputBox
            // 
            OutputBox.AcceptsReturn = true;
            OutputBox.BorderStyle = BorderStyle.FixedSingle;
            OutputBox.Location = new Point(15, 52);
            OutputBox.Margin = new Padding(4);
            OutputBox.Multiline = true;
            OutputBox.Name = "OutputBox";
            OutputBox.ReadOnly = true;
            OutputBox.ScrollBars = ScrollBars.Both;
            OutputBox.Size = new Size(608, 466);
            OutputBox.TabIndex = 3;
            // 
            // FocalSelector
            // 
            FocalSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            FocalSelector.FlatStyle = FlatStyle.System;
            FocalSelector.FormattingEnabled = true;
            FocalSelector.Items.AddRange(new object[] { "全片幅 (不使用等校焦段)", "Nikon APS-C 片幅 (1.5倍)", "Nikon CX 片幅 (1.7倍)", "Canon APS-H 片幅 (1.3倍)", "Canon APS-C 片幅 (1.6倍)", "Sony APS-C 片幅 (1.5倍)" });
            FocalSelector.Location = new Point(118, 25);
            FocalSelector.Margin = new Padding(4);
            FocalSelector.Name = "FocalSelector";
            FocalSelector.Size = new Size(206, 27);
            FocalSelector.TabIndex = 5;
            // 
            // ReadButton
            // 
            ReadButton.Enabled = false;
            ReadButton.Location = new Point(460, 15);
            ReadButton.Margin = new Padding(4);
            ReadButton.Name = "ReadButton";
            ReadButton.Size = new Size(163, 29);
            ReadButton.TabIndex = 6;
            ReadButton.Text = "讀取";
            ReadButton.UseVisualStyleBackColor = true;
            ReadButton.Click += RefreshButton_Click;
            // 
            // ColumNumPickup
            // 
            ColumNumPickup.Location = new Point(52, 67);
            ColumNumPickup.Margin = new Padding(4);
            ColumNumPickup.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            ColumNumPickup.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ColumNumPickup.Name = "ColumNumPickup";
            ColumNumPickup.Size = new Size(90, 27);
            ColumNumPickup.TabIndex = 7;
            ColumNumPickup.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 72);
            label1.Name = "label1";
            label1.Size = new Size(39, 19);
            label1.TabIndex = 8;
            label1.Text = "欄數";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ExportTXTButton);
            groupBox1.Location = new Point(352, 527);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(113, 104);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "文字檔輸出";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ExportWordButton);
            groupBox2.Controls.Add(ColumNumPickup);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(471, 525);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(152, 106);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Word檔輸出";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(radioButton2);
            groupBox3.Controls.Add(radioButton1);
            groupBox3.Controls.Add(FocalNumericSelector);
            groupBox3.Controls.Add(FocalSelector);
            groupBox3.Location = new Point(15, 527);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(331, 104);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "焦段倍率設定";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(6, 65);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(90, 23);
            radioButton2.TabIndex = 8;
            radioButton2.Text = "手動輸入";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(6, 28);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(105, 23);
            radioButton1.TabIndex = 7;
            radioButton1.TabStop = true;
            radioButton1.Text = "使用預設值";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // FocalNumericSelector
            // 
            FocalNumericSelector.DecimalPlaces = 1;
            FocalNumericSelector.Enabled = false;
            FocalNumericSelector.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            FocalNumericSelector.Location = new Point(119, 65);
            FocalNumericSelector.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            FocalNumericSelector.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            FocalNumericSelector.Name = "FocalNumericSelector";
            FocalNumericSelector.Size = new Size(205, 27);
            FocalNumericSelector.TabIndex = 6;
            FocalNumericSelector.Value = new decimal(new int[] { 11, 0, 0, 65536 });
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(639, 648);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(ReadButton);
            Controls.Add(OutputBox);
            Controls.Add(OpenFileButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MainWindow";
            Text = "MetadataGenerator";
            Load += MainWindow_Load;
            ((System.ComponentModel.ISupportInitialize)ColumNumPickup).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FocalNumericSelector).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OpenFileButton;
        private Button ExportTXTButton;
        private Button ExportWordButton;
        private TextBox OutputBox;
        private FolderBrowserDialog folderBrowserDialog;
        private ComboBox FocalSelector;
        private Button ReadButton;
        private SaveFileDialog saveFileDialog;
        private NumericUpDown ColumNumPickup;
        private Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RadioButton radioButton1;
        private NumericUpDown FocalNumericSelector;
        private RadioButton radioButton2;
    }
}
