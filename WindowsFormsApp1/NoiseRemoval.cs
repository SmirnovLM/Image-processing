using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    enum FilterNoiseRamovalName
    {
        MidPointFilter, MedianFilter
    }

    class NoiseRemoval
    {
        public Bitmap MidPointFilter(Bitmap sourceImage, int size, FilterNoiseRamovalName filterName)
        {
            Bitmap result = new Bitmap(sourceImage.Width, sourceImage.Height);
            int newSize = size / 2;

            for (int i = newSize; i < sourceImage.Width - newSize; i++)
            {
                for (int j = newSize; j < sourceImage.Height - newSize; j++)
                {
                    Color[] pixels = CalculatingTheNewPixelValue(sourceImage, i, j, size, newSize);

                    int[] redValues = new int[pixels.Length];
                    int[] greenValues = new int[pixels.Length];
                    int[] blueValues = new int[pixels.Length];

                    for (int k = 0; k < pixels.Length; k++)
                    {
                        redValues[k] = pixels[k].R;
                        greenValues[k] = pixels[k].G;
                        blueValues[k] = pixels[k].B;
                    }

                    Array.Sort(redValues);
                    Array.Sort(greenValues);
                    Array.Sort(blueValues);

                    switch (filterName)
                    {
                        case FilterNoiseRamovalName.MidPointFilter:
                            {
                                int R = 0; int G = 0; int B = 0;
                                R = (int)(((int)redValues[0] + redValues[redValues.Length - 1]) / 2);
                                G = (int)(((int)greenValues[0] + greenValues[greenValues.Length - 1]) / 2);
                                B = (int)(((int)blueValues[0] + blueValues[blueValues.Length - 1]) / 2);
                                Color newColor = Color.FromArgb(R, G, B);
                                result.SetPixel(i, j, newColor);
                            }
                            break;
                        case FilterNoiseRamovalName.MedianFilter:
                            {
                                int medianIndex = pixels.Length / 2;
                                Color medianColor = Color.FromArgb(redValues[medianIndex], greenValues[medianIndex], blueValues[medianIndex]);
                                result.SetPixel(i, j, medianColor);
                            }
                            break;
                    }
                }
            }
            return result;
        }

        private Color[] CalculatingTheNewPixelValue(Bitmap sourceImage, int x, int y, int size, int newSize)
        {
            int currentIndex = 0;
            Color[] pixels = new Color[size * size];

            for (int i = -newSize; i <= newSize; i++)
            {
                for (int j = -newSize; j <= newSize; j++)
                {
                    int newX = x + i;
                    int newY = y + j;
                    if (newX >= 0 && newX < sourceImage.Width && newY >= 0 && newY < sourceImage.Height)
                    {
                        pixels[currentIndex++] = sourceImage.GetPixel(newX, newY);
                    }
                }
            }
            return pixels;
        }

    }
}

