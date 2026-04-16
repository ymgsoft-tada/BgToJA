namespace App
{
	partial class FormBg_Progress
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBg_Progress));
			this.LblInfo = new System.Windows.Forms.Label();
			this.bgWorker = new System.ComponentModel.BackgroundWorker();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.IconIndicator = new System.Windows.Forms.PictureBox();
			this.Picture = new System.Windows.Forms.PictureBox();
			this.img0 = new System.Windows.Forms.PictureBox();
			this.img1 = new System.Windows.Forms.PictureBox();
			this.img2 = new System.Windows.Forms.PictureBox();
			this.img3 = new System.Windows.Forms.PictureBox();
			this.img4 = new System.Windows.Forms.PictureBox();
			this.img5 = new System.Windows.Forms.PictureBox();
			this.img6 = new System.Windows.Forms.PictureBox();
			this.img7 = new System.Windows.Forms.PictureBox();
			this.img8 = new System.Windows.Forms.PictureBox();
			this.img9 = new System.Windows.Forms.PictureBox();
			this.img10 = new System.Windows.Forms.PictureBox();
			this.img11 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.IconIndicator)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img0)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.img11)).BeginInit();
			this.SuspendLayout();
			// 
			// LblInfo
			// 
			this.LblInfo.Location = new System.Drawing.Point(76, 13);
			this.LblInfo.Name = "LblInfo";
			this.LblInfo.Size = new System.Drawing.Size(235, 20);
			this.LblInfo.TabIndex = 3;
			this.LblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// bgWorker
			// 
			this.bgWorker.WorkerReportsProgress = true;
			this.bgWorker.WorkerSupportsCancellation = true;
			this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
			this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			// 
			// timer
			// 
			this.timer.Interval = 80;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(76, 47);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(235, 12);
			this.ProgressBar.TabIndex = 6;
			// 
			// IconIndicator
			// 
			this.IconIndicator.BackColor = System.Drawing.Color.Transparent;
			this.IconIndicator.Image = ((System.Drawing.Image)(resources.GetObject("IconIndicator.Image")));
			this.IconIndicator.Location = new System.Drawing.Point(9, 14);
			this.IconIndicator.Margin = new System.Windows.Forms.Padding(4);
			this.IconIndicator.Name = "IconIndicator";
			this.IconIndicator.Size = new System.Drawing.Size(50, 50);
			this.IconIndicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.IconIndicator.TabIndex = 7;
			this.IconIndicator.TabStop = false;
			// 
			// Picture
			// 
			this.Picture.Location = new System.Drawing.Point(0, 0);
			this.Picture.Name = "Picture";
			this.Picture.Size = new System.Drawing.Size(322, 78);
			this.Picture.TabIndex = 9;
			this.Picture.TabStop = false;
			// 
			// img0
			// 
			this.img0.BackColor = System.Drawing.Color.Transparent;
			this.img0.Image = ((System.Drawing.Image)(resources.GetObject("img0.Image")));
			this.img0.Location = new System.Drawing.Point(-100, -100);
			this.img0.Margin = new System.Windows.Forms.Padding(4);
			this.img0.Name = "img0";
			this.img0.Size = new System.Drawing.Size(60, 60);
			this.img0.TabIndex = 10;
			this.img0.TabStop = false;
			this.img0.Visible = false;
			// 
			// img1
			// 
			this.img1.BackColor = System.Drawing.Color.Transparent;
			this.img1.Image = ((System.Drawing.Image)(resources.GetObject("img1.Image")));
			this.img1.Location = new System.Drawing.Point(-100, -100);
			this.img1.Margin = new System.Windows.Forms.Padding(4);
			this.img1.Name = "img1";
			this.img1.Size = new System.Drawing.Size(60, 60);
			this.img1.TabIndex = 11;
			this.img1.TabStop = false;
			this.img1.Visible = false;
			// 
			// img2
			// 
			this.img2.BackColor = System.Drawing.Color.Transparent;
			this.img2.Image = ((System.Drawing.Image)(resources.GetObject("img2.Image")));
			this.img2.Location = new System.Drawing.Point(-100, -100);
			this.img2.Margin = new System.Windows.Forms.Padding(4);
			this.img2.Name = "img2";
			this.img2.Size = new System.Drawing.Size(60, 60);
			this.img2.TabIndex = 12;
			this.img2.TabStop = false;
			this.img2.Visible = false;
			// 
			// img3
			// 
			this.img3.BackColor = System.Drawing.Color.Transparent;
			this.img3.Image = ((System.Drawing.Image)(resources.GetObject("img3.Image")));
			this.img3.Location = new System.Drawing.Point(-100, -100);
			this.img3.Margin = new System.Windows.Forms.Padding(4);
			this.img3.Name = "img3";
			this.img3.Size = new System.Drawing.Size(60, 60);
			this.img3.TabIndex = 13;
			this.img3.TabStop = false;
			this.img3.Visible = false;
			// 
			// img4
			// 
			this.img4.BackColor = System.Drawing.Color.Transparent;
			this.img4.Image = ((System.Drawing.Image)(resources.GetObject("img4.Image")));
			this.img4.Location = new System.Drawing.Point(-100, -100);
			this.img4.Margin = new System.Windows.Forms.Padding(4);
			this.img4.Name = "img4";
			this.img4.Size = new System.Drawing.Size(60, 60);
			this.img4.TabIndex = 14;
			this.img4.TabStop = false;
			this.img4.Visible = false;
			// 
			// img5
			// 
			this.img5.BackColor = System.Drawing.Color.Transparent;
			this.img5.Image = ((System.Drawing.Image)(resources.GetObject("img5.Image")));
			this.img5.Location = new System.Drawing.Point(-100, -100);
			this.img5.Margin = new System.Windows.Forms.Padding(4);
			this.img5.Name = "img5";
			this.img5.Size = new System.Drawing.Size(60, 60);
			this.img5.TabIndex = 15;
			this.img5.TabStop = false;
			this.img5.Visible = false;
			// 
			// img6
			// 
			this.img6.BackColor = System.Drawing.Color.Transparent;
			this.img6.Image = ((System.Drawing.Image)(resources.GetObject("img6.Image")));
			this.img6.Location = new System.Drawing.Point(-100, -100);
			this.img6.Margin = new System.Windows.Forms.Padding(4);
			this.img6.Name = "img6";
			this.img6.Size = new System.Drawing.Size(60, 60);
			this.img6.TabIndex = 16;
			this.img6.TabStop = false;
			this.img6.Visible = false;
			// 
			// img7
			// 
			this.img7.BackColor = System.Drawing.Color.Transparent;
			this.img7.Image = ((System.Drawing.Image)(resources.GetObject("img7.Image")));
			this.img7.Location = new System.Drawing.Point(-100, -100);
			this.img7.Margin = new System.Windows.Forms.Padding(4);
			this.img7.Name = "img7";
			this.img7.Size = new System.Drawing.Size(60, 60);
			this.img7.TabIndex = 17;
			this.img7.TabStop = false;
			this.img7.Visible = false;
			// 
			// img8
			// 
			this.img8.BackColor = System.Drawing.Color.Transparent;
			this.img8.Image = ((System.Drawing.Image)(resources.GetObject("img8.Image")));
			this.img8.Location = new System.Drawing.Point(-100, -100);
			this.img8.Margin = new System.Windows.Forms.Padding(4);
			this.img8.Name = "img8";
			this.img8.Size = new System.Drawing.Size(60, 60);
			this.img8.TabIndex = 18;
			this.img8.TabStop = false;
			this.img8.Visible = false;
			// 
			// img9
			// 
			this.img9.BackColor = System.Drawing.Color.Transparent;
			this.img9.Image = ((System.Drawing.Image)(resources.GetObject("img9.Image")));
			this.img9.Location = new System.Drawing.Point(-100, -100);
			this.img9.Margin = new System.Windows.Forms.Padding(4);
			this.img9.Name = "img9";
			this.img9.Size = new System.Drawing.Size(60, 60);
			this.img9.TabIndex = 19;
			this.img9.TabStop = false;
			this.img9.Visible = false;
			// 
			// img10
			// 
			this.img10.BackColor = System.Drawing.Color.Transparent;
			this.img10.Image = ((System.Drawing.Image)(resources.GetObject("img10.Image")));
			this.img10.Location = new System.Drawing.Point(-100, -100);
			this.img10.Margin = new System.Windows.Forms.Padding(4);
			this.img10.Name = "img10";
			this.img10.Size = new System.Drawing.Size(60, 60);
			this.img10.TabIndex = 20;
			this.img10.TabStop = false;
			this.img10.Visible = false;
			// 
			// img11
			// 
			this.img11.BackColor = System.Drawing.Color.Transparent;
			this.img11.Image = ((System.Drawing.Image)(resources.GetObject("img11.Image")));
			this.img11.Location = new System.Drawing.Point(-100, -100);
			this.img11.Margin = new System.Windows.Forms.Padding(4);
			this.img11.Name = "img11";
			this.img11.Size = new System.Drawing.Size(60, 60);
			this.img11.TabIndex = 21;
			this.img11.TabStop = false;
			this.img11.Visible = false;
			// 
			// FormBg_Progress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(324, 79);
			this.Controls.Add(this.img11);
			this.Controls.Add(this.img10);
			this.Controls.Add(this.img9);
			this.Controls.Add(this.img8);
			this.Controls.Add(this.img7);
			this.Controls.Add(this.img6);
			this.Controls.Add(this.img5);
			this.Controls.Add(this.img4);
			this.Controls.Add(this.img3);
			this.Controls.Add(this.img2);
			this.Controls.Add(this.img1);
			this.Controls.Add(this.img0);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.LblInfo);
			this.Controls.Add(this.IconIndicator);
			this.Controls.Add(this.Picture);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBg_Progress";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "しばらくお待ちください";
			((System.ComponentModel.ISupportInitialize)(this.IconIndicator)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img0)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.img11)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// バックグウンドタスク実行コントロール
		/// </summary>
		protected System.ComponentModel.BackgroundWorker bgWorker;
		private System.Windows.Forms.Timer timer;
		/// <summary>
		/// 
		/// </summary>
		public System.Windows.Forms.Label LblInfo;
		/// <summary>
		/// 
		/// </summary>
		public System.Windows.Forms.ProgressBar ProgressBar;
		/// <summary>
		/// 
		/// </summary>
		public System.Windows.Forms.PictureBox IconIndicator;
		/// <summary>
		/// 
		/// </summary>
		public System.Windows.Forms.PictureBox Picture;
		private System.Windows.Forms.PictureBox img0;
		private System.Windows.Forms.PictureBox img1;
		private System.Windows.Forms.PictureBox img2;
		private System.Windows.Forms.PictureBox img3;
		private System.Windows.Forms.PictureBox img4;
		private System.Windows.Forms.PictureBox img5;
		private System.Windows.Forms.PictureBox img6;
		private System.Windows.Forms.PictureBox img7;
		private System.Windows.Forms.PictureBox img8;
		private System.Windows.Forms.PictureBox img9;
		private System.Windows.Forms.PictureBox img10;
		private System.Windows.Forms.PictureBox img11;
	}
}