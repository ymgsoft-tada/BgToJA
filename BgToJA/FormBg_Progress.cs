using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using ComponentDebug;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// バックグラウンドタスク用のフレームフォームです。
	/// </summary>
	public partial class FormBg_Progress : Form
	{
		#region *** Delegate ***
		/// <summary>
		/// フォームロード時に呼び出されます。
		/// </summary>
		public event EventHandler			LoadEvent;
		/// <summary>
		/// バックグラウンドで実行されるイベント発生時に呼び出されます。DoWorkEventかDoWorkEventExのどちらかを選択できます。
		/// </summary>
		public event DoWorkEventHandler		DoWorkEvent;
		/// <summary>
		/// バックグラウンドで実行されるイベント発生時に呼び出されます。DoWorkEventかDoWorkEventExのどちらかを選択できます。
		/// </summary>
		public event DoWorkEventHandlerEx	DoWorkEventEx;
		/// <summary>
		/// BgWorkerを使わず実行するかどうか。
		/// </summary>
		bool		nobgworker;
		#endregion
		
		#region *** Private Value ***
		/// <summary>
		/// 進捗％。
		/// </summary>
		int			progress_perc;
		/// <summary>
		/// DoWorkEventHandlerで受け取る引数。
		/// </summary>
		object		arg;
		/// <summary>
		/// DoWorkEventHandlerExで受け取る引数。
		/// </summary>
		object[]	args;
		/// <summary>
		/// アイコンパターンリスト
		/// </summary>
		Image[]		images;
		/// <summary>
		/// アイコン表示パターン
		/// </summary>
		int			iconPat;
		#endregion
		
		#region *** Property ***
		/// <summary>
		/// アイコン表示
		/// </summary>
		public bool		IconVisible
		{
			get
			{
				return IconIndicator.Visible;
			}
			set
			{
				IconIndicator.Visible = value;
				iconVisible	  = value;
			}
		}
		bool			iconVisible;
		/// <summary>
		/// BgWorkerを使わず実行するかどうかを設定します。
		/// </summary>
		public bool		NoBgWorker
		{
			get
			{
				return nobgworker;
			}
			set
			{
				nobgworker = value;
			}
		}
		/// <summary>
		/// DoWorkEventHandlerで受け取る引数を取得、または設定します。
		/// </summary>
		public object	Arg
		{
			get
			{
				return arg;
			}
			set
			{
				arg = value;
			}
		}
		/// <summary>
		/// DoWorkEventHandlerExで受け取る引数を取得、または設定します。
		/// </summary>
		public object[]	Args
		{
			get
			{
				return args;
			}
			set
			{
				args = value;
			}
		}
		/// <summary>
		/// フォームの幅を設定します。
		/// </summary>
		public int FormWidth
		{
			set
			{
				if (value < (324 - 235 + 32))
				{
					value = 324 - 235 + 32;
				}
				
				this.Width		  = value;
				Picture.Width	  = value - 8;
				LblInfo.Width	  =
				ProgressBar.Width = (235 - 324) + value;
			}
			get
			{
				return this.Width;
			}
			
		}
		/// <summary>
		/// タイトルに表示するテキストを設定します。
		/// </summary>
		public string TitleText
		{
			set
			{
				if (this.InvokeRequired == true)
				{
					this.Invoke(
						(MethodInvoker)delegate()
						{
							this.Text = value;
						}
					);
				}
				else
				{
					this.Text = value;
				}				
			}
			get
			{
				return this.Text;
			}
		}
		/// <summary>
		/// ラベルに表示するテキストを設定します。
		/// </summary>
		public string LabelText
		{
			set
			{
				if (this.InvokeRequired == true)
				{
					this.Invoke(
						(MethodInvoker)delegate()
						{
							LblInfo.Text = value;
						}
					);
				}
				else
				{
					LblInfo.Text = value;
				}
			}
			get
			{
				return LblInfo.Text;
			}
		}

		/// <summary>
		/// プログレスバーの表示有無を取得・設定します。
		/// </summary>
		public bool ProgressBarVisible
		{
			set
			{
				ProgressBar.Visible = value;
				
				if (value == false)
				{
					// ラベルを中央へ表示
					LblInfo.Height = 45;
				}
			}
			get
			{
				return ProgressBar.Visible;
			}
		}

		/// <summary>
		/// 処理結果の格納先
		/// </summary>
		public object Result { get; private set; }
		/// <summary>
		/// フォームの×ボタンを押せなくする対策。
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				const int CS_NOCLOSE = 0x200;
				CreateParams cp = base.CreateParams;
				cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
				
				return cp;
			}
		}
		#endregion
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormBg_Progress()
		{
			InitializeComponent();
			
			progress_perc = 0;
			arg			  = null;
			nobgworker	  = false;
			iconVisible	  = true;
			
			PictureBox[]	imgctls	  = new PictureBox[]
			{
				img0,
				img1,
				img2,
				img3,
				img4,
				img5,
				img6,
				img7,
				img8,
				img9,
				img10,
				img11,
			};
			
			images = new Image[imgctls.Length];
			
			for (int i = 0; i < images.Length; i++)
			{
				images[i] = imgctls[i].Image;
			}
			
			this.BackColor = AppConst.FormBackColor;
			
			Picture.Controls.Add(IconIndicator);
			Picture.Controls.Add(LblInfo);
			Picture.Controls.Add(ProgressBar);
			
			this.Load += new EventHandler(Form_Load);
			this.Shown += new EventHandler(Form_Shown);
		}
		#endregion
		
		#region *** Public Method ***
		/// <summary>
		/// 進捗％をリセットします。
		/// </summary>
		public void	ResetProgress()
		{
			progress_perc = 0;
			
			bgWorker.ReportProgress(progress_perc);
		}
		
		/// <summary>
		/// 進捗％を完了させます。
		/// </summary>
		public void	FinishProgress()
		{
			SetProgress(100);
		}
		
		/// <summary>
		/// 進捗％を取得します。
		/// </summary>
		public int	GetProgress()
		{
			return progress_perc;
		}
		
		/// <summary>
		/// 進捗％を設定します。
		/// </summary>
		/// <param name="perc">セットする％</param>
		public void	SetProgress(int perc)
		{
			if (perc < 0)
			{
				perc = 0;
			}
			if (perc > 100)
			{
				perc = 100;
			}
			progress_perc = perc;
			
			bgWorker.ReportProgress(progress_perc);
		}
		
		/// <summary>
		/// 進捗％を進めます。最大100%まで累積します。
		/// </summary>
		/// <param name="perc">進める％</param>
		public void	AdvanceProgress(int perc)
		{
			progress_perc += perc;
			
			if (progress_perc > 100)
			{
				progress_perc = 100;
			}
			
			bgWorker.ReportProgress(progress_perc);
		}
		#endregion
		
		#region *** Protected Method ***
		/// <summary>
		/// フォームロード。
		/// </summary>
		void Form_Load(object sender, EventArgs e)
		{
			if (LoadEvent != null)
			{
				LoadEvent(sender, e);
			}
		}

		/// <summary>
		/// Windowが開かれた後に１回だけ実行されます。
		/// 読み込みスレッドを実行します。
		/// </summary>
		void Form_Shown(object sender, EventArgs e)
		{
			if (this.DesignMode == true)
			{
				return;
			}

			// スレッド実行
			if (nobgworker == true)
			{
				ErrLog.WriteLine("NoBgWorker モードで起動します。");
				
				bgWorker_DoWork(null, new DoWorkEventArgs(null));
				this.Close();
			}
			else
			{
				if (arg == null)
				{
					bgWorker.RunWorkerAsync();
				}
				else
				{
					bgWorker.RunWorkerAsync(arg);
				}
			}
			
			// デザイン時に動的な処理を実行しないように
			if (this.DesignMode == false)
			{
				iconPat = 0;
				timer.Start();
			}

			// 無効なキー操作（ファンクションキー等）を検出し、切るために KeyPreview = true にしておく
			this.KeyPreview = true;
			
			this.KeyDown += new KeyEventHandler(Form_KeyDown);
			this.FormClosing += new FormClosingEventHandler(Form_FormClosing);
		}

		/// <summary>
		/// 無効なキー操作（ファンクションキー等）を全て切る
		/// </summary>
		void Form_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;
		}
		
		/// <summary>
		/// ウィンドウクローズ
		/// </summary>
		protected virtual void Form_FormClosing(object sender, FormClosingEventArgs e)
		{		
			bgWorker.CancelAsync();

			// bgWorkerの処理が終了するまで待つ
			while (bgWorker.IsBusy == true)
			{
				Application.DoEvents();
			}
		}
		
		/// <summary>
		/// 別スレッドBgWorkerの実行処理。
		/// </summary>
		protected virtual void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (DoWorkEvent != null)
			{
				DoWorkEvent(this, e);
				
				// 戻り値を格納
				Result = e.Result;
			}
			else if (DoWorkEventEx != null)
			{
				DoWorkEventArgsEx eex = new DoWorkEventArgsEx(null);
				eex.Args = args;
				
				DoWorkEventEx(this, eex);
				
				// 戻り値を格納
				Result = eex.Result;
			}
		}
		
		/// <summary>
		/// 別スレッド処理の進捗状況表示。
		/// </summary>
		protected virtual void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			int		val = e.ProgressPercentage;
			
			if (val < 0)
			{
				val = 0;
			}
			if (val >= 100)
			{
				val = 100;
			}
			
			//http://dobon.net/vb/dotnet/control/pbdisableanimation.html
			// 値を増やしてから減らす方法

			if (ProgressBar.Value < val)
			{
				//値を増やす時
				if (val < ProgressBar.Maximum)
				{
					//目的の値より一つ大きくしてから、目的の値にする
					ProgressBar.Value = val + 1;
					ProgressBar.Value = val;
				}
				else
				{
					//最大値にする時
					//最大値を1つ増やしてから、元に戻す
					ProgressBar.Maximum++;
					ProgressBar.Value = val + 1;
					ProgressBar.Value = val;
					ProgressBar.Maximum--;
				}
			}
			else
			{
				//値を減らす時は、そのまま
				ProgressBar.Value = val;
			}

			//ProgressBar.Value = val;
		}
		
		/// <summary>
		/// 別スレッド処理の終了。
		/// </summary>
		protected virtual void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// 読み込み成功していたらグリッドに反映。
			if (e.Cancelled == false)
			{
			}
			
			this.Close();
		}
		#endregion
		
		#region *** Private Method ***
		/// <summary>
		/// インジケータタイマー
		/// </summary>
		void timer_Tick(object sender, EventArgs e)
		{
			if (iconVisible == true)
			{
				IconIndicator.Image = images[iconPat];
				
				if (--iconPat < 0)
				{
					iconPat = images.Length - 1;
				}
			}
		}
		#endregion
	}
	
	/// <summary>
	/// クリックされた行の内容を含む EventArgs です。
	/// </summary>
	public class DoWorkEventArgsEx : DoWorkEventArgs
	{
		/// <summary>
		/// クリックされた行の内容です。
		/// </summary>
		public object[]		Args;
		
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="argument">RunWorkerAsync で渡される引数。複数引数を渡す場合はArgsにいれること。</param>
		public DoWorkEventArgsEx(object argument) : base (argument)
		{
		}
	}
	
	/// <summary>
	/// クリックされた行の内容を含む EventHandler です。
	/// </summary>
	public delegate void DoWorkEventHandlerEx(object sender, DoWorkEventArgsEx e);
}