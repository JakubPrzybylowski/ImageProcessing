using PhotoChangerLibrary;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        string fileName;
        Image image;
        readonly Stopwatch stopwatch = new Stopwatch();
        ImageProcessingMethod method = new ImageProcessingMethod(new ImageProcessing());
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpdateImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = dialog.FileName;
                    pictureBox1.Image = method.LoadFile(fileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImageProcSyns_Click(object sender, EventArgs e)
        {
            stopwatch.Restart();
            stopwatch.Start();
            pictureBox1.Image = method.ToMainColorsSync(pictureBox1.Image);
            stopwatch.Stop();
            label2.Text = (stopwatch.ElapsedMilliseconds + "ms");
        }

        private async void btnImageProcAsync_Click(object sender, EventArgs e)
        {
            stopwatch.Restart();
            stopwatch.Start();
            pictureBox1.Image = await Task.Run(() => method.ToMainColorsAsync(pictureBox1.Image));
            stopwatch.Stop();
            label3.Text = (stopwatch.ElapsedMilliseconds + "ms");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            image = pictureBox1.Image;
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                    fileName = dialog.FileName;
                    method.SaveImage(image, fileName);
          
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}