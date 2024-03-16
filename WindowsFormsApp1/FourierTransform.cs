using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    public class Complex
    {
        public double Real;
        public double Imaginary;
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }
        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }
        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        }
        public static Complex operator *(Complex a, double scalar)
        {
            return new Complex(a.Real * scalar, a.Imaginary * scalar);
        }
        public static Complex operator /(Complex a, double divisor)
        {
            return new Complex(a.Real / divisor, a.Imaginary / divisor);
        }
    }
    internal class FourierTransform
    {
        private int width;
        private int height;
        private Complex[,] spectrumFourier;
        private double[,] filter;
        public Bitmap resultImage;

        public Bitmap Work(Bitmap sourceImage, bool isLow, int radius)
        {
            width = sourceImage.Width;
            height = sourceImage.Height;

            spectrumFourier = new Complex[width, height];
            filter = new double[width, height];
            resultImage = new Bitmap(width, height);

            Converting(sourceImage);

            ApplyDFT(false, true); // Прямое FFT по столбцам
            ApplyDFT(true, true);  // Прямое FFT по строкам
            CenterFT();

            CreateFilter(isLow, radius);
            ApplicationFilter(true);

            CenterFT();
            ApplicationFilter(false);
            ApplyDFT(true, false);
            ApplyDFT(false, false);
            
            GetReconstructedImage();
            return resultImage;
        }

        private void Converting(Bitmap input)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = input.GetPixel(x, y);
                    spectrumFourier[x, y] = new Complex((int)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B), 0);
                }
            }
        }

        private void ApplyDFT(bool isRow, bool isForward)
        {
            for (int i = 0; i < (isRow ? height : width); i++)
            {
                Complex[] input = GetRowOrCol(i, isRow);
                Complex[] result = CalculateDFT(input, isForward);
                SetRowOrCol(i, result, isRow);
            }
        }

        private Complex[] GetRowOrCol(int index, bool isRow)
        {
            var result = new Complex[isRow ? width : height];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = isRow ? spectrumFourier[i, index] : spectrumFourier[index, i];
            }
            return result;
        }

        private void SetRowOrCol(int index, Complex[] input, bool isRow)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (isRow)
                    spectrumFourier[i, index] = input[i];
                else 
                    spectrumFourier[index, i] = input[i];
            }
        }

        private Complex[] CalculateDFT(Complex[] input, bool isForward)
        {
            var lenght = input.Length;
            var result = new Complex[lenght];

            for (int k = 0; k < lenght; k++)
            {
                var sum = new Complex(0,0);
                for (int n = 0; n < lenght; n++)
                {
                    var angle = isForward ? (-2 * Math.PI * k * n / lenght) : (2 * Math.PI * k * n / lenght);
                    var twiddleFactor = new Complex(Math.Cos(angle), Math.Sin(angle));
                    sum += input[n] * twiddleFactor;
                }
                result[k] = isForward ? sum : sum / lenght;

            }
            return result;
        }
        private void CenterFT()
        {
            var w = width / 2;
            var h = height / 2;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Complex tmp = spectrumFourier[x, y];
                    spectrumFourier[x, y] = spectrumFourier[x + w, y + h];
                    spectrumFourier[x + w, y + h] = tmp;

                    tmp = spectrumFourier[x + w, y];
                    spectrumFourier[x + w, y] = spectrumFourier[x, y + h];
                    spectrumFourier[x, y + h] = tmp;
                }
            }
        }

        private void CreateFilter(bool isLow, int radius)
        {
            int centerX = width / 2;
            int centerY = height / 2;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
                    filter[x, y] = 1 / (isLow ? (1 + Math.Pow(distance / radius, 2)) : (1 + Math.Pow(radius / distance, 2)));
                }
            }
        }
        private void ApplicationFilter(bool isForward)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (!isForward && filter[x, y] != 0)
                    {
                        spectrumFourier[x, y] = spectrumFourier[x, y] / filter[x, y];
                    }
                    else if (isForward)
                    {
                        spectrumFourier[x, y] = spectrumFourier[x, y] * filter[x, y];
                    }
                }
            }
        }

        private void GetReconstructedImage()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var intensity = Math.Min(255, Math.Max(0, (int)spectrumFourier[x, y].Real));
                    Color color = Color.FromArgb(intensity, intensity, intensity);
                    resultImage.SetPixel(x, y, color);
                }
            }
        }
    }
}
