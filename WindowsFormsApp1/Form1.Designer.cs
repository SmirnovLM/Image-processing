namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ForwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeNoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gammaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noiseRemovelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.midPointFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operatorCannyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.momentsOfImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fourierTransformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lowPassFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highPassFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Location = new System.Drawing.Point(12, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.makeNoiseToolStripMenuItem,
            this.noiseRemovelToolStripMenuItem,
            this.operatorCannyToolStripMenuItem,
            this.momentsOfImageToolStripMenuItem,
            this.fourierTransformToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1331, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compareToolStripMenuItem,
            this.BackToolStripMenuItem,
            this.ForwardToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("compareToolStripMenuItem.Image")));
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.compareToolStripMenuItem.Text = "Compare";
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // BackToolStripMenuItem
            // 
            this.BackToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("BackToolStripMenuItem.Image")));
            this.BackToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BackToolStripMenuItem.Name = "BackToolStripMenuItem";
            this.BackToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.BackToolStripMenuItem.Text = "Back";
            this.BackToolStripMenuItem.Click += new System.EventHandler(this.BackToolStripMenuItem_Click);
            // 
            // ForwardToolStripMenuItem
            // 
            this.ForwardToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ForwardToolStripMenuItem.Image")));
            this.ForwardToolStripMenuItem.Name = "ForwardToolStripMenuItem";
            this.ForwardToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.ForwardToolStripMenuItem.Text = "Forward";
            this.ForwardToolStripMenuItem.Click += new System.EventHandler(this.ForwardToolStripMenuItem_Click);
            // 
            // makeNoiseToolStripMenuItem
            // 
            this.makeNoiseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gammaToolStripMenuItem});
            this.makeNoiseToolStripMenuItem.Name = "makeNoiseToolStripMenuItem";
            this.makeNoiseToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.makeNoiseToolStripMenuItem.Text = "Make Noise";
            // 
            // gammaToolStripMenuItem
            // 
            this.gammaToolStripMenuItem.Name = "gammaToolStripMenuItem";
            this.gammaToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.gammaToolStripMenuItem.Text = "Gamma";
            this.gammaToolStripMenuItem.Click += new System.EventHandler(this.gammaToolStripMenuItem_Click);
            // 
            // noiseRemovelToolStripMenuItem
            // 
            this.noiseRemovelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.midPointFilterToolStripMenuItem,
            this.medianFilterToolStripMenuItem});
            this.noiseRemovelToolStripMenuItem.Name = "noiseRemovelToolStripMenuItem";
            this.noiseRemovelToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.noiseRemovelToolStripMenuItem.Text = "Noise Removel";
            // 
            // midPointFilterToolStripMenuItem
            // 
            this.midPointFilterToolStripMenuItem.Name = "midPointFilterToolStripMenuItem";
            this.midPointFilterToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.midPointFilterToolStripMenuItem.Text = "MidPoint Filter";
            this.midPointFilterToolStripMenuItem.Click += new System.EventHandler(this.midPointFilterToolStripMenuItem_Click);
            // 
            // medianFilterToolStripMenuItem
            // 
            this.medianFilterToolStripMenuItem.Name = "medianFilterToolStripMenuItem";
            this.medianFilterToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.medianFilterToolStripMenuItem.Text = "Median Filter";
            this.medianFilterToolStripMenuItem.Click += new System.EventHandler(this.medianFilterToolStripMenuItem_Click);
            // 
            // operatorCannyToolStripMenuItem
            // 
            this.operatorCannyToolStripMenuItem.Name = "operatorCannyToolStripMenuItem";
            this.operatorCannyToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.operatorCannyToolStripMenuItem.Text = "Operator Canny";
            this.operatorCannyToolStripMenuItem.Click += new System.EventHandler(this.operatorCannyToolStripMenuItem_Click);
            // 
            // momentsOfImageToolStripMenuItem
            // 
            this.momentsOfImageToolStripMenuItem.Name = "momentsOfImageToolStripMenuItem";
            this.momentsOfImageToolStripMenuItem.Size = new System.Drawing.Size(149, 24);
            this.momentsOfImageToolStripMenuItem.Text = "Moments of Image";
            this.momentsOfImageToolStripMenuItem.Click += new System.EventHandler(this.momentsOfImageToolStripMenuItem_Click);
            // 
            // fourierTransformToolStripMenuItem
            // 
            this.fourierTransformToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lowPassFilterToolStripMenuItem,
            this.highPassFilterToolStripMenuItem});
            this.fourierTransformToolStripMenuItem.Name = "fourierTransformToolStripMenuItem";
            this.fourierTransformToolStripMenuItem.Size = new System.Drawing.Size(139, 24);
            this.fourierTransformToolStripMenuItem.Text = "Fourier Transform";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 672);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(800, 56);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(245, 644);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Текущее(шумовое) значение: 0 %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(835, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 13;
            // 
            // lowPassFilterToolStripMenuItem
            // 
            this.lowPassFilterToolStripMenuItem.Name = "lowPassFilterToolStripMenuItem";
            this.lowPassFilterToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.lowPassFilterToolStripMenuItem.Text = "Low Pass Filter";
            this.lowPassFilterToolStripMenuItem.Click += new System.EventHandler(this.lowPassFilterToolStripMenuItem_Click);
            // 
            // highPassFilterToolStripMenuItem
            // 
            this.highPassFilterToolStripMenuItem.Name = "highPassFilterToolStripMenuItem";
            this.highPassFilterToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.highPassFilterToolStripMenuItem.Text = "High Pass Filter";
            this.highPassFilterToolStripMenuItem.Click += new System.EventHandler(this.highPassFilterToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.ClientSize = new System.Drawing.Size(1331, 802);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeNoiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gammaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noiseRemovelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem midPointFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianFilterToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem BackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ForwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operatorCannyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem momentsOfImageToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem fourierTransformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowPassFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highPassFilterToolStripMenuItem;
    }
}

