using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace WindowsFormsApp1
{
    public struct ObjectProperties
    {
        public double square;        // 1) Площадь:
        public double centroidX;     // 2.1) Координата Х центра масс объекта:
        public double centroidY;     // 2.2) Координата У центра масс объекта:
        public double orientation;   // 3) Ориентация:
        public double scale;         // 4) Масштаб:
        public double momentInertia; // 5) Момент Инерции - вращательное движение объекта:
        public double eccentricity;  // 6) Эксцентриситет - степень вытянутости объекта:

        public ObjectProperties(double square, double centroidX, double centroidY, 
            double orientation, double scale, double momentInertia, double eccentricity)
        {
            this.square = square;
            this.centroidX = centroidX;
            this.centroidY = centroidY;
            this.orientation = orientation;
            this.scale = scale;
            this.momentInertia = momentInertia;
            this.eccentricity = eccentricity;
        }
    }

    class MomentsOfImage
    {
        private Bitmap image;
        private int[,] matrixImage;

        private List<int[,]> objects;
        public List<ObjectProperties> objectProperties;

        public MomentsOfImage(Bitmap sourceimage)
        {
            image = sourceimage;
            matrixImage = new int[image.Width, image.Height];

            objects = new List<int[,]>();
            objectProperties = new List<ObjectProperties>();

            // Разделение и Маркировка объектов
            ConvertToMatrix();
            MarkingConnectedComponents();
            SelectionOfIndividualObjects();

            // Анализ каждого полученного объекта
            AnalyzeObjectsProperties();
        }

        // Разделение и Маркировка объектов
        private void ConvertToMatrix()
        {
            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    int color = image.GetPixel(x, y).R;
                    if (color > 127)
                    {
                        matrixImage[x, y] = 255;
                    }
                }
            }
        }
        private void MarkingConnectedComponents()
        {
            int currentLabel = 0;

            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    if (matrixImage[x, y] == 255)
                    {
                        currentLabel++;
                        LabelComponentRecursive(x, y, currentLabel);
                    }
                }
            }
        }
        private void LabelComponentRecursive(int x, int y, int label)
        {
            var div = new int[2, 4] { {0, -1, 0, 1}, {-1, 0, 1, 0} };
            if (x < 0 || x >= image.Width || y < 0 || y >= image.Height)
            {
                return;
            }
            else if (matrixImage[x, y] != 255)
            {
                return;
            }
            else if (matrixImage[x, y] == label)
            {
                return;
            }
            else
            {
                for (int k = 0; k < 4; k++)
                {
                    matrixImage[x, y] = label;
                    LabelComponentRecursive(x + div[0, k], y + div[1, k], label);
                }
            }
        }

        private void SelectionOfIndividualObjects()
        {
            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    var mark = matrixImage[x, y];
                    if (mark > 0)
                    {
                        if (mark > objects.Count)
                        {
                            objects.Add(new int[image.Width, image.Height]);
                        }
                        objects[mark - 1][x, y] = 1;
                    }
                }
            }
        }


        // Анализ каждого полученного объекта
        private void AnalyzeObjectsProperties()
        {
            foreach (var obj in objects)
            {
                double m00 = RawMoment(obj, 0, 0);
                double m10 = RawMoment(obj, 1, 0);
                double m01 = RawMoment(obj, 0, 1);

                double μ00 = CentralMoment(obj, 0, 0);
                double μ11 = CentralMoment(obj, 1, 1);
                double μ20 = CentralMoment(obj, 2, 0);
                double μ02 = CentralMoment(obj, 0, 2);

                double square = m00;
                double centroidX = m10 / m00; 
                double centroidY = m01 / m00;
                double orientation = 1 / 2 * Math.Atan2(2 * μ11, μ20 - μ02);
                double scale = Math.Sqrt((μ20 + μ02) / μ00);
                double momentInertia = μ20 + μ02;
                double eccentricity = Math.Sqrt(μ20 * μ20 + μ02 * μ02 - 2 * μ20 * μ02) / (μ20 + μ02);

                objectProperties.Add(
                    new ObjectProperties(square, centroidX, centroidY, orientation, scale, momentInertia, eccentricity));
            }
        }

        // Сырые Моменты:
        private double RawMoment(int[,] obj, int p, int q)
        {
            double rawMoment = 0.0;
            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    if (obj[x, y] == 1)
                    {
                        rawMoment += Math.Pow(x, p) * Math.Pow(y, q);
                    }
                }
            }
            return rawMoment;
        }

        // Центральные Моменты:
        private double CentralMoment(int[,] obj, int p, int q)
        {
            double centralMoment = 0.0;

            // Координаты центра масс
            double centralMasX = RawMoment(obj, 1, 0) / RawMoment(obj, 0, 0);
            double centralMasY = RawMoment(obj, 0, 1) / RawMoment(obj, 0, 0);

            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    if (obj[x, y] == 1)
                    {
                        centralMoment += Math.Pow(x - centralMasX, p) * Math.Pow(y - centralMasY, q) * obj[x, y];
                    }
                }
            }
            return centralMoment;
        }
    }
}
