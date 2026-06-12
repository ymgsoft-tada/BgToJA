using ComponentDB;
using ComponentDebug;
using ComponentForm;
using GControlGcComboBoxEx;
using GControlGcDateEx;
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
	/// メイン画面
	/// 作成者:kj
	/// </summary>
	public partial class FormMain : AppForm
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormMain()
		{
			InitializeComponent();

			AppPath.Init();
		}

		/// <summary>
		/// ロード処理
		/// </summary>
		protected override void AppForm_Load(object sender, EventArgs e)
		{
			this.Text = $"{AppConst.AppTitle}　Version {AppConst.AppVersion}";
		}

		/// <summary>
		/// 初回描画処理
		/// </summary>
		protected override void AppForm_Shown(object sender, EventArgs e)
		{
			// 入力フィールドのルール設定
			AppDbRule.SetControlByRule(iCodeMoto, AppDbRule.RuleHulf20);
			AppDbRule.SetControlByRule(iCodeKen, AppDbRule.RuleHulf20);

			// 値の取得
			loadCode();

			// イベントの登録
			btnExec.Click += BtnExec_Click;
			btnClose.Click += BtnClose_Click;
			linkSetting.Click += LinkSetting_Click;

			btnClose.Select();
		}

		void loadCode()
		{
			iCodeKen.Text	= Properties.Settings.Default.CodeKen;
			iCodeMoto.Text	= Properties.Settings.Default.CodeShukamoto;
		}

		/// <summary>
		/// 設定リンク
		/// </summary>
		private void LinkSetting_Click(object sender, EventArgs e)
		{
			FormDlg_Setting frm = new FormDlg_Setting();
			frm.ShowDialog();

			if (frm.CloseRason == eCloseReason.Save)
			{
				// 設定値の全初期化
				if (frm.InitAll == true)
				{
					Properties.Settings.Default.CodeKen			= AppConst.CodeKen;
					Properties.Settings.Default.CodeShukamoto	= AppConst.CodeShukamoto;

					Properties.Settings.Default.SrcPath			= AppPath.GetImportDefaultPath();
					Properties.Settings.Default.DstPath			= AppPath.GetExportDefaultPath();

					loadCode();
				}
				else
				{
					// パスだけ保存
					Properties.Settings.Default.SrcPath = frm.BugyoFile;
					Properties.Settings.Default.DstPath = frm.JAFolder;
				}

				Properties.Settings.Default.Save();
			}

			frm.Dispose();
		}

		/// <summary>
		/// 実行ボタン
		/// </summary>
		private void BtnExec_Click(object sender, EventArgs e)
		{
			try
			{
				// １．奉行データの読込
				AppCsvImportBugyo bugyo = new AppCsvImportBugyo();

				AppCsvImport.ImportStatus status = bugyo.LoadFile(AppPath.GetImportFilePath());

				string msg = string.Empty;
				string cap = string.Empty;
				MessageBoxIcon icon = MessageBoxIcon.None;

				if (status != AppCsvImport.ImportStatus.Success)
				{
					// エラー値でメッセージを変える場合に使う
					switch(status)
					{
						default : 
							icon = MessageBoxIcon.Error;
							cap = "エラー";
							msg = "奉行ファイルの読込に失敗しました。"; 
							break;
					}

					MessageBox.Show(this, msg, cap, MessageBoxButtons.OK, icon);
					return;
				}

				// ２．商品区分一覧の読込
				AppCsvImportShohin shohin = new AppCsvImportShohin();
				status = shohin.LoadFile();

				if (status != AppCsvImport.ImportStatus.Success)
				{
					// エラー値でメッセージを変える場合に使う
					switch(status)
					{
						default : 
							icon = MessageBoxIcon.Error;
							cap = "エラー";
							msg = "商品区分一覧の読込に失敗しました。"; 
							break;
					}

					MessageBox.Show(this, msg, cap, MessageBoxButtons.OK, icon);
					return;
				}

				// 3．JA送信データに変換
				AppCsvExportJA ja	= new AppCsvExportJA();
				ja.DbView			= new DBView(bugyo.CsvTable);
				ja.CsvPathname		= AppPath.GetExportFilePath(bugyo.GetUriageDate1stRow());
				ja.CodeKen			= iCodeKen.Text;
				ja.CodeShukamoto	= iCodeMoto.Text;
				ja.ShohinTable		= shohin.CsvTable;

				FormBg_Progress bgform		= new FormBg_Progress();
				bgform.LabelText			= "CSVをエクスポートしています...";
				bgform.DoWorkEvent			+= new DoWorkEventHandler(ja.BgProcessDataConvert);
				bgform.ShowDialog();
			
				ja.FileSave();

			
				// 結果を表示。
				switch (ja.ConvertResult)
				{
					case AppCsvExport.ConvertStatus.Success :
						icon = MessageBoxIcon.Information;
						cap = "成功";
						msg = $"対象期間：{bugyo.GetUriageDateRange()}\r\n\r\n" +
							  $"変換に成功しました。";

						// 最後にフィールド値を保存
						Properties.Settings.Default.CodeKen			= iCodeKen.Text;
						Properties.Settings.Default.CodeShukamoto	= iCodeMoto.Text;
						Properties.Settings.Default.Save();
						break;
					case AppCsvExport.ConvertStatus.SaveFailed :
						icon = MessageBoxIcon.Warning;
						cap = "ファイルの保存に失敗しました。";
						msg = "保存先フォルダが存在しない、\nまたはファイルが使用されている可能性があります。";
						break;
					default :
						icon = MessageBoxIcon.Error;
						cap = "エラー";
						msg = "CSVのエクスポートに失敗しました。";
						break;
				}

				MessageBox.Show(this,msg,cap, MessageBoxButtons.OK,icon);
			}
			catch(Exception ex)
			{
				ErrLog.WriteException(ex);
			}
		}

		/// <summary>
		/// 終了ボタン
		/// </summary>
		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
