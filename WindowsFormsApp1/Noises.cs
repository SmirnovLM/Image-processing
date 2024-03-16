using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Noises
    {
        private Random random = new Random();
      
       

        
        public Bitmap GammaNoise(Bitmap image, double share, double scale, double noisePersentage)
        {
            double[] distribution = new double[256]; // Распределение
            double sum = 0; 

            Bitmap resultImage = new Bitmap(image);

            for (int i = 0; i < distribution.Length; i++)
            {
                distribution[i] = GammaDistibution(i, share, scale);
                sum += distribution[i];
            }
            for (int i = 0; i < distribution.Length; i++)
            {
                distribution[i] = distribution[i] / sum;
            }

            int imageSize = image.Width * image.Height;
            int countOfPixels = (int)(imageSize * noisePersentage);

            while (countOfPixels-- > 0)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);

                Color sourceColor = image.GetPixel(x, y);
                Color noiseColor = SelectRandomColor(sourceColor, distribution);
                resultImage.SetPixel(x, y, noiseColor);
            }
            return resultImage;
        }

        public double[] ComputeGammaValues(double share, double scale)
        {
            double[] distribition = new double[256];
            double sum = 0;

            for (int i = 0; i < distribition.Length; i++)
            {
                distribition[i] = GammaDistibution(i, share, scale);
                sum += distribition[i];
            }

            for (int i = 0; i < distribition.Length; i++)
            {
                distribition[i] /= sum;
            }

            return distribition;
        }

        private Color SelectRandomColor(Color color, double[] distribution)
        {
            double randomValue = random.NextDouble();
            double cumulativeProbability = 0.0;
            for (int colorValue = 0; colorValue < 256; colorValue++)
            {
                cumulativeProbability += distribution[colorValue];
                if (randomValue <= cumulativeProbability)
                {
                    return Color.FromArgb(
                        Clamp(color.R + colorValue), 
                        Clamp(color.G + colorValue), 
                        Clamp(color.B + colorValue)
                        );
                }
            }
            return Color.White;
        }

        private double GammaDistibution(int x, double alpha, double beta)
        {
            return (Math.Pow(beta, alpha) * Math.Pow(x, alpha - 1) * Math.Exp(-beta * x)) / Factorial(alpha - 1);
        }
        
        private long Factorial(double x)
        {
            long tmp = 1;
            for (int i = 2; i <= x; i++)
            {
                tmp *= i;
            }
            return tmp;
        }

        private int Clamp(int value, int min = 0, int max = 255)
        {
            return Math.Min(max, Math.Max(min, value));
        }
    }
}
