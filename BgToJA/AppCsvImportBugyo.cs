using ComponentDebug;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App
{
	/// <summary>
	/// 奉行用CSVのインポートクラス
	/// 作成者:kj
	/// </summary>
	public class AppCsvImportBugyo : AppCsvImport
	{
		/// <summary></summary>
		public const string 区切 = "区切";
		/// <summary></summary>
		public const string 売上日付 = "売上日付";
		/// <summary></summary>
		public const string 伝票No = "伝票No.";
		/// <summary></summary>
		public const string 得意先コード = "得意先コード";
#if DEBUG
		/// <summary></summary>
		public const string 得意先名 = "得意先名１";
#else
		/// <summary></summary>
		public const string 得意先名 = "得意先名";
#endif
		/// <summary></summary>
		public const string 摘要 = "摘要";
		/// <summary></summary>
		public const string 商品コード = "商品コード";
		/// <summary></summary>
		public const string 商品名 = "商品名";
		/// <summary></summary>
		public const string 数量 = "数量";
		/// <summary></summary>
		public const string 単価 = "単価";
		/// <summary></summary>
		public const string 売単価 = "売単価";
		/// <summary></summary>
		public const string 売価金額 = "売価金額";
		/// <summary></summary>
		public const string 付箋メモ = "付箋メモ";

		/// <summary>取り込んだCSVをテーブルとして取得します。</summary>
		public DataTable CsvTable { get { return dtSrc; }}

		/// <summary>
		/// 読み込んだデータのチェック
		/// </summary>
		/// <returns></returns>
		protected override bool checkSrcColumnRequried()
		{
			if (dtSrc == null)
			{
				ErrLog.WriteLine("CSVファイルの読込に失敗しています。");
				return false;
			}

			List<string> req_flds = new List<string>()
			{
				区切,
				売上日付,
				伝票No,
				得意先コード,
				得意先名,
				摘要,
				商品コード,
				商品名,
				数量,
				単価,
				売単価,
				売価金額,
				付箋メモ,
			};

			foreach(var fld in req_flds)
			{
				if (dtSrc.Columns.Contains(fld) == false)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 1行目の売上日付を取得します。
		/// </summary>
		/// <returns></returns>
		public string GetUriageDate1stRow()
		{
			string date = string.Empty;

			if (dtSrc.Rows.Count > 0)
			{
				// 2025/05/01 →20250501とする
				date = Regex.Replace(Cast.String(dtSrc.Rows[0][売上日付]),"/","");
			}

			return date;
		}

		/// <summary>
		/// 売上日付のfrom～toで返します。
		/// </summary>
		/// <returns></returns>
		public string GetUriageDateRange()
		{
			string date = string.Empty;

			if (dtSrc.Rows.Count > 0)
			{
				date = $"{Cast.String(dtSrc.Rows[dtSrc.Rows.Count-1][売上日付])}～" +
					   $"{Cast.String(dtSrc.Rows[0][売上日付])}";
			}

			return date;
		}
	}
}
