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
            RefreshButton = new Button();
            saveFileDialog = new SaveFileDialog();
            ColumNumPickup = new NumericUpDown();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)ColumNumPickup).BeginInit();
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
            ExportTXTButton.Location = new Point(289, 526);
            ExportTXTButton.Margin = new Padding(4);
            ExportTXTButton.Name = "ExportTXTButton";
            ExportTXTButton.Size = new Size(334, 29);
            ExportTXTButton.TabIndex = 1;
            ExportTXTButton.Text = "轉出文字檔";
            ExportTXTButton.UseVisualStyleBackColor = true;
            ExportTXTButton.Click += ExportTXTButton_Click;
            // 
            // ExportWordButton
            // 
            ExportWordButton.Enabled = false;
            ExportWordButton.Location = new Point(289, 563);
            ExportWordButton.Margin = new Padding(4);
            ExportWordButton.Name = "ExportWordButton";
            ExportWordButton.Size = new Size(163, 29);
            ExportWordButton.TabIndex = 2;
            ExportWordButton.Text = "轉出Word檔";
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
            FocalSelector.Location = new Point(13, 528);
            FocalSelector.Margin = new Padding(4);
            FocalSelector.Name = "FocalSelector";
            FocalSelector.Size = new Size(268, 27);
            FocalSelector.TabIndex = 5;
            // 
            // RefreshButton
            // 
            RefreshButton.Enabled = false;
            RefreshButton.Location = new Point(460, 15);
            RefreshButton.Margin = new Padding(4);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(163, 29);
            RefreshButton.TabIndex = 6;
            RefreshButton.Text = "重新讀取";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // ColumNumPickup
            // 
            ColumNumPickup.Enabled = false;
            ColumNumPickup.Location = new Point(505, 566);
            ColumNumPickup.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            ColumNumPickup.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ColumNumPickup.Name = "ColumNumPickup";
            ColumNumPickup.Size = new Size(118, 27);
            ColumNumPickup.TabIndex = 7;
            ColumNumPickup.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(460, 568);
            label1.Name = "label1";
            label1.Size = new Size(39, 19);
            label1.TabIndex = 8;
            label1.Text = "欄數";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(639, 602);
            Controls.Add(label1);
            Controls.Add(ColumNumPickup);
            Controls.Add(RefreshButton);
            Controls.Add(FocalSelector);
            Controls.Add(OutputBox);
            Controls.Add(ExportWordButton);
            Controls.Add(ExportTXTButton);
            Controls.Add(OpenFileButton);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MainWindow";
            Text = "Form1";
            Load += MainWindow_Load;
            ((System.ComponentModel.ISupportInitialize)ColumNumPickup).EndInit();
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
        private Button RefreshButton;
        private SaveFileDialog saveFileDialog;
        private NumericUpDown ColumNumPickup;
        private Label label1;
    }
}
