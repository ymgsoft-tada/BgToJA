using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
	/// <summary>
	/// パスを管理するクラス
	/// 作成者:kj
	/// </summary>
	public class AppPath
	{
#if DEBUG
		/// <summary>インポートファイル名</summary>
		public static string ImportFileDir = @"file\";
		/// <summary>エクスポートファイル名</summary>
		public static string ExportFileDir = @"file\";
#else
		/// <summary>インポートファイル名</summary>
		public static string ImportFileDir = @"C:\種苗奉行DT\";
		/// <summary>エクスポートファイル名</summary>
		public static string ExportFileDir = @"C:\種苗JA送信DT\";
#endif
		public static string ImportFileName = "奉行_売上伝票.csv";
		public static string ExportFileName = "WebDT-{0}.txt";

		/// <summary>
		/// 初期化処理
		/// </summary>
		public static void Init()
		{
			bool changed = false;

			// 初期値のセット
			if (string.IsNullOrEmpty(Properties.Settings.Default.SrcPath))
			{
				Properties.Settings.Default.SrcPath = GetImportDefaultPath();
				changed = true;
			}
			if (string.IsNullOrEmpty(Properties.Settings.Default.DstPath))
			{
				Properties.Settings.Default.DstPath = GetExportDefaultPath();
				changed = true;
			}

			if (changed) Properties.Settings.Default.Save();
		}

		/// <summary>
		/// インポート用の初期ファイルパスを取得します。
		/// </summary>
		/// <returns></returns>
		public static string GetImportDefaultPath()
		{
			return $"{ImportFileDir}{ImportFileName}";
		}

		/// <summary>
		/// エクスポート用の初期パスを取得します。
		/// </summary>
		/// <returns></returns>
		public static string GetExportDefaultPath()
		{
			return $"{ExportFileDir}";
		}

		/// <summary>
		/// インポート用のファイルパスを取得します。
		/// </summary>
		/// <returns></returns>
		public static string GetImportFilePath()
		{
			return Properties.Settings.Default.SrcPath;
		}

		/// <summary>
		/// エクスポート用のフォルダーパスを取得します。
		/// </summary>
		/// <returns></returns>
		public static string GetExportFolderPath()
		{
			return $"{Properties.Settings.Default.DstPath}";
		}
		/// <summary>
		/// エクスポート用のファイルパス（指定日）を取得します。
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static string GetExportFilePath(string date)
		{
			return $"{GetExportFolderPath()}{string.Format(ExportFileName, date.Substring(2))}"; //dateは"yyMMdd"へ
		}
	}
}
