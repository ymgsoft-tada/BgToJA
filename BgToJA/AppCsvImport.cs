using ComponentDebug;
using System;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 kj]
	/// CSVファイルのインポート用クラス
	/// </summary>
	public class AppCsvImport
	{
		/// <summary>インポート状況</summary>
		public enum ImportStatus
		{
			/// <summary></summary>
			None,
			/// <summary>成功</summary>
			Success,
			/// <summary>ファイルオープン失敗</summary>
			OpenFileFailed,
			/// <summary>コンバート失敗</summary>
			ConvertFailed,
			/// <summary>必須項目不足</summary>
			NothingRequired,
			/// <summary>コンバート対象データがない</summary>
			NoData,
		}

		protected DataTable dtSrc;

		/// <summary>インポート実行結果</summary>
		protected ImportStatus execStatus;

		/// <summary>ログ内容</summary>
		public LogTable Log { get; private set; }
		public object FieldType { get; private set; }

		/// <summary>
		/// ファイルパスを取得します。
		/// </summary>
		/// <param name="title">ファイル名に表示する名前</param>
		public static string ShowOpenFilePath(string title)
		{
			string filepath = null;

			OpenFileDialog	fd		= new OpenFileDialog();

			// はじめに表示されるフォルダ
			string	inipath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			
			if (title != null)
			{
				string path = Path.GetDirectoryName(title);

				if (path.Length > 0)
				{
					inipath = path;
				}
			}
			
			fd.InitialDirectory = inipath;
			fd.FileName			= Path.GetFileName(title);
			// [ファイルの種類]に表示される選択肢を指定する
			fd.Filter = string.Format("CSVファイル|*.csv");
			fd.FilterIndex = 0;
			fd.RestoreDirectory = true;
			
			// ダイアログを表示
			if (fd.ShowDialog() == DialogResult.OK)
			{
				filepath = fd.FileName;
			}
			fd.Dispose();

			return filepath;
		}

		/// <summary>
		/// CSVファイルの読込
		/// </summary>
		/// <param name="path">CSVファイル</param>
		/// <returns></returns>
		public ImportStatus LoadFile(string path)
		{
			this.Log = new LogTable();

			if (loadFileToTable(path) == false)
			{
				Log.Apend(LogTable.eStatus.Error, "対象外のファイルか、他で使用中の為ファイルをオープンできません。");
				return ImportStatus.OpenFileFailed;
			}

			if (checkSrcColumnRequried() == false)
			{
				Log.Apend(LogTable.eStatus.Error, "指定されたファイルは、インポート対象ファイルではありません。");

				return ImportStatus.NothingRequired;
			}

			Log.Apend(LogTable.eStatus.None, $"{path}の読み込みに成功しました。");

			return ImportStatus.Success;
		}

		/// <summary>
		/// インポート処理を実行します。
		/// </summary>
		/// <returns></returns>
		public ImportStatus ExecImport()
		{
			FormBg_Progress prog = new FormBg_Progress();
			prog.TitleText = "しばらくお待ちください。";
			prog.LabelText = "インポートを実行しています。";
			
			prog.DoWorkEvent += doImportEvent;
			prog.ShowDialog();
			prog.Dispose();
			prog = null;

			return execStatus;
		}

		/// <summary>
		/// 実際のインポート処理
		/// </summary>
		protected virtual void doImportEvent(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			// 中身は継承先で
		}

		/// <summary>
		/// CSVファイルの状態を確認します。
		/// </summary>
		/// <returns></returns>
		protected virtual bool checkSrcColumnRequried()
		{
			// 中身は継承先で
			return true;
		}

		/// <summary>
		/// CSVファイルの読み込み処理
		/// </summary>
		/// <param name="path">ファイルパス</param>
		protected bool loadFileToTable(string path)
		{
			dtSrc = new DataTable();

			try
			{
				// CSVの読み込み
				TextFieldParser parser = new TextFieldParser(path, Encoding.GetEncoding("Shift_JIS"));

				parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
				parser.SetDelimiters(",");

				int idx = 0;

				while(!parser.EndOfData)
				{
					string[] row = parser.ReadFields();

					if (idx == 0)
					{						
						for (int i = 0; i < row.Length; i++)
						{
							dtSrc.Columns.Add(row[i], typeof(string));
						}
					}
					// ２行目以降がデータ行
					if (row.Length > 0 && idx > 0)
					{
						if (row.Length <= dtSrc.Columns.Count)
						{
							DataRow nrow = dtSrc.NewRow();

							// データ行
							for(int i = 0; i < row.Length; i++)
							{
								if (dtSrc.Columns.Count > i)
								{
									nrow[i] = row[i];
								}
								else
								{
									ErrLog.WriteLine("列数{0}は範囲外です。", i);
								}
							}

							dtSrc.Rows.Add(nrow);
						}
					}

					idx++;
				}

				parser.Close();
				parser.Dispose();

				return true;
			}
			catch(Exception ex)
			{
				ErrLog.WriteException(ex);
				dtSrc = null;

				return false;
			}
		}
	}
}
