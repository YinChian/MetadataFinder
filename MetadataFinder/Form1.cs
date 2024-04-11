using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MetadataFinder
{
    public partial class MainWindow : Form
    {
        private string imgdir = "";
        private readonly Dictionary<string, int> exifConst = new Dictionary<string, int>() {
            {"ExposureTime", 0x829A},
            {"FNumber", 0x829D},
            {"ISOSpeed", 0x8827},
            {"FocalLength", 0x920A}
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                imgdir = folderBrowserDialog.SelectedPath;
            }
            else return;

            OutputBox.Clear();

            string[] imageFiles = Directory.GetFiles(imgdir, "*.jpg");

            foreach (string imagePath in imageFiles)
            {
                try
                {
                    Image image = Image.FromFile(imagePath);
                    foreach (KeyValuePair<string, int> entry in exifConst)
                    {
                        // do something with entry.Value or entry.Key

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"�L�k�B�z�ɮסI\n��]�G{ex.Message}", "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            FocalSelector.SelectedIndex = 0;
        }
    }
}
