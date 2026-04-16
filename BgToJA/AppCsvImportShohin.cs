using ComponentDB;
using ComponentDebug;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
	/// <summary>
	/// 商品区分一覧用のCSVインポート
	/// </summary>
	public class AppCsvImportShohin:AppCsvImport
	{
		/// <summary>ファイルパス（システム固定）</summary>
		public const string FilePath = @"system\shohin.csv";

		/// <summary></summary>
		public const string 品種コード="品種コード";
		/// <summary></summary>
		public const string 品種名="品種名";
		/// <summary></summary>
		public const string 品種略称名="品種略称名";
		/// <summary></summary>
		public const string JA商品ｸﾞﾙｰﾌﾟ="JA商品ｸﾞﾙｰﾌﾟ";

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
				品種コード,
				品種名,
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
		/// CSVファイルの読込
		/// </summary>
		/// <returns></returns>
		public ImportStatus LoadFile()
		{
			return LoadFile(FilePath);
		}
	}

	/// <summary>
	/// 商品区分を管理するクラス
	/// </summary>
	public class ShohinManager
	{
		Dictionary<string, Shohin> dics_all;

		/// <summary>商品テーブル</summary>
		public DataTable Table { get; private set; }
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ShohinManager()
		{
			dics_all = new Dictionary<string, Shohin>();
		}

		/// <summary>
		/// 初期化処理
		/// </summary>
		/// <param name="tbl">商品用テーブル</param>
		public void Init(DataTable tbl)
		{
			DBView dv = new DBView(tbl);
			dv.SortQuery(AppCsvImportShohin.品種コード);

			for (int i = 0; i < dv.Count; i++)
			{
				DataRow row = dv[i].Row;

				string cd = Cast.String(row[AppCsvImportShohin.品種コード]);
				if (dics_all.ContainsKey(cd) == false)
				{
					dics_all.Add(cd, new Shohin());
				}

				dics_all[cd].Code		= cd;
				dics_all[cd].Name		= Cast.String(row[AppCsvImportShohin.品種名]);
				dics_all[cd].ShortName	= Cast.String(row[AppCsvImportShohin.品種略称名]);
				dics_all[cd].JAGrpCode	= Cast.String(row[AppCsvImportShohin.JA商品ｸﾞﾙｰﾌﾟ]);
			}
		}

		/// <summary>
		/// 品種コードから商品クラスを取得します。
		/// </summary>
		/// <param name="cd"></param>
		/// <returns></returns>
		public Shohin GetByCode(string cd)
		{
			if (dics_all.ContainsKey(cd))
			{
				return dics_all[cd];
			}

			return null;
		}
	}

	/// <summary>
	/// 商品区分クラス
	/// </summary>
	public class Shohin
	{
		/// <summary>品種コード</summary>
		public string Code { get; set; } = string.Empty;
		/// <summary>品種名</summary>
		public string Name { get; set; } = string.Empty;
		/// <summary>品種略称名</summary>
		public string ShortName { get; set; } = string.Empty;
		/// <summary>JAグループコード</summary>
		public string JAGrpCode { get; set; } = string.Empty;
	}
}
