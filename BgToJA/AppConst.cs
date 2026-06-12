using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace App
{
	/// <summary>
	/// 定数群
	/// </summary>
	public class AppConst
	{
		/// <summary>
		/// 
		/// </summary>
		public const string AppName = "BgToJA";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public static readonly string AppVersion = $"{AppMajor}{AppMinor}{AppRevision}";
		/// <summary>
		/// メジャーバージョン
		/// </summary>
		public const string AppMajor = "0";
		/// <summary>
		/// マイナーバージョン
		/// </summary>
		public const string AppMinor = ".7";
		/// <summary>
		/// リビジョン
		/// </summary>
		public const string AppRevision = ".7";

		/// <summary></summary>
		public const string AppTitle = "売上データ変換ForJA";

		/// <summary>システムフォルダ</summary>
		public const string SystemFolder = @"system\";

		/// <summary>レポートフォルダ</summary>
		public const string ReportFolder = @"report\";


		/// <summary>遠隔サポートファイル</summary>
		public const string RemoteSupportFile = "TeamViewerQS_ja.exe";

		/// <summary>遠隔実行ファイルパス</summary>
		public const string RemoteSupportFilePath = SystemFolder + RemoteSupportFile;

		/// <summary>フォームの背景色</summary>
		public static readonly Color FormBackColor = Color.WhiteSmoke;

		/// <summary>県コード初期値</summary>
		public static string CodeKen = "36";
		/// <summary>出荷元コード初期値</summary>
		public static string CodeShukamoto = "2003B50000";
	}

}
