using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// バックグラウンドタスク用のフレームフォームです。
	/// </summary>
	public partial class FormBg_Worker : Form
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
		/// 乱数生成
		/// </summary>
		Random		rnd;
		#endregion
		
		#region *** Property ***
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
				
				this.Width = value;
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
							lblInfo.Text = value;
						}
					);
				}
				else
				{
					lblInfo.Text = value;
				}
			}
			get
			{
				return lblInfo.Text;
			}
		}
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
		public FormBg_Worker()
		{
			InitializeComponent();
			
			progress_perc = 0;
			arg			  = null;
			nobgworker	  = false;
			
			rnd = new Random();
			
			this.BackColor = Color.WhiteSmoke;
			this.Load += new EventHandler(Form_Load);
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
			
			this.Shown += new EventHandler(Form_Shown);
		}

		/// <summary>
		/// Windowが開かれた後に１回だけ実行されます。
		/// 読み込みスレッドを実行します。
		/// </summary>
		void Form_Shown(object sender, EventArgs e)
		{
			// スレッド実行
			if (nobgworker == true)
			{
				Debug.WriteLine("■ NoBgWorker モードで起動します。");
				
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
			}
			else if (DoWorkEventEx != null)
			{
				DoWorkEventArgsEx eex = new DoWorkEventArgsEx(null);
				eex.Args = args;
				
				DoWorkEventEx(this, eex);
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
		/// タイマーによって適当にHDDランプを点灯・消灯させる。
		/// </summary>
		void timer_Tick(object sender, EventArgs e)
		{
			if (pic1.Visible == true)
			{
				pic1.Visible = false;
			}
			else
			{
				pic1.Visible = true;
			}

			// 点灯時間にバラつきを持たせる
			timer.Interval = 100 + rnd.Next(5) * 100;
		}
		#endregion
	}
}