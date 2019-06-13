using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTopdfApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> _fielPaths = new List<string>();
        string savePath = "";
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFiles = new OpenFileDialog();
            openFiles.Multiselect = true;
            openFiles.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

           

            if (openFiles.ShowDialog()==DialogResult.OK)
            {
                foreach(string item in openFiles.FileNames)
                {
                    listBox1.Items.Add(item);
                    _fielPaths.Add(item);
                }
                string fullPath = openFiles.FileNames[0];
                string fileName = openFiles.SafeFileName;
                savePath = fullPath.Replace(fileName, "szsoft_imageTopdf.pdf");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            _createPDF(_fielPaths);

        }

        public void _createPDF(List<string> _images)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Sigorta Gonder Dosya";

            foreach (string _image in _images)
            {

                XImage image = XImage.FromFile(_image);

                PdfPage pag = document.AddPage();
                pag.Height = (int)image.Height;
                pag.Width = (int)image.Width;

                XGraphics gfx = XGraphics.FromPdfPage(pag);


                gfx.DrawImage(image, 0, 0, (int)pag.Width, (int)pag.Height);
            }
            

            
            document.Save(savePath);
            listBox1.Items.Clear();
            _fielPaths.Clear();
            MessageBox.Show("PDF Olusturuldu\n", "Dosya Yolu:"+savePath);
        }

    }
}
