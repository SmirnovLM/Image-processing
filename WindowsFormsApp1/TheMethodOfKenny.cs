using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class TheMethodOfKenny
    {
        private double[,] matrixAngles;
        private Bitmap image;
        public Bitmap WorkTheMethodOfKenny(Bitmap sourceImage)
        {
            matrixAngles = new double[sourceImage.Width, sourceImage.Height];
            image = sourceImage;
            image = ConvertImageToGrayScale();
            image = Smoothing(3, 5);
            image = ResetImageBounds();
            image = GradientAndAngleCalculation();
            image = SuppressionNonMaximums();
            image = ThresholdFiltering(0.5,0.6);
            image = BlobAnalysis();
            
            return image;
        }

        private Bitmap ConvertImageToGrayScale()
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var color = image.GetPixel(x, y);
                    var grayScale = (int)(color.R * 0.299 + color.G * 0.587 + color.B * 0.114);
                    image.SetPixel(x, y, Color.FromArgb(grayScale, grayScale, grayScale));       
                }
            }
            return image;
        }

        private Bitmap ResetImageBounds()
        {
            for (var x = 0; x < image.Width; x++)
            {
                image.SetPixel(x, 0, Color.FromArgb(0, 0, 0));
                image.SetPixel(x, image.Height - 1, Color.FromArgb(0, 0, 0));
            }
            for (var y = 0; y < image.Height; y++)
            {
                image.SetPixel(0, y, Color.FromArgb(0,0,0));
                image.SetPixel(image.Width - 1, y, Color.FromArgb(0, 0, 0));
            }
            return image;
        }

        private Bitmap Smoothing(int radius, int sigma)
        {
            // изображение, радиус ядра, степень размытия
            int size = 2 * radius + 1; // Размер ядра
            double[,] kernelOfGaussian = new double[size, size]; // Ядро фильтра Гаусса
            double sumOfElementsInKernel = 0; // Сумма элементов в ядре

            // Вычисление элементов ядра Гаусса в двумерном случае
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    kernelOfGaussian[x, y] = 
                        Math.Exp(-(x * x + y * y) / (2 * sigma * sigma)) / 
                        (2 * Math.PI * sigma * sigma);
                    sumOfElementsInKernel += kernelOfGaussian[x, y];
                }
            }
            // Нормализация ядра фильтра Гаусса
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    kernelOfGaussian[x, y] /= sumOfElementsInKernel;
                }
            }

            // Свертка с использованием ядра Гаусса
            for (int x = radius; x < image.Width - radius; x++)
            {
                for (int y = radius; y < image.Height - radius; y++)
                {
                    int intensity = 0;
                    for (int i = -radius; i <= radius; i++)
                    {
                        for (int j = -radius; j <= radius; j++)
                        {
                            intensity += (int)(image.GetPixel(x + i, y + j).R * kernelOfGaussian[i + radius, j + radius]);
                        }
                    }
                    image.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
                }
            }
            return image;
        }

        private Bitmap GradientAndAngleCalculation()
        {
            Bitmap resultImage = new Bitmap(image.Width, image.Height); 
            var sobelX = new int[,] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } }; // Матрица для Производной по Х
            var sobelY = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } }; // Матрица для Производной по У

            for (int x = 1; x < resultImage.Width - 1; x++)
            {
                for (int y = 1; y < resultImage.Height - 1; y++)
                {
                    int GX = 0, GY = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            GX += image.GetPixel(x + i, y + j).R * sobelX[i + 1, j + 1];
                            GY += image.GetPixel(x + i, y + j).R * sobelY[i + 1, j + 1];
                        }
                    }
                    var gradient = (int)(Math.Sqrt(GX * GX + GY * GY));
                    gradient = Math.Max(0, Math.Min(255, gradient));
                    resultImage.SetPixel(x, y, Color.FromArgb(gradient, gradient, gradient));

                    var angle = Math.Round(Math.Atan2(GY, GX) * (Math.PI / 4)) * (Math.PI / 4) - (Math.PI / 2);
                    matrixAngles[x, y] = angle;
                    
                }
            }
            return resultImage;
        }

        private Bitmap SuppressionNonMaximums()
        {
            for (var x = 1; x < image.Width - 1; x++)
            {
                for (var y = 1; y < image.Height - 1; y++)
                {
                    var dx = Math.Sign(Math.Cos(matrixAngles[x, y]));
                    var dy = -Math.Sign(Math.Sin(matrixAngles[x, y]));

                    var intensity1 = image.GetPixel(x + dx, y + dy).R;
                    var intensity2 = image.GetPixel(x - dx, y - dy).R;
                    var intensity = image.GetPixel(x, y).R;

                    if ( intensity < intensity1 || intensity < intensity2)
                    {
                        image.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return image;
        } 

        private Bitmap ThresholdFiltering(double lower, double upper)
        {
            for (int x = 1; x < image.Width - 1; x++)
            {
                for (int y = 1; y < image.Height - 1; y++)
                {
                    double intensity = image.GetPixel(x, y).R;

                    if (intensity <= lower * 255)
                    {
                        image.SetPixel(x, y, Color.Black);
                    }
                    else if (intensity >= upper * 255)
                    {
                        image.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        image.SetPixel(x, y, Color.FromArgb(127, 127, 127));
                    }
                }
            }
            return image;
        }

        private Bitmap BlobAnalysis()
        {
            var moveDir = new int[2, 8] { { -1, -1, -1, 0, 0, 1, 1, 1 }, { -1, 0, 1, -1, 1, -1, 0, 1 } };
            for (var x = 1; x < image.Width - 1; x++)
            {
                for (var y = 1; y < image.Height - 1; y++)
                {
                    if (image.GetPixel(x,y).R == 255)
                    {
                        for (var k = 0; k < 8; k++)
                        {
                            var color = image.GetPixel(x + moveDir[0, k], y + moveDir[1, k]).R;
                            if (color == 127)
                            {
                                image.SetPixel(x + moveDir[0, k], y + moveDir[1, k], Color.White);
                            }
                        }
                    }
                }
            }
            return image;
        }
    }
}
