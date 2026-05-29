using ComponentForm;
using GControlGcComboBoxEx;
using GControlGcDateEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// フォームのスーパークラス
	/// 作成者:kj
	/// </summary>
	public class AppForm:Form
	{
		/// <summary>
		/// 閉じる理由
		/// </summary>
		public enum eCloseReason
		{
			Exec,
			Save,
			Close,
			Cancel,
		}

		/// <summary>
		/// フォームを閉じた理由を取得・設定します。
		/// </summary>
		public eCloseReason CloseRason { get{ return closeReason; } }

		protected eCloseReason closeReason;

		/// <summary>
		/// エンターキーによるフォーカス遷移
		/// </summary>
		public bool EnterFocus { get; set; } = true;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppForm()
		{
			this.Icon = Properties.Resources.icon;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;

			// キーダウンイベントの登録
			this.KeyPreview = true;
			this.KeyDown += FormMain_KeyDown;
			this.KeyPress += FormMain_KeyPress;

			this.Load += AppForm_Load;
			this.Shown += AppForm_Shown;
		}

		protected virtual void AppForm_Load(object sender, EventArgs e)
		{
			
		}

		protected virtual void AppForm_Shown(object sender, EventArgs e)
		{
			
		}

		private void FormMain_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (EnterFocus == true)
			{
				FormCommon.EnterFocusKeyPress(sender, e);
			}
		}

		private void FormMain_KeyDown(object sender, KeyEventArgs e)
		{
			Form frm = (Form)sender;
			
			//■ ファンクションキーの標準ショートカットの抑止
			e.Handled = Patcher_CheckShortcutForKillingWindowsAction(e.KeyCode, e.Alt);

			if (CheckEnabledFormKeyPreview(e.KeyCode) == true)
			{
				return;
			}

			if (frm.ActiveControl == null)
			{
				return;
			}

			if (EnterFocus == true)
			{
				FormCommon.EnterFocusKeyDown(sender, e);
			}
		}

		
		/// <summary>
		/// フォームでキープレビューしても問題ないか調査します。
		/// </summary>
		/// <returns></returns>
		public bool CheckEnabledFormKeyPreview(Keys keycode)
		{
			bool	handled = false;
			
			switch (keycode)
			{
				case Keys.Escape :
					break;
				case Keys.Enter :
					if (this.ActiveControl is GcComboBoxEx)
					{
						GcComboBoxEx		c = (GcComboBoxEx)this.ActiveControl;
						
						if (c.DroppedDown == true)	handled = true;
					}
					else if (this.ActiveControl is GcDateEx)
					{
						GcDateEx		c = (GcDateEx)this.ActiveControl;
						
						if (c.DroppedDown == true)	handled = true;
					}
					break;
				case Keys.Up :
				case Keys.Down :
					if (this.ActiveControl is GcComboBoxEx)
					{
						// ComboEx は上下で項目変更。
						handled = true;
					}
					else
					if (this.ActiveControl is GcDateEx)
					{
						// DateEx は上下で日付変更。
						handled = true;
					}
					break;
				default :
					break;
			}
			
			return handled;
		}

		/// <summary>
		/// ファンクションキーによるWindows標準ショートカット機能を抑止するかチェックします。
		/// </summary>
		/// <param name="keycode">キーコード</param>
		/// <param name="alt">Altキー</param>
		/// <returns></returns>
		public bool Patcher_CheckShortcutForKillingWindowsAction(Keys keycode, bool alt)
		{
			bool	handled = false;
			
			if ((keycode == Keys.F4 && alt == true) ||
				(keycode == Keys.F10))
			{
				// Alt+F4とF10キーは無効とする。
				handled  = true;
			}

			return handled;
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppForm));
			this.SuspendLayout();
			// 
			// AppForm
			// 
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AppForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}
	}
}
