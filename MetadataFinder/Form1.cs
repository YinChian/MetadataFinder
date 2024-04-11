using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2021.MipLabelMetaData;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MetadataFinder
{
    public partial class MainWindow : Form
    {
        private string _imageLoadingDir = "";
        private bool _isTouched = false;

        private void ImageDataFetchProc()
        {
            Invoke(new MethodInvoker(delegate() { OutputBox.Text = @"Test";}));
            //OutputBox.Clear();

            var imageFiles = Directory.GetFiles(_imageLoadingDir, "*.jpg");

            foreach (var imagePath in imageFiles)
            {
                OutputBox.Text += imagePath;
                OutputBox.Text += Environment.NewLine;

                try
                {
                    var image = Image.FromFile(imagePath);
                    //statusStrip.Text = @"處理中";
                    foreach (var propertyItem in image.PropertyItems)
                    {
                        switch (propertyItem.Id)
                        {
                            // 焦段
                            case 0x920A:
                                {
                                    var focalLength = BitConverter.ToInt32(propertyItem.Value, 0);

                                    OutputBox.Text += $@"焦段: {focalLength} mm";

                                    if (FocalSelector.SelectedIndex != 0)
                                    {
                                        double[] focal = { 1.5, 1.7, 1.3, 1.6, 1.5 };
                                        OutputBox.Text += $@" (等校焦段: {focalLength * focal[FocalSelector.SelectedIndex - 1]})";
                                    }

                                    OutputBox.Text += Environment.NewLine;
                                    break;
                                }

                            // ISO值
                            case 0x8827:
                                {
                                    var iso = BitConverter.ToUInt16(propertyItem.Value, 0);

                                    OutputBox.Text += $@"ISO: {iso}";
                                    OutputBox.Text += Environment.NewLine;
                                    break;
                                }

                            // 曝光時間
                            case 0x829A:
                                {
                                    var numerator = BitConverter.ToUInt32(propertyItem.Value, 0);
                                    var denominator = BitConverter.ToUInt32(propertyItem.Value, 4);

                                    if (denominator == 1 || denominator == 10)
                                    {
                                        OutputBox.Text += $@"曝光時間: {((double)numerator / denominator)} 秒";
                                    }
                                    else
                                    {
                                        OutputBox.Text += $@"曝光時間: {numerator}/{denominator} 秒";
                                    }
                                    OutputBox.Text += Environment.NewLine;
                                    break;
                                }

                            // 光圈
                            case 0x829D:
                                {
                                    var numerator = BitConverter.ToUInt32(propertyItem.Value, 0);
                                    var denominator = BitConverter.ToUInt32(propertyItem.Value, 4);
                                    var fStop = (double)numerator / denominator;
                                    OutputBox.Text += $@"光圈: f/{fStop}";
                                    OutputBox.Text += Environment.NewLine;
                                    break;
                                }
                        }
                    }
                    OutputBox.Text += Environment.NewLine;
                    ExportTXTButton.Enabled = true;
                    ExportWordButton.Enabled = true;
                    _isTouched = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"無法處理檔案！原因：{ex.Message}", @"錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                _imageLoadingDir = folderBrowserDialog.SelectedPath;
            }
            else return;
            
            ImageDataFetchProc();

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            FocalSelector.SelectedIndex = 0;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ImageDataFetchProc();
        }

        private void ExportTXTButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = @"純文字檔 (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            File.WriteAllText(saveFileDialog.FileName, OutputBox.Text);

        }

        private void ExportWordButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未完成，敬請期待");

            //saveFileDialog.Filter = @"文件檔(*.docx) | *.docx";

            //if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            //var saveFileDir = saveFileDialog.FileName;

            //using (var wordDocument = WordprocessingDocument.Create(saveFileDir, WordprocessingDocumentType.Document))
            //{
            //    var mainPart = wordDocument.AddMainDocumentPart();
            //    mainPart.Document = new Document();
            //    mainPart.Document.AppendChild(new Body());

            //    var table = new Table();

            //    var tp = new TableProperties(
            //        //指定田字形六條線的樣式及線寬
            //        new TableBorders(
            //            //Size 單位為 1/8 點 [註]
            //            new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
            //            new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
            //            new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
            //            new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
            //            new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
            //            new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 }
            //        )
            //    );
            //    tp.AppendChild(new TableWidth() { Width = "100%" });
            //    table.AppendChild(tp); 
            //    var row = new TableRow();

            //    for (var i = 0; i < ColumNumPickup.Value; i++)
            //    {
            //        var cell = new TableCell();
            //    }


            //    var cell1 = new TableCell();
            //    var cell2 = new TableCell();


            //    //cell1.Append(new Paragraph(new Run(new Text("123"))));
            //    //cell2.Append(new Paragraph(new Run(new Text("456"))));

            //    row.Append(cell1);
            //    row.Append(cell2);


            //    table.Append(row);


            //    mainPart.Document.Body.AppendChild(table);


            //    mainPart.Document.Save();
            //}

        }
    }

}
