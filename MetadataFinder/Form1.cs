using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace MetadataFinder
{
    public partial class MainWindow : Form
    {
        public class ImageData
        {
            public string FileName = string.Empty;
            public byte[] BinaryData;
            public Stream DataStream => new MemoryStream(BinaryData);
            public int SourceWidth;
            public int SourceHeight;
            public decimal Width;
            public decimal Height;
            public long WidthInEMU => Convert.ToInt64(Width * CM_TO_EMU);
            public long HeightInEMU => Convert.ToInt64(Height * CM_TO_EMU);
            private const decimal INCH_TO_CM = 2.54M;
            private const decimal CM_TO_EMU = 360000M;
            public string ImageName;
            public ImageData(string fileName, byte[] data, int dpi = 300)
            {
                FileName = fileName;
                BinaryData = data;
                Bitmap img = new Bitmap(new MemoryStream(data));
                SourceWidth = img.Width;
                SourceHeight = img.Height;
                Width = ((decimal)SourceWidth) / dpi * INCH_TO_CM;
                Height = ((decimal)SourceHeight) / dpi * INCH_TO_CM;
                ImageName = $"IMG_{Guid.NewGuid().ToString().Substring(0, 8)}";
            }
            public ImageData(string fileName, int dpi = 300) :
                this(fileName, File.ReadAllBytes(fileName), dpi)
            {
            }
        }
        
        private string _imageDir = "";
        private List<Dictionary<string, string>> _imageMetaList = new();
        
        private List<Dictionary<string, string>> ImageDataFetchProc(string imageLoadingDir)
        {
            var metaDataList = new List<Dictionary<string, string>>();

            _imageMetaList.Clear();


            if (FocalSelector.SelectedIndex == 0)
            {
                MessageBox.Show(
                    $@"�z��ܤF�����T��X�A�N���|��ܵ��ĵJ�q�I{Environment.NewLine}�p�G�o���O�z�Q�n���A�i�H�b���s��n�J�q����U���s��z���s�C",
                    @"�T�w��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }

            var imageFiles = Directory.GetFiles(imageLoadingDir, "*.jpg");



            foreach (var imagePath in imageFiles)
            {
                var metaData = new Dictionary<string, string>
                {
                    ["�ɮצW��"] = imagePath
                };

                try
                {
                    var image = Image.FromFile(imagePath);
                    foreach (var propertyItem in image.PropertyItems)
                    {
                        switch (propertyItem.Id)
                        {
                            // �ɶ�
                            case 0x0132:
                                {
                                    metaData["����ɶ�"] = Encoding.UTF8.GetString(propertyItem.Value ?? Array.Empty<byte>())
                                        .Replace("\0", string.Empty);
                                    break;
                                }

                            // �J�q
                            case 0x920A:
                                {
                                    var focalLength = BitConverter.ToInt32(propertyItem.Value ?? Array.Empty<byte>(), 0);
                                    
                                    metaData["�J�q"] = $@"{focalLength} mm";

                                    if (radioButton1.Checked && FocalSelector.SelectedIndex != 0)
                                    {
                                        double[] focal = { 1.5, 1.7, 1.3, 1.6, 1.5 };
                                        //metaData["���ĵJ�q"] =
                                        //    $@"{Math.Round(focalLength * focal[FocalSelector.SelectedIndex - 1], 1)} mm";
                                        metaData["�J�q"] +=
                                            $@" (���ĵJ�q: {Math.Round(focalLength * focal[FocalSelector.SelectedIndex - 1], 1)} mm)";
                                    }
                                    else if (radioButton2.Checked && Math.Abs((double)FocalNumericSelector.Value - 1.0) > 0.01)
                                    {
                                        metaData["�J�q"] +=
                                            $@" (���ĵJ�q: {Math.Round(focalLength * FocalNumericSelector.Value, 1)} mm)";
                                    }
                                    break;
                                }

                            // ISO��
                            case 0x8827:
                                {
                                    var iso = BitConverter.ToUInt16(propertyItem.Value ?? Array.Empty<byte>(), 0);
                                    metaData["ISO"] = $@"{iso}";

                                    break;
                                }

                            // �n���ɶ�
                            case 0x829A:
                                {
                                    var numerator = BitConverter.ToUInt32(propertyItem.Value ?? Array.Empty<byte>(), 0);
                                    var denominator = BitConverter.ToUInt32(propertyItem.Value ?? Array.Empty<byte>(), 4);

                                    if (denominator is 1 or 10)
                                    {
                                        metaData["�n���ɶ�"] = @$"{(double)numerator / denominator} ��";
                                    }
                                    else
                                    {
                                        metaData["�n���ɶ�"] = $@"{numerator}/{denominator} ��";
                                    }
                                    break;
                                }

                            // ����
                            case 0x829D:
                                {
                                    var numerator = BitConverter.ToUInt32(propertyItem.Value ?? Array.Empty<byte>(), 0);
                                    var denominator = BitConverter.ToUInt32(propertyItem.Value ?? Array.Empty<byte>(), 4);
                                    var fStop = (double)numerator / denominator;
                                    metaData["����"] = $@"����: f/{fStop}";

                                    break;
                                }
                        }
                    }
                    metaDataList.Add(metaData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"�L�k�B�z�ɮסI��]�G{ex.Message}", @"���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return metaDataList;
        }

        private void UpdateView(List<Dictionary<string, string>> imageMetaDataList)
        {
            OutputBox.Text = "";
            foreach (var metaData in imageMetaDataList)
            {
                foreach (var meta in metaData)
                {
                    OutputBox.Text += $@"{meta.Key}: {meta.Value}{Environment.NewLine}";
                }

                OutputBox.Text += Environment.NewLine;
            }
        }

        private static Drawing AddImage(string relationshipId, ImageData img, int cellWidth)
        {
            long size_x = cellWidth;
            long size_y = (int)(((double)img.HeightInEMU / img.WidthInEMU) * cellWidth);
            

            return new Drawing(
                     new DW.Inline(
                         //Size of image, unit = EMU(English Metric Unit)
                         //1 cm = 360000 EMUs
                         new DW.Extent() { Cx = size_x, Cy = size_y },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = img.ImageName
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = img.FileName
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents()
                                             {
                                                 Cx = size_x,
                                                 Cy = size_y
                                             }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });
        }
        

        public MainWindow()
        {
            InitializeComponent();
        }


        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

            _imageDir = folderBrowserDialog.SelectedPath;

            var fileCount = Directory.GetFiles(_imageDir, "*.jpg").Length;

            switch (fileCount)
            {
                case > 50:
                    MessageBox.Show(
                    @"�z���b�}�Ҥj�q�Ϥ��ɮסI�o�˰��ڵL�k�O�ҵ{����í�w�ʡC��ĳ�z���˹Ϥ��C",
                    @"�j�q�}��ĵ�i",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                    break;

                case 0:
                    MessageBox.Show(
                        @"�䤣���Ƨ������Ϥ��ɮסI"
                    );
                    break;
            }

            OutputBox.Text = @$"�w���J{fileCount}�i�Ϥ��A���U�k�W�誺Ū���HŪ���z���ɮסC";
            ExportTXTButton.Enabled = false;
            ExportWordButton.Enabled = false;
            ReadButton.Enabled = true;
            ReadButton.Text = @"Ū��";
            ReadButton.Focus();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked && FocalSelector.SelectedIndex == -1)
            {
                MessageBox.Show(
                    @"�п�ܥ��U�誺�J�q�I",
                    @"���~",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            _imageMetaList = ImageDataFetchProc(_imageDir);
            UpdateView(_imageMetaList);
            ExportTXTButton.Enabled = true;
            ExportWordButton.Enabled = true;
            ReadButton.Text = @"���sŪ��";
        }

        private void ExportTXTButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.Reset();
            saveFileDialog.Filter = @"�¤�r�� (*.txt)|*.txt";


            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            File.WriteAllText(saveFileDialog.FileName, OutputBox.Text);

        }

        private void ExportWordButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.Reset();
            saveFileDialog.Filter = @"�����(*.docx) | *.docx";


            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            var saveFileDir = saveFileDialog.FileName;

            if (_imageMetaList.Count < ColumNumPickup.Value)
            {
                MessageBox.Show(
                    @"�����z�A�z���Ϥ��ƶq�����z�]�w���C�ơA���ͪ����|���ťժ��C��I", 
                    @"����",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }

            var totalRow = (int)Math.Ceiling(_imageMetaList.Count / (double)ColumNumPickup.Value) * 2;

            var retries = 0;
            while (true)
            {
                try
                {
                    using var wordDocument = WordprocessingDocument.Create(saveFileDir, WordprocessingDocumentType.Document);
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

                    // Table total width = A4 Text area (5.8 inch) - (Number of Column + 1) * line width (1 point)
                    // DXA Width = 1/20 point = 1/1440 inch
                    // Total width in points = 5.8 * 72 - (Num of Col. + 1) 
                    // Total width in DXA = (Total width in points) * 20
                    tp.AppendChild(new TableWidth() { Type = TableWidthUnitValues.Dxa, Width =
                        ((417.6 - ((int)ColumNumPickup.Value + 1)) * 20).ToString("0.0") });

                    table.AppendChild(tp);


                    for (var i = 0; i < totalRow; i++)
                    {
                        var row = new TableRow();
                        for (var j = 0; j < ColumNumPickup.Value; j++)
                        {
                            var cell = new TableCell();
                            var photoIndex = i / 2 * (int)ColumNumPickup.Value + j;
                            if (photoIndex < _imageMetaList.Count)
                            {
                                if (i % 2 == 0)
                                {

                                    var img = new ImageData(_imageMetaList[photoIndex]["�ɮצW��"]);
                                    var imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                                    var relationshipId = mainPart.GetIdOfPart(imagePart);
                                    imagePart.FeedData(img.DataStream);
                                    
                                    // Cell width = (5.8inch - (Number of column + 1) * table line width) / Number of Column
                                    // 5.8 inch => 5.8 * 914400 = 5303520 EMU
                                    var cellWidth = (5303520 - 12700 * ((int)ColumNumPickup.Value + 1)) /
                                                    (int)ColumNumPickup.Value;

                                    // Put image
                                    cell.Append(
                                        new Paragraph(
                                            new Run(
                                                AddImage(relationshipId, img, cellWidth)
                                            )
                                        )
                                    );
                                }
                                else
                                {
                                    // Put description
                                    var run = new Run();
                                    foreach (var meta in _imageMetaList[photoIndex].Where(meta => meta.Key is not (@"�ɶ�" or @"�ɮצW��")))
                                    {
                                        run.AppendChild(new Text($@"{meta.Key}: {meta.Value}"));
                                        if (meta.Key != _imageMetaList[photoIndex].Last().Key) run.AppendChild(new Break());
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

                    mainPart.Document.Body?.AppendChild(table);

                    mainPart.Document.Save();
                    break;
                }
                catch (IOException)
                {
                    if (retries < 10)
                    {
                        retries++;
                        continue;
                    }
                    var res = MessageBox.Show(
                        @"�й���������J�Ϥ��B��X����A�A���դ@�M�C", 
                        @"�ɮ׿��~", 
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Hand
                        );
                    if (res == DialogResult.Cancel) break;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                FocalSelector.Enabled = true;
                FocalNumericSelector.Enabled = false;
            }
            else
            {
                FocalSelector.Enabled = false;
                FocalNumericSelector.Enabled = true;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            FocalSelector.SelectedIndex = 0;
        }
    }

}
