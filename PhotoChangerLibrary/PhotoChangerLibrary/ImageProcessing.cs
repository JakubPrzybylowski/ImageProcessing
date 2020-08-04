using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace PhotoChangerLibrary
{
    public class ImageProcessing
    { 
        public Image LoadImage(string fileName)
        {
           Image image = Image.FromFile($"{fileName}");
            return image;
        }

        public void SaveImage(Image image,string fileName)
        {
            image.Save($"{fileName}");
        }
        public Image ToMainColorSync(Image image)
        {
            Bitmap processedBitmap = (Bitmap)image;
            BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
     
            unsafe
            { 
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];
                        if (oldRed > oldGreen && oldRed > oldBlue)
                        {
                            currentLine[x] = 0;
                            currentLine[x + 1] = 0;
                            currentLine[x + 2] = 255;

                        }
                        else if (oldGreen > oldRed && oldGreen > oldBlue)
                        {
                            currentLine[x] = 0;
                            currentLine[x + 1] = 255;
                            currentLine[x + 2] = 0;
                        }
                        else if (oldBlue > oldRed && oldBlue > oldGreen)
                        {
                            currentLine[x] = 255;
                            currentLine[x + 1] = 0;
                            currentLine[x + 2] = 0;
                        }else
                        {
                            currentLine[x] = (byte)oldBlue;
                            currentLine[x + 1] = (byte)oldGreen;
                            currentLine[x + 2] = (byte)oldRed;
                        }
                    }
                });
                processedBitmap.UnlockBits(bitmapData);
                return processedBitmap;
            }
        }
        public async Task<Image> ToMainColorAsync(Image image)
        {
            Bitmap processedBitmap = (Bitmap)image;
            BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
            await Task.Run(() => { 
                unsafe
                { 
                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                    int heightInPixels = bitmapData.Height;
                    int widthInBytes = bitmapData.Width * bytesPerPixel;
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                    Parallel.For(0, heightInPixels, y =>
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                        for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                        {
                            int oldBlue = currentLine[x];
                            int oldGreen = currentLine[x + 1];
                            int oldRed = currentLine[x + 2];
                            if (oldRed > oldGreen && oldRed > oldBlue)
                            {
                                currentLine[x] = 0;
                                currentLine[x + 1] = 00;
                                currentLine[x + 2] = 255;

                            }
                            else if (oldGreen > oldRed && oldGreen > oldBlue)
                            {
                                currentLine[x] = 0;
                                currentLine[x + 1] = 255;
                                currentLine[x + 2] = 0;
                            }
                            else if (oldBlue > oldRed && oldBlue > oldGreen)
                            {
                                currentLine[x] = 255;
                                currentLine[x + 1] = 0;
                                currentLine[x + 2] = 0;
                            }
                        }
                    });
                    processedBitmap.UnlockBits(bitmapData);
                }
                
            });
            return processedBitmap;
            
        }
    }
}
