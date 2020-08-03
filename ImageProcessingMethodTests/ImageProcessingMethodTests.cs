

using PhotoChangerLibrary;
using System;
using System.Drawing;
using WindowsFormsApp1;
using Xunit;



namespace ImageProcessingMethodTests
{
    public class ImageProcessingMethodTests
    {
        [Fact]
        public void LoadFile_FileNameIsEmpty_ThrowExceptation()
        {
            // Arrange
            string fileName = string.Empty;
            var img = new ImageProcessingMethod(new ImageProcessing());

            // Assert
            Assert.Throws<ArgumentException>(() => img.LoadFile(fileName));
        }

        [Fact]
        public void LoadFile_CorrectFileName_ResultNotEmpty()
        {
            // Arrange
            string fileName = "./Images/test2.jpg";
  
            var img = new ImageProcessingMethod(new ImageProcessing());

            // Act
            var result = img.LoadFile(fileName);

            // Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public void ToMainColorSync_CorrectImage_ReturnNotNull()
        {
            // Arrange
            string fileName = "./Images/test2.jpg";
            var img = new ImageProcessingMethod(new ImageProcessing());
            var image = img.LoadFile(fileName);

            // Act
            var result = img.ToMainColorsSync(image);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ToMainColorSync_ImageIsNull_ReturnNull()
        {
            // Arrange
            var img = new ImageProcessingMethod(new ImageProcessing());

            // Act
            var result = img.ToMainColorsSync(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToMainColorAsync_CorrectImage_ReturnNotNull()
        {
            // Arrange
            string fileName = "./Images/test2.jpg";
            var img = new ImageProcessingMethod(new ImageProcessing());
            var image = img.LoadFile(fileName);

            // Act
            var result = img.ToMainColorsAsync(image);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ToMainColorAsync_ImageIsNull_ReturnNull()
        {
            // Arrange
            var img = new ImageProcessingMethod(new ImageProcessing());

            // Act
            var result = img.ToMainColorsAsync(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void SaveImage_FileNameIsEmpty_ReturnFalse()
        {
            // Arrange
            string fileName = string.Empty;
            Image image = null;
            ImageProcessingMethod img = new ImageProcessingMethod(new ImageProcessing());


            // Assert
            Assert.False(img.SaveImage(image,fileName));
        }


        [Fact]
        public void SaveImage_FileIsCorrect_ReturnTrue()
        {
            // Arrange
            string fileName = string.Empty;
            Image image = null;
            ImageProcessingMethod img = new ImageProcessingMethod(new ImageProcessing());


            // Assert
            Assert.False(img.SaveImage(image, fileName));
        }
    }
}
