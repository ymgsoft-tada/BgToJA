
namespace App
{
	partial class FormMain
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.iCodeMoto = new GControlGcTextBoxEx.GcTextBoxEx();
			this.iCodeKen = new GControlGcTextBoxEx.GcTextBoxEx();
			this.btnExec = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.ycLabelEx4 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.linkSetting = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.iCodeMoto)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iCodeKen)).BeginInit();
			this.SuspendLayout();
			// 
			// iCodeMoto
			// 
			this.iCodeMoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iCodeMoto.ExFocusHighlight = true;
			this.iCodeMoto.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iCodeMoto.ImeStr = "";
			this.iCodeMoto.Location = new System.Drawing.Point(153, 128);
			this.iCodeMoto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.iCodeMoto.Name = "iCodeMoto";
			this.iCodeMoto.SingleBorderColor = System.Drawing.Color.Silver;
			this.iCodeMoto.Size = new System.Drawing.Size(150, 27);
			this.iCodeMoto.TabIndex = 1;
			// 
			// iCodeKen
			// 
			this.iCodeKen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iCodeKen.ExFocusHighlight = true;
			this.iCodeKen.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iCodeKen.ImeStr = "";
			this.iCodeKen.Location = new System.Drawing.Point(153, 93);
			this.iCodeKen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.iCodeKen.Name = "iCodeKen";
			this.iCodeKen.SingleBorderColor = System.Drawing.Color.Silver;
			this.iCodeKen.Size = new System.Drawing.Size(150, 27);
			this.iCodeKen.TabIndex = 0;
			// 
			// btnExec
			// 
			this.btnExec.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.btnExec.Location = new System.Drawing.Point(153, 201);
			this.btnExec.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnExec.Name = "btnExec";
			this.btnExec.Size = new System.Drawing.Size(106, 42);
			this.btnExec.TabIndex = 2;
			this.btnExec.Text = "実行";
			this.btnExec.UseVisualStyleBackColor = true;
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.btnClose.Location = new System.Drawing.Point(299, 201);
			this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(87, 42);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "終了";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// ycLabelEx4
			// 
			this.ycLabelEx4.BackColor = System.Drawing.Color.CadetBlue;
			this.ycLabelEx4.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx4.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx4.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ycLabelEx4.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx4.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.IconImage = null;
			this.ycLabelEx4.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx4.Location = new System.Drawing.Point(49, 128);
			this.ycLabelEx4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ycLabelEx4.Name = "ycLabelEx4";
			this.ycLabelEx4.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.Size = new System.Drawing.Size(97, 27);
			this.ycLabelEx4.TabIndex = 7;
			this.ycLabelEx4.Text = "出荷元コード";
			this.ycLabelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ycLabelEx3
			// 
			this.ycLabelEx3.BackColor = System.Drawing.Color.CadetBlue;
			this.ycLabelEx3.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx3.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx3.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ycLabelEx3.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx3.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.IconImage = null;
			this.ycLabelEx3.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx3.Location = new System.Drawing.Point(49, 93);
			this.ycLabelEx3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(97, 27);
			this.ycLabelEx3.TabIndex = 6;
			this.ycLabelEx3.Text = " 県コード";
			this.ycLabelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ycLabelEx2
			// 
			this.ycLabelEx2.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx2.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ycLabelEx2.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.IconImage = null;
			this.ycLabelEx2.Location = new System.Drawing.Point(49, 36);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(243, 27);
			this.ycLabelEx2.TabIndex = 5;
			this.ycLabelEx2.Text = "全農送信用　売上データ変換";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.CadetBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(30, 32);
			this.ycLabelEx1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(11, 34);
			this.ycLabelEx1.TabIndex = 4;
			// 
			// linkSetting
			// 
			this.linkSetting.AutoSize = true;
			this.linkSetting.LinkColor = System.Drawing.Color.DodgerBlue;
			this.linkSetting.Location = new System.Drawing.Point(318, 9);
			this.linkSetting.Name = "linkSetting";
			this.linkSetting.Size = new System.Drawing.Size(87, 20);
			this.linkSetting.TabIndex = 8;
			this.linkSetting.TabStop = true;
			this.linkSetting.Text = "システム設定";
			this.linkSetting.VisitedLinkColor = System.Drawing.Color.DodgerBlue;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(417, 264);
			this.Controls.Add(this.linkSetting);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnExec);
			this.Controls.Add(this.iCodeMoto);
			this.Controls.Add(this.iCodeKen);
			this.Controls.Add(this.ycLabelEx4);
			this.Controls.Add(this.ycLabelEx3);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.ycLabelEx1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.iCodeMoto)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iCodeKen)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private YControlLabelEx.YcLabelEx ycLabelEx4;
		private GControlGcTextBoxEx.GcTextBoxEx iCodeKen;
		private GControlGcTextBoxEx.GcTextBoxEx iCodeMoto;
		private System.Windows.Forms.Button btnExec;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.LinkLabel linkSetting;
	}
}

