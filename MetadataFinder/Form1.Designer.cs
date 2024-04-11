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
            statusStrip = new StatusStrip();
            StatusLabel = new ToolStripStatusLabel();
            folderBrowserDialog = new FolderBrowserDialog();
            FocalSelector = new ComboBox();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // OpenFileButton
            // 
            OpenFileButton.Location = new Point(12, 12);
            OpenFileButton.Name = "OpenFileButton";
            OpenFileButton.Size = new Size(118, 23);
            OpenFileButton.TabIndex = 0;
            OpenFileButton.Text = "開啟資料夾...";
            OpenFileButton.UseVisualStyleBackColor = true;
            OpenFileButton.Click += OpenFileButton_Click;
            // 
            // ExportTXTButton
            // 
            ExportTXTButton.Location = new Point(225, 415);
            ExportTXTButton.Name = "ExportTXTButton";
            ExportTXTButton.Size = new Size(127, 23);
            ExportTXTButton.TabIndex = 1;
            ExportTXTButton.Text = "轉出文字檔";
            ExportTXTButton.UseVisualStyleBackColor = true;
            // 
            // ExportWordButton
            // 
            ExportWordButton.Location = new Point(358, 415);
            ExportWordButton.Name = "ExportWordButton";
            ExportWordButton.Size = new Size(127, 23);
            ExportWordButton.TabIndex = 2;
            ExportWordButton.Text = "轉出Word檔";
            ExportWordButton.UseVisualStyleBackColor = true;
            // 
            // OutputBox
            // 
            OutputBox.BorderStyle = BorderStyle.FixedSingle;
            OutputBox.Location = new Point(12, 41);
            OutputBox.Multiline = true;
            OutputBox.Name = "OutputBox";
            OutputBox.ReadOnly = true;
            OutputBox.Size = new Size(473, 368);
            OutputBox.TabIndex = 3;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { StatusLabel });
            statusStrip.Location = new Point(0, 446);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(497, 22);
            statusStrip.TabIndex = 4;
            statusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(115, 17);
            StatusLabel.Text = "請先開啟圖檔資料夾";
            // 
            // FocalSelector
            // 
            FocalSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            FocalSelector.FlatStyle = FlatStyle.System;
            FocalSelector.FormattingEnabled = true;
            FocalSelector.Items.AddRange(new object[] { "全片幅 (不使用等校焦段)", "Nikon APS-C 片幅 (1.5倍)", "Nikon CX 片幅 (1.7倍)", "Canon APS-H 片幅 (1.3倍)", "Canon APS-C 片幅 (1.6倍)", "Sony APS-C 片幅 (1.5倍)" });
            FocalSelector.Location = new Point(12, 415);
            FocalSelector.Name = "FocalSelector";
            FocalSelector.Size = new Size(207, 23);
            FocalSelector.TabIndex = 5;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 468);
            Controls.Add(FocalSelector);
            Controls.Add(statusStrip);
            Controls.Add(OutputBox);
            Controls.Add(ExportWordButton);
            Controls.Add(ExportTXTButton);
            Controls.Add(OpenFileButton);
            MaximizeBox = false;
            Name = "MainWindow";
            Text = "Form1";
            Load += MainWindow_Load;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OpenFileButton;
        private Button ExportTXTButton;
        private Button ExportWordButton;
        private TextBox OutputBox;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel StatusLabel;
        private FolderBrowserDialog folderBrowserDialog;
        private ComboBox FocalSelector;
    }
}
