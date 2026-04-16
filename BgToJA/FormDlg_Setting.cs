using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// 設定外面
	/// 作成者：kj
	/// </summary>
	public partial class FormDlg_Setting : AppForm
	{
		/// <summary>設定値初期化を取得します。</summary>
		public bool InitAll { get { return  iInitCode.Checked;}}
		/// <summary>奉行ファイルのパス</summary>
		public string BugyoFile { get { return iBugyo.Text; }}
		/// <summary>JAフォルダのパス</summary>
		public string JAFolder { get { return iJA.Text; }}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormDlg_Setting()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void AppForm_Shown(object sender, EventArgs e)
		{
			AppDbRule.SetControlByRule(iBugyo, AppDbRule.RuleHulf200);
			AppDbRule.SetControlByRule(iJA, AppDbRule.RuleHulf200);

			iBugyo.Text = AppPath.GetImportFilePath();
			iJA.Text	= AppPath.GetExportFolderPath();

			// イベント登録
			btnCancel.Click += BtnCancel_Click;
			btnSave.Click += BtnSave_Click;
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(this, "設定を登録します。よろしいですか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

			if (dr == DialogResult.Yes)
			{
				closeReason = eCloseReason.Save;
				this.Close();
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
