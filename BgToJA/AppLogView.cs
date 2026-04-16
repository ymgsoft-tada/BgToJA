using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
	/// <summary>
	/// ログテーブルクラス
	/// </summary>
	public class LogTable
	{
		/// <summary>
		/// 状況
		/// </summary>
		public enum eStatus
		{
			/// <summary></summary>
			None,
			/// <summary></summary>
			Error,
			/// <summary></summary>
			Warning,
			/// <summary></summary>
			Success,
		}
		DataTable tbl;

		public const string F_No		= "F_No";
		public const string F_Type		= "F_Type";
		public const string F_Message	= "F_Message";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public LogTable()
		{
			tbl = new DataTable();

			tbl.Columns.Add(F_No, typeof(int));
			tbl.Columns.Add(F_Type, typeof(int));
			tbl.Columns.Add(F_Message);
		}

		public void Clear()
		{
			tbl.Clear();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sts"></param>
		/// <param name="msg"></param>
		public void	Apend(eStatus sts, string msg)
		{
			DataRow row = tbl.NewRow();

			row[F_No]	= tbl.Rows.Count + 1;
			row[F_Type] = (int)sts;
			row[F_Message] = msg;

			tbl.Rows.Add(row);
		}

		/// <summary>
		/// ログ用テーブルを取得します。
		/// </summary>
		/// <returns></returns>
		public DataTable GetTable()
		{
			return tbl;
		}
	}
}
