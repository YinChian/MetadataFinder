using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MetadataFinder
{
    public partial class MainWindow : Form
    {
        private string _imageLoadingDir = "";
        private bool _isTouched;
        private int _fileCount;
        private List<string> _metaData = new();

        private void ImageDataFetchProc()
        {
            OutputBox.Clear();

            var imageFiles = Directory.GetFiles(_imageLoadingDir, "*.jpg");
            
            foreach (var imagePath in imageFiles)
            {

                OutputBox.Text += imagePath;
                OutputBox.Text += Environment.NewLine;

                var metaData = "";
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

                                    metaData += $@"焦段: {focalLength} mm";

                                    if (FocalSelector.SelectedIndex != 0)
                                    {
                                        double[] focal = { 1.5, 1.7, 1.3, 1.6, 1.5 };
                                        metaData += $@" (等校焦段: {Math.Round(focalLength * focal[FocalSelector.SelectedIndex - 1], 1)})";
                                    }

                                    metaData += Environment.NewLine;
                                    break;
                                }

                            // ISO值
                            case 0x8827:
                                {
                                    var iso = BitConverter.ToUInt16(propertyItem.Value, 0);

                                    metaData += $@"ISO: {iso}";
                                    metaData += Environment.NewLine;
                                    break;
                                }

                            // 曝光時間
                            case 0x829A:
                                {
                                    var numerator = BitConverter.ToUInt32(propertyItem.Value, 0);
                                    var denominator = BitConverter.ToUInt32(propertyItem.Value, 4);

                                    if (denominator is 1 or 10)
                                    {
                                        metaData += $@"曝光時間: {((double)numerator / denominator)} 秒";
                                    }
                                    else
                                    {
                                        metaData += $@"曝光時間: {numerator}/{denominator} 秒";
                                    }
                                    metaData += Environment.NewLine;
                                    break;
                                }

                            // 光圈
                            case 0x829D:
                                {
                                    var numerator = BitConverter.ToUInt32(propertyItem.Value, 0);
                                    var denominator = BitConverter.ToUInt32(propertyItem.Value, 4);
                                    var fStop = (double)numerator / denominator;
                                    metaData += $@"光圈: f/{fStop}";
                                    metaData += Environment.NewLine;
                                    break;
                                }
                        }
                    }

                    _metaData.Add(metaData);
                    OutputBox.Text += metaData;
                    OutputBox.Text += Environment.NewLine;
                    _fileCount++;

                    _isTouched = true;
                    ExportTXTButton.Enabled = true;
                    ExportWordButton.Enabled = true;
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
            MessageBox.Show(@"未完成，敬請期待");

            saveFileDialog.Filter = @"文件檔(*.docx) | *.docx";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            var saveFileDir = saveFileDialog.FileName;
            try
            {
                using (var wordDocument = WordprocessingDocument.Create(saveFileDir, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    mainPart.Document.AppendChild(new Body());

                    var table = new Table();

                    var tp = new TableProperties(
                        //指定田字形六條線的樣式及線寬
                        new TableBorders(
                            //Size 單位為 1/8 點 [註]
                            new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                            new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                            new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                            new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                            new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                            new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 }
                        )
                    );
                    tp.AppendChild(new TableWidth() { Width = "100%" });
                    table.AppendChild(tp);

                    var totalRow = (int)Math.Ceiling(_fileCount / (double)ColumNumPickup.Value) * 2;

                    for (var i = 0; i < totalRow; i++)
                    {
                        var row = new TableRow();
                        for (var j = 0; j < ColumNumPickup.Value; j++)
                        {
                            var cell = new TableCell();

                            var photo_index = i / 2 * (int)ColumNumPickup.Value + j;
                            if (photo_index < _metaData.Count)
                            {
                                if (i % 2 == 0)
                                {
                                    // Put image
                                    cell.Append(
                                        new Paragraph(
                                            new Run(
                                                new Text("")
                                            )
                                        )
                                    );
                                }
                                else
                                {
                                    // Put description

                                    var run = new Run();
                                    var lines = _metaData[photo_index].Split(Environment.NewLine);
                                    run.AppendChild(new Text(lines[0]));
                                    for (var ii = 1; ii < lines.Length - 1; ii++)
                                    {
                                        run.AppendChild(new Break());
                                        run.AppendChild(new Text(lines[ii]));
                                    }
                                    cell.Append(new Paragraph(run));
                                }
                            }
                            else
                            {
                                cell.Append(new Paragraph(new Run(new Text(""))));
                            }

                            row.Append(cell);
                        }
                        table.Append(row);
                    }

                    mainPart.Document.Body.AppendChild(table);


                    mainPart.Document.Save();
                }
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(@"請先將輸出文件關閉！", @"錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            

        }
    }

}
