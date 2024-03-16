using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Bitmap> imageList = new List<Bitmap>();
        int currentIndex;
        
        public Form1()
        {
            InitializeComponent();
            currentIndex = -1;
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            imageList.Clear();
            currentIndex = 0;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
               imageList.Add(new Bitmap(dialog.FileName));
            }
            currentIndex = imageList.Count - 1;
            pictureBox1.Image = imageList[currentIndex];
            pictureBox1.Refresh();
        }
        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filter1 = new Compare().processImage(imageList[0]);
            var filter2 = new Compare().processImage(imageList[currentIndex]);
            var mse_result = new MSE().Work(filter1, filter2);
            var uqi_result = new UQI().Work(filter1, filter2);
            var uqiM_result = new UQI().WorkM(filter1, filter2);
            var message = MessageBox.Show("MSE = " + mse_result.ToString() + "\n" +
                                          "UQI = " + uqi_result.ToString() + "\n" +
                                          "UQI(m) = " + uqiM_result.ToString(),
                                          " Сomparison result: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistogramForm histogramForm = new HistogramForm();
            Chart chart = histogramForm.chart1;
            ChartArea chartArea = chart.ChartAreas[0];
            chartArea.BackColor = Color.LightGray;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;

            Title title = new Title("Gamma distribition");
            title.Font = new Font("Arial", 14, FontStyle.Bold); 
            chart.Titles.Add(title);

            chartArea.AxisX.Title = "Gamma Values (share = " + 30 + ", scale = " + 1 + ")";
            chartArea.AxisX.TitleFont = new Font("Arial", 14, FontStyle.Bold);

            Noises noise = new Noises();
               
            imageList.Add(noise.GammaNoise(imageList[currentIndex++], 30, 1, (double)trackBar1.Value / 10));
            pictureBox1.Image = imageList[currentIndex];
            pictureBox1.Refresh();

            double[] ChannelHistogram = noise.ComputeGammaValues(30, 1);

            Series series = new Series("RedChannelHistogram");
            series.IsVisibleInLegend = false;
            series.ChartType = SeriesChartType.Column;
            for (int i = 0; i < ChannelHistogram.Length; i++)
            {
                series.Points.AddXY(i, ChannelHistogram[i]);
            }

            chart.Series.Clear();
            series.Palette = ChartColorPalette.Bright;
            chart.Series.Add(series);

            histogramForm.ShowDialog();
        }

        private void midPointFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoiseRemoval filter = new NoiseRemoval();
            imageList[++currentIndex] = filter.MidPointFilter(imageList[currentIndex], 5, FilterNoiseRamovalName.MidPointFilter);
            pictureBox1.Image = imageList[currentIndex];
            pictureBox1.Refresh();
        }
        private void medianFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoiseRemoval filter = new NoiseRemoval();
            imageList[++currentIndex] = filter.MidPointFilter(imageList[currentIndex], 5, FilterNoiseRamovalName.MedianFilter);
            pictureBox1.Image = imageList[currentIndex];
            pictureBox1.Refresh();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = String.Format("Текущее(шумовое) значение: {0} %", trackBar1.Value * 10);
        }

        private void ForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentIndex + 1 < imageList.Count)
            {
                pictureBox1.Image = imageList[++currentIndex];
                pictureBox1.Refresh();
            }
        }
        private void BackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                pictureBox1.Image = imageList[--currentIndex];
                pictureBox1.Refresh();
            }
        }

        private void operatorCannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TheMethodOfKenny filter = new TheMethodOfKenny();
            imageList.Add(filter.WorkTheMethodOfKenny(imageList[currentIndex++]));
            pictureBox1.Image = imageList[currentIndex];
            pictureBox1.Refresh();
        }
        private void momentsOfImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MomentsOfImage momentsOfImage = new MomentsOfImage(imageList[currentIndex]);
            label2.Text += "imageSize: Width = " + imageList[currentIndex].Width + ", Height = " + imageList[currentIndex].Height + "\n" + "\n";
            for (int k = 0; k < momentsOfImage.objectProperties.Count; k++)
            {
                label2.Text +=
                    "Object " + (k+1).ToString() + ":\n" +
                    "Square = " + momentsOfImage.objectProperties[k].square.ToString() + "\n" +
                    "centroidX = " + momentsOfImage.objectProperties[k].centroidX.ToString() + "\n" +
                    "centroidY = " + momentsOfImage.objectProperties[k].centroidY.ToString() + "\n" +
                    "orientation = " + momentsOfImage.objectProperties[k].orientation.ToString() + "\n" +
                    "scale = " + momentsOfImage.objectProperties[k].scale.ToString() + "\n" +
                    "momentInertia = " + momentsOfImage.objectProperties[k].momentInertia.ToString() + "\n" +
                    "eccentricity = " + momentsOfImage.objectProperties[k].eccentricity.ToString() + "\n" + "\n";
            }
        }

        private void highPassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FourierTransform f = new FourierTransform();
            imageList.Add(f.Work(imageList[currentIndex++], false, 10));
            pictureBox1.Image = imageList[currentIndex];
            pictureBox1.Refresh();
        }

        private void lowPassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FourierTransform f = new FourierTransform();
            imageList.Add(f.Work(imageList[currentIndex++], true, 100));
            pictureBox1.Image = imageList[currentIndex];
            pictureBox1.Refresh();
        }
    }
}
