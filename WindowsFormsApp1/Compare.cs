using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Compare
    {
        public Bitmap processImage(Bitmap sourceImage)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }

        private Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int intensity = (int)(0.36 * sourceColor.R) + (int)(0.53 * sourceColor.G) + (int)(0.11 * sourceColor.B);
            return Color.FromArgb(intensity, intensity, intensity);
        }

        public int Clamp(int value, int min = 0, int max = 255)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }

    class MSE
    {
        public double mse_value;

        public double Work(Bitmap image1, Bitmap image2)
        {
            for (int i = 0; i < image1.Width; i++)
            {
                for (int j = 0; j < image1.Height; j++)
                {
                    Color color1 = image1.GetPixel(i, j);
                    Color color2 = image2.GetPixel(i, j);
                    int tmp = color1.R - color2.R;
                    mse_value += Math.Pow(tmp, 2);
                }
            }
            mse_value = mse_value / (image1.Width * image2.Height);
            return mse_value;
        }
    }
    class UQI
    {
        private double uqi, uqim, _x, _y, sigma_x_2, sigma_y_2, sigma_x_y;

        public double Work(Bitmap image1, Bitmap image2)
        {
            counting_x(image1);
            counting_y(image2);
            counting_sigma_x_2(image1);
            counting_sigma_y_2(image2);
            counting_sigma_x_y(image1, image2);

            uqi = (sigma_x_y * 4 * (_x * _y + 1)) /
                ((sigma_x_2 + sigma_y_2 + 1) * (Math.Pow(_x, 2) + Math.Pow(_y, 2) + 1));
            return uqi;
        }

        public double counting_x(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var color = image.GetPixel(i, j);
                    _x += color.R;
                }
            }
            return _x / (image.Width * image.Height);
        }
        public double counting_y(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var color = image.GetPixel(i, j);
                    _y += color.R;
                }
            }
            return _y / (image.Width * image.Height);
        }
        public double counting_sigma_x_2(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var color = image.GetPixel(i, j);
                    sigma_x_2 += Math.Pow((color.R - _x), 2);
                }
            }
            return sigma_x_2 / (image.Width * image.Height);
        }
        public double counting_sigma_y_2(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var color = image.GetPixel(i, j);
                    sigma_y_2 += Math.Pow((color.R - _y), 2);
                }
            }
            return sigma_y_2 / (image.Width * image.Height);
        }
        public double counting_sigma_x_y(Bitmap image1, Bitmap image2)
        {
            for (int i = 0; i < image1.Width; i++)
            {
                for (int j = 0; j < image1.Height; j++)
                {
                    var color1 = image1.GetPixel(i, j);
                    var color2 = image2.GetPixel(i, j);
                    sigma_x_y += (color1.R - _x) * (color2.R - _y);
                }
            }
            return sigma_x_y / (image1.Width * image1.Height);
        }

        public double WorkM(Bitmap image1, Bitmap image2)
        {
            int k = 10;      // 480, 360
            Bitmap i1 = new Bitmap(image1.Width / k, image1.Height / k);
            Bitmap i2 = new Bitmap(image2.Width / k, image2.Height / k);

            int l1 = 0, l2 = 0;
            while (l1 < k)
            {
                while (l2 < k)
                {
                    for (int i = l1 * (image1.Width / k); i < (l1 + 1) * (image1.Width / k); i++)
                    {
                        for (int j = l2 * (image1.Height / k); j < (l2 + 1) * (image1.Height / k); j++)
                        {
                            i1.SetPixel(i % k, j % k, image1.GetPixel(i, j));
                            i2.SetPixel(i % k, j % k, image2.GetPixel(i, j));
                        }
                    }
                    l2++;
                    uqim += Work(i1, i2);
                }
                l2 = 0;
                l1++;
            }
            return uqim / (k * k);
        }
    }
}

