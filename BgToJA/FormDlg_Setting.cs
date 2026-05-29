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
		public string BugyoFile { get { return iBugyoSelect.UcText; } }
		/// <summary>JAフォルダのパス</summary>
		public string JAFolder { get { return iJASelect.UcText; } }

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
			AppDbRule.SetControlByRule(iBugyoSelect.UcTextBox, AppDbRule.RuleHulf200); // ※AppDbRule.RuleHulf200: テキスト領域に設定するルール
			AppDbRule.SetControlByRule(iJASelect.UcTextBox, AppDbRule.RuleHulf200);

			iBugyoSelect.UcText = AppPath.GetImportFilePath();
			iJASelect.UcText = AppPath.GetExportFolderPath();

			// UcFileSelectコントロールの設定
			iBugyoSelect.FileDialogType = UcFileSelect.eFileDialogType.FileOpen;
			iBugyoSelect.FileFilter = "CSV (コンマ区切り) (*.csv)|*.csv|すべてのファイル(*.*)|*.*";
			iJASelect.FileDialogType = UcFileSelect.eFileDialogType.FolderSelect;
//			iJASelect.FileFilter = "旧富士山財務会計ファイル(*.vbfz)|*.vbfz|すべてのファイル(*.*)|*.*";

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
