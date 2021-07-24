namespace TheKingOfFighters
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.timerEnter = new System.Windows.Forms.Timer(this.components);
            this.timerPlayerMove = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.timerDetection = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.MediaPlayer2 = new AxWMPLib.AxWindowsMediaPlayer();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.progressBar4 = new System.Windows.Forms.ProgressBar();
            this.pbPlayer1 = new System.Windows.Forms.PictureBox();
            this.pbPlayer2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer2)).BeginInit();
            this.SuspendLayout();
            // 
            // timerEnter
            // 
            this.timerEnter.Interval = 280;
            this.timerEnter.Tick += new System.EventHandler(this.timerEnter_Tick);
            // 
            // timerPlayerMove
            // 
            this.timerPlayerMove.Interval = 5;
            this.timerPlayerMove.Tick += new System.EventHandler(this.timerPlayerMove_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::TheKingOfFighters.Properties.Resources.背景图1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(884, 461);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // MediaPlayer1
            // 
            this.MediaPlayer1.Enabled = true;
            this.MediaPlayer1.Location = new System.Drawing.Point(0, 52);
            this.MediaPlayer1.Name = "MediaPlayer1";
            this.MediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer1.OcxState")));
            this.MediaPlayer1.Size = new System.Drawing.Size(59, 34);
            this.MediaPlayer1.TabIndex = 3;
            this.MediaPlayer1.Visible = false;
            this.MediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.MediaPlayer1_PlayStateChange);
            this.MediaPlayer1.KeyDownEvent += new AxWMPLib._WMPOCXEvents_KeyDownEventHandler(this.MediaPlayer1_KeyDownEvent);
            // 
            // timerDetection
            // 
            this.timerDetection.Interval = 20;
            this.timerDetection.Tick += new System.EventHandler(this.timerDetection_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Red;
            this.progressBar1.Location = new System.Drawing.Point(51, 23);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(261, 20);
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // progressBar2
            // 
            this.progressBar2.BackColor = System.Drawing.Color.Red;
            this.progressBar2.Location = new System.Drawing.Point(569, 23);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(261, 20);
            this.progressBar2.TabIndex = 4;
            this.progressBar2.Visible = false;
            // 
            // MediaPlayer2
            // 
            this.MediaPlayer2.Enabled = true;
            this.MediaPlayer2.Location = new System.Drawing.Point(12, 391);
            this.MediaPlayer2.Name = "MediaPlayer2";
            this.MediaPlayer2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer2.OcxState")));
            this.MediaPlayer2.Size = new System.Drawing.Size(203, 58);
            this.MediaPlayer2.TabIndex = 5;
            this.MediaPlayer2.Visible = false;
            this.MediaPlayer2.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.MediaPlayer2_PlayStateChange);
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(51, 43);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(261, 12);
            this.progressBar3.TabIndex = 6;
            this.progressBar3.Visible = false;
            // 
            // progressBar4
            // 
            this.progressBar4.Location = new System.Drawing.Point(569, 43);
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(261, 12);
            this.progressBar4.TabIndex = 6;
            this.progressBar4.Visible = false;
            // 
            // pbPlayer1
            // 
            this.pbPlayer1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbPlayer1.BackgroundImage")));
            this.pbPlayer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbPlayer1.Location = new System.Drawing.Point(19, 23);
            this.pbPlayer1.Name = "pbPlayer1";
            this.pbPlayer1.Size = new System.Drawing.Size(32, 32);
            this.pbPlayer1.TabIndex = 7;
            this.pbPlayer1.TabStop = false;
            this.pbPlayer1.Visible = false;
            // 
            // pbPlayer2
            // 
            this.pbPlayer2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbPlayer2.BackgroundImage")));
            this.pbPlayer2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbPlayer2.Location = new System.Drawing.Point(830, 23);
            this.pbPlayer2.Name = "pbPlayer2";
            this.pbPlayer2.Size = new System.Drawing.Size(32, 32);
            this.pbPlayer2.TabIndex = 7;
            this.pbPlayer2.TabStop = false;
            this.pbPlayer2.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.pbPlayer2);
            this.Controls.Add(this.pbPlayer1);
            this.Controls.Add(this.progressBar4);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.MediaPlayer2);
            this.Controls.Add(this.MediaPlayer1);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拳皇";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timerEnter;
        private System.Windows.Forms.Timer timerPlayerMove;
        private AxWMPLib.AxWindowsMediaPlayer MediaPlayer1;
        private System.Windows.Forms.Timer timerDetection;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private AxWMPLib.AxWindowsMediaPlayer MediaPlayer2;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.ProgressBar progressBar4;
        private System.Windows.Forms.PictureBox pbPlayer1;
        private System.Windows.Forms.PictureBox pbPlayer2;
    }
}

