using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text;


namespace MetadataFinder
{
    public partial class MainWindow : Form
    {
        private string _imageLoadingDir = "";
        //private bool _isTouched;
        //private int _fileCount;
        private readonly List<string> _metaData = new();

        private void ImageDataFetchProc()
        {
            OutputBox.Clear();

            var imageFiles = Directory.GetFiles(_imageLoadingDir, "*.jpg");

            if(imageFiles.Length > 50)
            {
                var result = MessageBox.Show(
                    @"�z���b�}�Ҥj�q���ɮסI�ڱN�L�k�O�ҥ\�઺í�w�ʡA�z�T�w�n�~��ܡH",
                    @"�ɮפj�q�}��ĵ�i",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2
                    );

                if (result == DialogResult.No) return;
            }
            
            foreach (var imagePath in imageFiles)
            {

                OutputBox.Text += imagePath;
                OutputBox.Text += Environment.NewLine;

                var metaData = "";
                try
                {
                    var image = Image.FromFile(imagePath);
                    //statusStrip.Text = @"�B�z��";
                    foreach (var propertyItem in image.PropertyItems)
                    {
                        switch (propertyItem.Id)
                        {
                            // �ɶ�
                            case 0x0132:
                                {
                                    var dateTaken = Encoding.UTF8.GetString(propertyItem.Value).Replace("\0", string.Empty);
                                    metaData += $@"����ɶ�: {dateTaken}";
                                    metaData += Environment.NewLine;
                                    break;
                                }
                            // �J�q
                            case 0x920A:
                                {
                                    var focalLength = BitConverter.ToInt32(propertyItem.Value, 0);

                                    metaData += $@"�J�q: {focalLength} mm";

                                    if (FocalSelector.SelectedIndex != 0)
                                    {
                                        double[] focal = { 1.5, 1.7, 1.3, 1.6, 1.5 };
                                        metaData += $@" (���յJ�q: {Math.Round(focalLength * focal[FocalSelector.SelectedIndex - 1], 1)})";
                                    }

                                    metaData += Environment.NewLine;
                                    break;
                                }

                            // ISO��
                            case 0x8827:
                                {
                                    var iso = BitConverter.ToUInt16(propertyItem.Value, 0);

                                    metaData += $@"ISO: {iso}";
                                    metaData += Environment.NewLine;
                                    break;
                                }

                            // �n���ɶ�
                            case 0x829A:
                                {
                                    var numerator = BitConverter.ToUInt32(propertyItem.Value, 0);
                                    var denominator = BitConverter.ToUInt32(propertyItem.Value, 4);

                                    if (denominator is 1 or 10)
                                    {
                                        metaData += $@"�n���ɶ�: {((double)numerator / denominator)} ��";
                                    }
                                    else
                                    {
                                        metaData += $@"�n���ɶ�: {numerator}/{denominator} ��";
                                    }
                                    metaData += Environment.NewLine;
                                    break;
                                }

                            // ����
                            case 0x829D:
                                {
                                    var numerator = BitConverter.ToUInt32(propertyItem.Value, 0);
                                    var denominator = BitConverter.ToUInt32(propertyItem.Value, 4);
                                    var fStop = (double)numerator / denominator;
                                    metaData += $@"����: f/{fStop}";
                                    metaData += Environment.NewLine;
                                    break;
                                }
                        }
                    }

                    _metaData.Add(metaData);
                    OutputBox.Text += metaData;
                    OutputBox.Text += Environment.NewLine;
                    //_fileCount++;

                    //_isTouched = true;
                    RefreshButton.Enabled = true;
                    ExportTXTButton.Enabled = true;
                    ExportWordButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"�L�k�B�z�ɮסI��]�G{ex.Message}", @"���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            saveFileDialog.Filter = @"�¤�r�� (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            File.WriteAllText(saveFileDialog.FileName, OutputBox.Text);

        }

        private void ExportWordButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"�������A�q�д���");

            saveFileDialog.Filter = @"�����(*.docx) | *.docx";

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
                        new TableBorders(
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

                    var totalRow = (int)Math.Ceiling(_metaData.Count / (double)ColumNumPickup.Value) * 2;

                    for (var i = 0; i < totalRow; i++)
                    {
                        var row = new TableRow();
                        for (var j = 0; j < ColumNumPickup.Value; j++)
                        {
                            var cell = new TableCell();
                            var photoIndex = i / 2 * (int)ColumNumPickup.Value + j;
                            if (photoIndex < _metaData.Count)
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
                                    var lines = _metaData[photoIndex].Split(Environment.NewLine);
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
                MessageBox.Show(@"�Х��N��X��������I", @"���~", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            

        }
    }

}
