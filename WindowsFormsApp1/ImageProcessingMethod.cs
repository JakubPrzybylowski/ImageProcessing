
using PhotoChangerLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;
using Xunit.Sdk;

namespace WindowsFormsApp1
{
    public class ImageProcessingMethod
    {
        private ImageProcessing imageProcessing;
        private Image image;

        public ImageProcessingMethod(ImageProcessing imageProcessing)
        {
            this.imageProcessing = imageProcessing;
        }

        public Image LoadFile(string fileName)  
        {
            this.image = null;

            if (fileName != null)
            {
                this.image = imageProcessing.LoadImage(fileName);
            }
            return this.image;
        }
        public bool SaveImage(Image image, string fileName)
        {
            bool isSaved;
            if (image != null)
            {
                imageProcessing.SaveImage(image, fileName);
                isSaved = true;
                return isSaved;
            }
            else
            {
                return isSaved = false;
            }
        }

        public Image ToMainColorsSync(Image image)
        {
            if (image != null)
            {
                image = imageProcessing.ToMainColorSync(image);
                return image;
            }
            else
            {
                MessageBox.Show("No image chose ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public Image ToMainColorsAsync(Image image)
        {
            if (image != null)
            {
                image = imageProcessing.ToMainColorAsync(image).Result;
                return image;
            }
            else
            {
                MessageBox.Show("No image chose ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
