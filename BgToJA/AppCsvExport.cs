using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.ComponentModel;
using System.Diagnostics;

using ComponentDB;
using ComponentFile;
using ComponentIO;
using ComponentRegistry;
using System.Windows.Forms;
using ComponentDebug;

namespace App
{
	/// <summary>
	/// [作成者 fj]
	/// CSV 変換クラスです。
	/// </summary>
	public class AppCsvExport
	{
		#region *** Class Defines ***
		/// <summary>
		/// フィールド変換の実行定義を行うクラスです。
		/// </summary>
		protected class FieldDef
		{
			/// <summary>
			/// 変換時に実行されるメソッドデリゲート。
			/// </summary>
			/// <param name="dv">変換に必要なデータビュー。</param>
			/// <param name="args">変換に必要な引数。</param>
			public delegate string FieldDefDelegate(DBView dv, params object[] args);
			
			/// <summary>
			/// 出力用フィールド名。
			/// </summary>
			public string			Field;
			/// <summary>
			/// 変換時に実行されるメソッド。
			/// </summary>
			public FieldDefDelegate	FieldDefMethod;
			/// <summary>
			/// フィールドタイプ。
			/// </summary>
			public FieldType		FieldType;
			/// <summary>
			/// 最大長。
			/// </summary>
			public int				MaxLength;
			/// <summary>
			/// 変換メソッドの使用する引数。
			/// </summary>
			public object[]			Args;
			/// <summary>
			/// 最大長を超えた変換の場合、途中で切る(false)、System.DBNull.Value にする(true)を選ぶフラグ。基本は false。
			/// </summary>
			public bool				IsLengthOverEmpty = false;
			
			/// <summary>
			/// コンストラクタ。
			/// </summary>
			/// <param name="field">出力用フィールド名。</param>
			/// <param name="method">変換時に実行されるメソッド。</param>
			/// <param name="fieldtype">フィールドタイプ。</param>
			/// <param name="maxlength">パラメータの最大長。</param>
			/// <param name="args">変換メソッドの使用する引数。</param>
			public FieldDef(string field, FieldDefDelegate method, FieldType fieldtype, int maxlength, params object[] args)
			{
				Field			= field;
				FieldDefMethod	= method;
				FieldType		= fieldtype;
				MaxLength		= maxlength;
				Args			= args;
			}
		}
		#endregion

		#region *** Enum Defines ***
		/// <summary>
		/// コンバート状況。
		/// </summary>
		public enum ConvertStatus
		{
			/// <summary>
			/// 未処理
			/// </summary>
			None = 0,
			/// <summary>
			/// 成功
			/// </summary>
			Success,
			/// <summary>
			/// DB オープン失敗
			/// </summary>
			DbOpenFailed,
			/// <summary>
			/// DB 読込失敗
			/// </summary>
			DbReadFailed,
			/// <summary>
			/// コンバート失敗
			/// </summary>
			ConvertFailed,
			/// <summary>
			/// 保存失敗
			/// </summary>
			SaveFailed,
			/// <summary>
			/// コンバートデータがない
			/// </summary>
			NoData,
		}
		
		/// <summary>
		/// フィールドタイプ。
		/// </summary>
		public enum FieldType
		{
			/// <summary>
			/// 数字。0101 などは表現できないので、その場合は StringInt を使うこと。
			/// </summary>
			Number,
			/// <summary>
			/// 数字扱いで、ダブルクオーテーションでは区切らないが、0101 のように先頭に０がつく可能性のあるタイプ。
			/// </summary>
			StringNumber,
			/// <summary>
			/// 文字列。
			/// </summary>
			String,
		}
		#endregion
		
		#region *** Properties ***
		/// <summary>
		/// CSV テキストの保存先パス。
		/// </summary>
		public string		CsvPathname
		{
			set
			{
				csvpathname = value;
			}
			get
			{
				return csvpathname;
			}
		}
		/// <summary>
		/// CSV テキストの保存先パス。
		/// </summary>
		protected string	csvpathname;
		
		/// <summary>
		/// データビュー。
		/// </summary>
		public DBView		DbView
		{
			set
			{
				dbview = new DBView(value.DataTable);
			}
			get
			{
				return dbview;
			}
		}
		/// <summary>
		/// データビュー。
		/// </summary>
		protected DBView	dbview;
		
		/// <summary>
		/// コンバート結果を示すフラグ。
		/// </summary>
		public ConvertStatus ConvertResult
		{
			get
			{
				return convresult;
			}
		}
		/// <summary>
		/// コンバート結果を示すフラグ。
		/// </summary>
		protected ConvertStatus	convresult = ConvertStatus.None;
		#endregion
		
		#region *** Private Values ***
		/// <summary>
		/// フィールドに対する変換ルールを格納したリスト。
		/// </summary>
		protected List<FieldDef>	fielddefs;
		/// <summary>
		/// 作成した CSV テキスト。
		/// </summary>
		protected string			csvtext;
		#endregion
		
		#region *** Constructor ***
		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public AppCsvExport()
		{
			fielddefs = new List<FieldDef>();
		}
		#endregion
		
		/// <summary>
		/// データコンバートメインプロセス。
		/// </summary>
		public virtual void BgProcessDataConvert(object sender, DoWorkEventArgs e)
		{
		}
		
		/// <summary>
		/// フィールド変換リストに従ってテーブルを作成します。
		/// </summary>
		/// <returns>作成したテーブル。</returns>
		public DataTable MakeDataTable()
		{
			//■ 変換後テーブル作成。
			DataTable	dt = new DataTable();
			
			for (int i = 0; i < fielddefs.Count; i++)
			{
				FieldDef	f = fielddefs[i];
				
				dt.Columns.Add(f.Field, typeof(string));
				
				// カラム固有情報の設定。
				if (f.FieldType == FieldType.String)
				{
					dt.Columns[i].ExtendedProperties.Add("string", null);
				}
			}
			
			return dt;
		}

		/// <summary>
		/// 変換処理を実行し データテーブルへ格納します。
		/// </summary>
		/// <param name="srcdv">変換前データビュー。</param>
		/// <param name="dstdt">変換後データテーブル。</param>
		/// <param name="bgform">（必要な場合）プログレスバー用</param>
		/// <param name="start">（必要な場合）プログレスバースタート位置（％）</param>
		/// <param name="count">（必要な場合）プログレスバー進捗カウント（％）</param>
		protected virtual void AppendConvertedData(DBView srcdv, DataTable dstdt, FormBg_Progress bgform, int start, int count)
		{
			for (int i = 0; i < srcdv.Count; i++)
			{
				DataRow		drow = dstdt.NewRow();
				
				// カレント行の移動。（FieldDefMethodが参照するため必要）
				srcdv.Row = i;
				
				// フィールドリストの数だけ繰り返す。
				for (int j = 0; j < fielddefs.Count; j++)
				{
					FieldDef	f = fielddefs[j];
					string		r = null;
					
					// データを変換。
					if (f.FieldDefMethod != null)
					{
						try
						{
							r = f.FieldDefMethod(srcdv, f.Args);
						}
						catch
						{
							// 変換失敗。
							ErrLog.WriteLine("CSV 変換失敗。{0}行目 : {1}", i, f.Field);
							r = null;
						}
					}
					
					if (r != null)
					{
						// 桁区切り。
						try
						{
							drow[f.Field] = valueCut(r, f.MaxLength, f.IsLengthOverEmpty);
						}
						catch
						{
							// 変換失敗。
							ErrLog.WriteLine("桁区切り失敗。{0}行目 : {1}", i, f.Field);
							
							drow[f.Field] = System.DBNull.Value;
						}
					}
				}
				
				dstdt.Rows.Add(drow);
				
				//▽▽▽ 進捗バーの更新。
				if (bgform != null) bgform.SetProgress(80 * (i+1) / srcdv.Count);
			}
		}
		
		/// <summary>
		/// データテーブルから CSV用文字列データ を作成します。
		/// </summary>
		/// <param name="dt">データテーブル。</param>
		/// <param name="bgform">（必要な場合）プログレスバー用</param>
		/// <param name="start">（必要な場合）プログレスバースタート位置（％）</param>
		/// <param name="count">（必要な場合）プログレスバー進捗カウント（％）</param>
		/// <param name="hasHeader">ヘッダの有無</param>
		/// <param name="stringEscape">文字列をダブルクォートでエスケープするか否か</param>
		/// <returns>作成した CSV。</returns>
		protected virtual string MakeCsv(DataTable dt, FormBg_Progress bgform, int start, int count, bool hasHeader, bool stringEscape)
		{
			StringBuilder	sb = new StringBuilder(100 * 1024);
			
			if (hasHeader == true)
			{
				// 列ヘッダ。
				for (int i = 0; i < dt.Columns.Count; i++)
				{
					sb.Append(dt.Columns[i]);

					if (i < dt.Columns.Count - 1)
					{
						sb.Append(",");
					}
					else
					{
						sb.Append("\r\n");
					}
				}
			}
			
			// 情報。
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow		row = dt.Rows[i];
				
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					PropertyCollection	p = dt.Columns[j].ExtendedProperties;
					
					// 文字列はダブルクオートで囲む。
					if (p.ContainsKey("string") == true && stringEscape == true)
					{
						sb.Append(@"""");
					}
					sb.Append(Cast.String(row[dt.Columns[j]]));
					if (p.ContainsKey("string") == true && stringEscape == true)
					{
						sb.Append(@"""");
					}
					
					if (j < dt.Columns.Count - 1)
					{
						sb.Append(",");
					}
					else
					{
						sb.Append("\r\n");
					}
				}
				
				//▽▽▽ 進捗バーの更新。
				if (bgform != null) bgform.SetProgress(start + count * (i+1) / dt.Rows.Count);
			}
			
			return sb.ToString();
		}
		
		/// <summary>
		/// 作成した CSV用データ をファイルへ保存します。
		/// </summary>
		public virtual void FileSave()
		{
			try
			{
				if (FileText.Write(csvpathname, csvtext) == false)
				{
					convresult = ConvertStatus.SaveFailed;
				}
				else
				{
					// FileSaveでも成功フラグを立てる。
					convresult = ConvertStatus.Success;
				}
			}
			catch
			{
				convresult = ConvertStatus.SaveFailed;
			}
		}
		
		#region ■■■ Field 変換定義（共通） ■■■
		/// <summary>
		/// 空欄。
		/// </summary>
		protected string FDef_Null(DBView dv, params object[] args)
		{
			return null;
		}
		
		/// <summary>
		/// 文字列。
		/// </summary>
		protected string FDef_String(DBView dv, params object[] args)
		{
			if (args != null && args.Length > 0)
			{
				DataRow		srow  = dv.CurrentRow.Row;
				string		fname = Cast.String(args[0]);
			
				if (srow[fname] == System.DBNull.Value)
				{
					return null;
				}
			
				return Cast.String(srow[fname]);
			}

			return null;
		}
		
		/// <summary>
		/// 金額。
		/// </summary>
		protected string FDef_Decimal(DBView dv, params object[] args)
		{
			DataRow		srow  = dv.CurrentRow.Row;
			string		fname = Cast.String(args[0]);
			
			if (srow[fname] == System.DBNull.Value)
			{
				return null;
			}
			
			decimal		val	= Cast.Decimal(srow[fname]);
			
			return val.ToString("#0");
		}

		/// <summary>
		/// 金額。
		/// </summary>
		protected string FDef_Decimal_ZeroNull(DBView dv, params object[] args)
		{
			DataRow		srow  = dv.CurrentRow.Row;
			string		fname = Cast.String(args[0]);
			
			decimal		val	= Cast.Decimal(srow[fname]);

			if (val == 0)
			{
				return null;
			}
			
			return val.ToString("#0");
		}

		/// <summary>
		/// 小数点第2位まで
		/// </summary>
		protected string FDef_Decimal_Min2(DBView dv, params object[] args)
		{
			DataRow		srow  = dv.CurrentRow.Row;
			string		fname = Cast.String(args[0]);
			
			if (srow[fname] == System.DBNull.Value)
			{
				return null;
			}
			
			decimal		val	= Cast.Decimal(srow[fname]);
			
			return val.ToString("#0.00");
		}
		#endregion
		
		/// <summary>
		/// ファイルパスを取得します。
		/// </summary>
		/// <param name="title">ファイル名に表示する名前</param>
		public static string ShowSaveFilePath(string title)
		{
			string filepath = null;

			SaveFileDialog	fd		= new SaveFileDialog();

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
			fd.Title = "保存先を指定してください。";
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
		/// value を指定の長さにカット。
		/// </summary>
		/// <param name="s">value</param>
		/// <param name="maxlength">長さ（バイト長）</param>
		/// <param name="isoverempty">長さを超えた場合切る(false)か、ないことにする(true)か</param>
		/// <returns>カットした value</returns>
		object	valueCut(string s, int maxlength, bool isoverempty)
		{
			object	o;
			
			if (s.Length >= maxlength)
			{
				if (isoverempty == true)
				{
					o = System.DBNull.Value;
				}
				else
				{
					o = s.Substring(0, maxlength);
				}
			}
			else
			{
				o = s;
			}
			
			return o;
		}
	}
}
