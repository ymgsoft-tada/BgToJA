using ComponentDB;
using ComponentDebug;
using ComponentIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App
{
	/// <summary>
	/// 全農用送信データ（CSV）変換
	/// 作成者:kj
	/// </summary>
	public class AppCsvExportJA:AppCsvExport
	{
		/// <summary>県コードの値</summary>
		public string CodeKen { get; set; }
		/// <summary>出荷元コード</summary>
		public string CodeShukamoto { get; set; }

		/// <summary>商品テーブルを取得・設定します。</summary>
		public DataTable ShohinTable
		{
			get
			{
				if (sm != null) return sm.Table;

				return null;
			}
			set
			{
				// 商品管理クラスを初期化
				sm = new ShohinManager();
				sm.Init(value);
			}
		}

		// 商品管理クラス
		ShohinManager sm;

		/// <summary>
		/// 出力定義
		/// </summary>
		public class ExportFormat
		{
			/// <summary></summary>
			public const string SEQ_NO = "ＳＥＱ－ＮＯ";
			/// <summary></summary>
			public const string 県コード = "県コード";
			/// <summary></summary>
			public const string 荷受人コード = "荷受人コード";
			/// <summary></summary>
			public const string 荷受人名称 = "荷受人名称";
			/// <summary></summary>
			public const string 配送先コード = "配送先コード";
			/// <summary></summary>
			public const string 配送先名称 = "配送先名称";
			/// <summary></summary>
			public const string 運送業者コード = "運送業者コード";
			/// <summary></summary>
			public const string 出荷元コード = "出荷元コード";
			/// <summary></summary>
			public const string 指図ＮＯ = "指図ＮＯ";
			/// <summary></summary>
			public const string 指図ＮＯ枝番 = "指図ＮＯ枝番";
			/// <summary></summary>
			public const string 出報ＮＯ = "出報ＮＯ";
			/// <summary></summary>
			public const string 伝票ＮＯ = "伝票ＮＯ";
			/// <summary></summary>
			public const string 車ＮＯ = "車ＮＯ";
			/// <summary></summary>
			public const string 指図年月日 = "指図年月日";
			/// <summary></summary>
			public const string 出荷年月日 = "出荷年月日";
			/// <summary></summary>
			public const string 着荷年月日 = "着荷年月日";
			/// <summary></summary>
			public const string 扱区分 = "扱区分";
			/// <summary></summary>
			public const string 受入限月 = "受入限月";
			/// <summary></summary>
			public const string 供給限月 = "供給限月";
			/// <summary></summary>
			public const string 次別コード = "次別コード";
			/// <summary></summary>
			public const string 受入受渡条件コード = "受入受渡条件コード";
			/// <summary></summary>
			public const string 供給受渡条件コード = "供給受渡条件コード";
			/// <summary></summary>
			public const string 元伝ＮＯ = "元伝ＮＯ";
			/// <summary></summary>
			public const string 元伝年月日 = "元伝年月日";
			/// <summary></summary>
			public const string 事故摘要コード = "事故摘要コード";
			/// <summary></summary>
			public const string 分送区分 = "分送区分";
			/// <summary></summary>
			public const string ＪＡ発注ＮＯ = "ＪＡ発注ＮＯ";
			/// <summary></summary>
			public const string 伝票行ＮＯ = "伝票行ＮＯ";
			/// <summary></summary>
			public const string 指図行ＮＯ = "指図行ＮＯ";
			/// <summary></summary>
			public const string 取引先品名コード = "取引先品名コード";
			/// <summary></summary>
			public const string 商品グループコード = "商品グループコード";
			/// <summary></summary>
			public const string 品名コード = "品名コード";
			/// <summary></summary>
			public const string 容量コード = "容量コード";
			/// <summary></summary>
			public const string 荷姿コード = "荷姿コード";
			/// <summary></summary>
			public const string メーカコード = "メーカコード";
			/// <summary></summary>
			public const string 型式 = "型式";
			/// <summary></summary>
			public const string 品名名称 = "品名名称";
			/// <summary></summary>
			public const string 特別区分 = "特別区分";
			/// <summary></summary>
			public const string レス区分 = "レス区分";
			/// <summary></summary>
			public const string 予約当用区分 = "予約当用区分";
			/// <summary></summary>
			public const string 諸掛コード = "諸掛コード";
			/// <summary></summary>
			public const string 数量非計算区分 = "数量非計算区分";
			/// <summary></summary>
			public const string サイン = "サイン";
			/// <summary></summary>
			public const string 数量 = "数量";
			/// <summary></summary>
			public const string 金額区分 = "金額区分";
			/// <summary></summary>
			public const string 受入単価 = "受入単価";
			/// <summary></summary>
			public const string 受入金額 = "受入金額";
			/// <summary></summary>
			public const string 本部間単価 = "本部間単価";
			/// <summary></summary>
			public const string 本部間金額 = "本部間金額";
			/// <summary></summary>
			public const string 供給単価 = "供給単価";
			/// <summary></summary>
			public const string 供給金額 = "供給金額";
			/// <summary></summary>
			public const string 明細備考 = "明細備考";
			/// <summary></summary>
			public const string メーカ照合ＮＯ = "メーカ照合ＮＯ";
			/// <summary></summary>
			public const string 出報明細商品グループ別フィールド = "出報明細商品グループ別フィールド";
			/// <summary></summary>
			public const string 出報明細県別フィールド = "出報明細県別フィールド";
			/// <summary></summary>
			public const string 供給課税区分 = "供給課税区分";
			/// <summary></summary>
			public const string 受入課税区分 = "受入課税区分";
			/// <summary></summary>
			public const string 仮払総額区分 = "仮払総額区分";
			/// <summary></summary>
			public const string 仮受総額区分 = "仮受総額区分";
			/// <summary></summary>
			public const string 伝票消費税額サイン = "伝票消費税額サイン";
			/// <summary></summary>
			public const string 伝票仮受消費税額 = "伝票仮受消費税額";
			/// <summary></summary>
			public const string 伝票仮払消費税額 = "伝票仮払消費税額";
			/// <summary></summary>
			public const string 出報伝票商品グループ別フィールド = "出報伝票商品グループ別フィールド";
			/// <summary></summary>
			public const string 出報伝票県別フィールド = "出報伝票県別フィールド";
			/// <summary></summary>
			public const string 伝票備考 = "伝票備考";
		}

		/// <summary>
		/// データコンバートメイン
		/// </summary>
		public override void BgProcessDataConvert(object sender, DoWorkEventArgs e)
		{
			FormBg_Progress		bgform = (FormBg_Progress)sender;

			try
			{
				// フィールド枠・変換定義。
				fielddefs.Add(new FieldDef(ExportFormat.SEQ_NO,			convSeqNo,		FieldType.Number,	255,	AppCsvImportBugyo.区切));
				fielddefs.Add(new FieldDef(ExportFormat.県コード,		convCdKen,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.荷受人コード,	FDef_String,	FieldType.Number,	255,	AppCsvImportBugyo.得意先コード));
				fielddefs.Add(new FieldDef(ExportFormat.荷受人名称,		convWide,		FieldType.Number,	255,	AppCsvImportBugyo.得意先名));
				fielddefs.Add(new FieldDef(ExportFormat.配送先コード,	FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.配送先名称,		FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.運送業者コード,	FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.出荷元コード,	convCdMoto,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.指図ＮＯ,		convStringZero,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.指図ＮＯ枝番,	FDef_Null,		FieldType.Number,	255));

				fielddefs.Add(new FieldDef(ExportFormat.出報ＮＯ,		FDef_String,	FieldType.Number,	255,	AppCsvImportBugyo.伝票No));
				fielddefs.Add(new FieldDef(ExportFormat.伝票ＮＯ,		FDef_String,	FieldType.Number,	255,	AppCsvImportBugyo.伝票No));
				fielddefs.Add(new FieldDef(ExportFormat.車ＮＯ,			FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.指図年月日,		convDateNow,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.出荷年月日,		convDate,		FieldType.Number,	255,	AppCsvImportBugyo.売上日付));
				fielddefs.Add(new FieldDef(ExportFormat.着荷年月日,		FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.扱区分,			convStringOne,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.受入限月,		FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.供給限月,		FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.次別コード,		FDef_Null,		FieldType.Number,	255));

				fielddefs.Add(new FieldDef(ExportFormat.受入受渡条件コード,	FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.供給受渡条件コード,	FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.元伝ＮＯ,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.元伝年月日,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.事故摘要コード,		FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.分送区分,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.ＪＡ発注ＮＯ,		FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.伝票行ＮＯ,			convSubCd,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.指図行ＮＯ,			convSubCd,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.取引先品名コード,	convCdShohin,	FieldType.Number,	255,	AppCsvImportBugyo.商品コード));

				fielddefs.Add(new FieldDef(ExportFormat.商品グループコード,	FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.品名コード,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.容量コード,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.荷姿コード,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.メーカコード,		FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.型式,				FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.品名名称,			convShohin,	FieldType.Number,	255,		AppCsvImportBugyo.商品コード));
				fielddefs.Add(new FieldDef(ExportFormat.特別区分,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.レス区分,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.予約当用区分,		convStringOne,	FieldType.Number,	255));

				fielddefs.Add(new FieldDef(ExportFormat.諸掛コード,			FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.数量非計算区分,		convSoryo,		FieldType.Number,	255,	AppCsvImportBugyo.商品コード));
				fielddefs.Add(new FieldDef(ExportFormat.サイン,				FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.数量,				FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.金額区分,			convSoryo,		FieldType.Number,	255,	AppCsvImportBugyo.商品コード));
				fielddefs.Add(new FieldDef(ExportFormat.受入単価,			FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.受入金額,			convKingaku,	FieldType.Number,	255,	AppCsvImportBugyo.商品コード));
				fielddefs.Add(new FieldDef(ExportFormat.本部間単価,			FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.本部間金額,			FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.供給単価,			FDef_Null,		FieldType.Number,	255));
				
				fielddefs.Add(new FieldDef(ExportFormat.供給金額,			FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.明細備考,			FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.メーカ照合ＮＯ,		FDef_Null,		FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.出報明細商品グループ別フィールド,	FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.出報明細県別フィールド,	FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.供給課税区分,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.受入課税区分,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.仮払総額区分,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.仮受総額区分,			FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.伝票消費税額サイン,		FDef_Null,	FieldType.Number,	255));

				fielddefs.Add(new FieldDef(ExportFormat.伝票仮受消費税額,		FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.伝票仮払消費税額,		FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.出報伝票商品グループ別フィールド,	FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.出報伝票県別フィールド,	FDef_Null,	FieldType.Number,	255));
				fielddefs.Add(new FieldDef(ExportFormat.伝票備考,				FDef_Null,	FieldType.Number,	255));

				//fielddefs.Add(new FieldDef(ExportFormat.スタッフコード,	convertCode,	FieldType.String,	255,	t_staff.FCD_Staff));
				//fielddefs.Add(new FieldDef(ExportFormat.スタッフ名,		FDef_String,	FieldType.String,	255,	t_staff.FSTF_Name));
				//fielddefs.Add(new FieldDef(ExportFormat.フリガナ,		FDef_String,	FieldType.String,	255,	t_staff.FSTF_NameFurigane));
				//fielddefs.Add(new FieldDef(ExportFormat.旧姓,			FDef_String,	FieldType.String,	255,	t_staff.FSTF_NameOld));
				//fielddefs.Add(new FieldDef(ExportFormat.性別,			convertSex,		FieldType.String,	255));
				//fielddefs.Add(new FieldDef(ExportFormat.職種区分,		convertJob,		FieldType.String,	255));
				//fielddefs.Add(new FieldDef(ExportFormat.職務コード,		convertSMCode,	FieldType.String,	255));
				//fielddefs.Add(new FieldDef(ExportFormat.職務名,			convertSMName,	FieldType.String,	255));

				//fielddefs.Add(new FieldDef(ExportFormat.現況,			convertGenkyo,	FieldType.String,	255));
				//fielddefs.Add(new FieldDef(ExportFormat.生年月日,		formatDate,		FieldType.String,	255,	t_staff.FSTF_DateBirthday));
				//fielddefs.Add(new FieldDef(ExportFormat.入社日,			formatDate,		FieldType.String,	255,	t_staff.FSTF_DateNyusha));
				//fielddefs.Add(new FieldDef(ExportFormat.退社日,			formatDate,		FieldType.String,	255,	t_staff.FSTF_DateTaishoku));

				//fielddefs.Add(new FieldDef(ExportFormat.電話番号,		FDef_String,	FieldType.String,	255,	t_staff.FSTF_Tel1));
				//fielddefs.Add(new FieldDef(ExportFormat.携帯番号,		FDef_String,	FieldType.String,	255,	t_staff.FSTF_Tel2));
				//fielddefs.Add(new FieldDef(ExportFormat.Email,			FDef_String,	FieldType.String,	255,	t_staff.FSTF_Mail1));
				//fielddefs.Add(new FieldDef(ExportFormat.携帯メール,		FDef_String,	FieldType.String,	255,	t_staff.FSTF_Mail2));
				//fielddefs.Add(new FieldDef(ExportFormat.郵便番号,		FDef_String,	FieldType.String,	255,	t_staff.FSTF_Post));
				//fielddefs.Add(new FieldDef(ExportFormat.住所１,			FDef_String,	FieldType.String,	255,	t_staff.FSTF_Addr1));
				//fielddefs.Add(new FieldDef(ExportFormat.住所２,			FDef_String,	FieldType.String,	255,	t_staff.FSTF_Addr2));

				//fielddefs.Add(new FieldDef(ExportFormat.銀行コード,		FDef_String,		FieldType.String,	255,	t_staff.FSTF_BankCode));
				//fielddefs.Add(new FieldDef(ExportFormat.銀行名,			convertBankName,	FieldType.String,	255));
				//fielddefs.Add(new FieldDef(ExportFormat.支店コード,		FDef_String,		FieldType.String,	255,	t_staff.FSTF_BankShitenCode));
				//fielddefs.Add(new FieldDef(ExportFormat.支店名,			convertShitenName,	FieldType.String,	255));
				//fielddefs.Add(new FieldDef(ExportFormat.口座種別,		convertKozaType,	FieldType.String,	255));
				//fielddefs.Add(new FieldDef(ExportFormat.口座番号,		FDef_String,		FieldType.String,	255,	t_staff.FSTF_BankKozaNo));
				//fielddefs.Add(new FieldDef(ExportFormat.口座名義,		FDef_String,		FieldType.String,	255,	t_staff.FSTF_BankKozaName));

				//fielddefs.Add(new FieldDef(ExportFormat.提出先コード,	convertTeishutsusakiCD,	FieldType.String,	255));
				//fielddefs.Add(new FieldDef(ExportFormat.提出先名,		convertTeishutsusakiName,	FieldType.String,	255));


				//■ 変換後テーブル作成。
				DataTable	dt = MakeDataTable();

				//■ データを変換して、変換後テーブルに順次格納していく。
				AppendConvertedData(dbview, dt, bgform, 0, 80);

				//■ CSV の作成。
				csvtext = MakeCsv(dt, bgform, 80, 15,true, true);
				
				// 一旦コンバート成功としておく。
				convresult = ConvertStatus.Success;
			}
			catch(Exception ex)
			{
				ErrLog.WriteException(ex);
				convresult = ConvertStatus.ConvertFailed;
			}
			
			//▽▽▽ 進捗の完了。
			if (bgform != null) bgform.FinishProgress();
		}

		protected string convWide(DBView dv, params object[] args)
		{
			return StrConv.ToWide(FDef_String(dv, args));
		}

		int sub_cd;

		protected string convSeqNo(DBView dv, params object[] args)
		{
			string str = FDef_String(dv, args);

			// サブコードのカウンター
			if (str == "*")
			{
				// *が親になるのでカウンターをリセット
				sub_cd = 1;
			}
			else
			{
				//　子の場合はカウントアップ
				sub_cd++;
			}

			// カレント行数を返す
			return $"{dv.Row + 1}";
		}

		// サブコードの設定
		protected string convSubCd(DBView dv, params object[] args)
		{
			return $"{sub_cd}";
		}

		protected string convCdKen(DBView dv, params object[] args)
		{
			return CodeKen;
		}

		protected string convCdShohin(DBView dv, params object[] args)
		{
			string code = FDef_String(dv, args);

			// 最初の6桁取得
			if (code.Length > 6) code = code.Substring(0,6);

			return code;
		}

		protected string convCdMoto(DBView dv, params object[] args)
		{
			return CodeShukamoto;
		}

		protected string convStringZero(DBView dv, params object[] args)
		{
			return "0";
		}

		protected string convStringOne(DBView dv, params object[] args)
		{
			return "1";
		}

		protected string convDate(DBView dv, params object[] args)
		{
			return Regex.Replace(FDef_String(dv,args),"/","");
		}

		protected string convDateNow(DBView dv, params object[] args)
		{
			return DateTime.Now.ToString("yyyyMMdd");
		}

		const string SHOHIN_SORYO_CODE = "566907";

		protected string convSoryo(DBView dv, params object[] args)
		{
			string code = FDef_String(dv, args);

			if (code.IndexOf(SHOHIN_SORYO_CODE) == 0)
			{
				return "1";
			}

			return "0";
		}

		protected string convKingaku(DBView dv, params object[] args)
		{
			string code = FDef_String(dv, args);

			if (code.IndexOf(SHOHIN_SORYO_CODE) == 0)
			{
				/// 送料なら金額セット
				return Cast.String(dv.CurrentRow[AppCsvImportBugyo.売価金額]);
			}

			return null;
		}

		protected string convShohin(DBView dv, params object[] args)
		{
			string code = FDef_String(dv, args);
			string name = Cast.String(dv.CurrentRow[AppCsvImportBugyo.商品名]);

			if (code.Length > 6)
			{
				// 2～6番目の文字だけ取得
				code = code.Substring(2,4);

				Shohin sh = sm.GetByCode(code);

				if (sh != null)
				{
					return $"{sh.Name}{name}";
				}
			}

			return null;
		}
	}
}
