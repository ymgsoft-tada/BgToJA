using ComponentGControlDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// 入力ルール
	/// </summary>
	public class AppDbRule
	{
		/// <summary>
		/// ルールクラス
		/// </summary>
		public static GControlDBRule Rule { get; private set; }

		/// <summary>合計用ルール名</summary>
		public const string RuleCurrencyTotal = "Currency_Total";

		/// <summary>金額用ルール名</summary>
		public const string RuleCurrency = "Currency";

		/// <summary>金額用ルール名</summary>
		public const string RuleCurrency9 = "Currency9";

		/// <summary>金額用ルール名</summary>
		public const string RuleCurrency9_1 = "Currency9_1";

		/// <summary>少数点第1用</summary>
		public const string RuleRatio1 = "Ratio1";
		/// <summary>少数点第2用</summary>
		public const string RuleRatio2 = "Ratio2";
		/// <summary>少数点第3用</summary>
		public const string RuleRatio3 = "Ratio3";
		/// <summary>半角20文字</summary>
		public const string RuleHulf20 = "RuleHulf20";
		/// <summary>半角200文字</summary>
		public const string RuleHulf200 = "RuleHulf200";

		public const int Code6 = 6;

		/// <summary>
		/// 初期化処理
		/// </summary>
		public static void Initialize()
		{
			Rule = new GControlDBRule();

			GControlDBRuleNumber code = new GControlDBRuleNumber("Code", Code6, 0);
			code.Align[0] = System.Drawing.ContentAlignment.BottomLeft;
			Rule.Add(code);

			GControlDBRuleNumber code4 = new GControlDBRuleNumber("Code4", 4, 0);
			code4.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code4);

			GControlDBRuleNumber code3 = new GControlDBRuleNumber("Code3", 3, 0);
			code3.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code3);

			GControlDBRuleNumber code7 = new GControlDBRuleNumber("Code7", 7, 0);
			code7.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code7);

			GControlDBRuleNumber code10 = new GControlDBRuleNumber("Code10", 10, 0);
			code10.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code10);

			GControlDBRuleNumber code12 = new GControlDBRuleNumber("Code12", 12, 0);
			code12.Align[0] = System.Drawing.ContentAlignment.BottomRight;
			Rule.Add(code12);

			// 合計値フィールド
			GControlDBRuleCurrency total = new GControlDBRuleCurrency(RuleCurrencyTotal, 11, 0);
			total.Align[0] = ContentAlignment.TopRight;
			Rule.Add(total);

			GControlDBRuleCurrency curr9 = new GControlDBRuleCurrency(RuleCurrency9, 9, 0);
			curr9.Align[0] = ContentAlignment.BottomRight;
			curr9.AlternateBlank[0] = true;
			Rule.Add(curr9);

			GControlDBRuleCurrency curr9_1 = new GControlDBRuleCurrency(RuleCurrency9_1, 9, 1);
			curr9_1.Align[0] = ContentAlignment.BottomRight;
			curr9_1.AlternateBlank[0] = true;
			Rule.Add(curr9_1);

			GControlDBRuleCurrency curr = new GControlDBRuleCurrency(RuleCurrency, 7, 0);
			curr.Align[0] = ContentAlignment.BottomRight;
			curr.AlternateBlank[0] = true;
			Rule.Add(curr);

			GControlDBRuleNumber r1 = new GControlDBRuleNumber(RuleRatio1, 1,1);
			Rule.Add(r1);

			GControlDBRuleNumber r2 = new GControlDBRuleNumber(RuleRatio2, 3,2);
			Rule.Add(r2);

			GControlDBRuleNumber r3 = new GControlDBRuleNumber(RuleRatio3, 3,3);
			Rule.Add(r3);

			// 郵便番号(住所上段を30桁）
			GControlDBRulePostAddr post = new GControlDBRulePostAddr();
			post.MaxLength[2] = 30;

			// 半角カナのフィールド
			GControlDBRuleText kana = new GControlDBRuleText("Furigana", 40);
			kana.Format[0] = "H";
			kana.AllowSpace[0] = GrapeCity.Win.Editors.AllowSpace.Narrow;
			kana.ImeMode[0]	= ImeMode.KatakanaHalf;
			Rule.Add(kana);

			GControlDBRuleText kana20 = new GControlDBRuleText("Furigana20", 20);
			kana20.Format[0] = "H";
			kana20.AllowSpace[0] = GrapeCity.Win.Editors.AllowSpace.Narrow;
			kana20.ImeMode[0]	= ImeMode.KatakanaHalf;
			Rule.Add(kana20);

			GControlDBRuleText kana60 = new GControlDBRuleText("Furigana60", 60);
			kana60.Format[0] = "H";
			kana60.AllowSpace[0] = GrapeCity.Win.Editors.AllowSpace.Narrow;
			kana60.ImeMode[0]	= ImeMode.KatakanaHalf;
			Rule.Add(kana60);

			GControlDBRuleText kana30 = new GControlDBRuleText("Furigana30", 30);
			kana30.Format[0] = "H";
			kana30.AllowSpace[0] = GrapeCity.Win.Editors.AllowSpace.Narrow;
			kana30.ImeMode[0]	= ImeMode.KatakanaHalf;
			Rule.Add(kana30);


			GControlDBRuleText eisu = new GControlDBRuleText("Eisu", 40);
			eisu.ImeMode[0] = ImeMode.Off;
			Rule.Add(eisu);

			GControlDBRuleText eisu20 = new GControlDBRuleText(RuleHulf20, 20);
			eisu20.ImeMode[0] = ImeMode.Off;
			Rule.Add(eisu20);

			GControlDBRuleText eisu200 = new GControlDBRuleText(RuleHulf200, 200);
			eisu200.ImeMode[0] = ImeMode.Off;
			Rule.Add(eisu200);

			GControlDBRuleText hankaku = new GControlDBRuleText(80);
			hankaku.ImeMode[0]	= ImeMode.Disable;

			GControlDBRuleText numStr = new GControlDBRuleText(15);
			numStr.ImeMode[0]	= ImeMode.Disable;

			GControlDBRuleText numStr13 = new GControlDBRuleText(13);
			numStr13.ImeMode[0]	= ImeMode.Disable;

			GControlDBRuleText numStr12 = new GControlDBRuleText(12);
			numStr12.ImeMode[0]	= ImeMode.Disable;

			GControlDBRuleText memo = new GControlDBRuleText("Memo", 100);
			memo.Align[0] = ContentAlignment.TopLeft;

			GControlDBRuleText memo20 = new GControlDBRuleText("Memo", 20);
			memo20.Align[0] = ContentAlignment.TopLeft;

			GControlDBRuleText memo10 = new GControlDBRuleText("Memo", 10);
			memo10.Align[0] = ContentAlignment.TopLeft;
			GControlDBRuleText tekiyo = new GControlDBRuleText("Tekiyo", 20);
			tekiyo.Align[0] = ContentAlignment.TopLeft;
			tekiyo.HighlightText[0] = false;


			// 検索用などの日付入力フィールド
			Rule.Add(new GControlDBRuleDate("Date"));

		}

		/// <summary>
		/// コントロールに設定されたルールを適用します。
		/// </summary>
		/// <param name="ctl">適用させるコントロール</param>
		/// <param name="rowfield">フィールド名</param>
		public static void SetControlByRule(Control ctl, string rowfield)
		{
			SetControlByRule(ctl, rowfield, "");
		}
		
		/// <summary>
		/// コントロールに設定されたルールを適用します。
		/// </summary>
		/// <param name="ctl">適用させるコントロール</param>
		/// <param name="rowfield">フィールド名</param>
		/// <param name="datatag">データタグ名</param>
		public static void SetControlByRule(Control ctl, string rowfield, string datatag)
		{
			Control[] ctls = { ctl };
			
			SetControlByRule(ctls, rowfield, datatag);
		}
		
		/// <summary>
		/// コントロールに設定されたルールを適用します。
		/// </summary>
		/// <param name="ctls">適用させるコントロール</param>
		/// <param name="rowfield">フィールド名</param>
		public static void SetControlByRule(Control[] ctls, string rowfield)
		{
			SetControlByRule(ctls, rowfield, "");
		}
		
		/// <summary>
		/// コントロールに設定されたルールを適用します。
		/// </summary>
		/// <param name="ctls">適用させるコントロール</param>
		/// <param name="rowfield">フィールド名</param>
		/// <param name="datatag">データタグ名</param>
		public static void SetControlByRule(Control[] ctls, string rowfield, string datatag)
		{
			GControlDBRuleBase	rbase = Rule.GetRule(rowfield, datatag);
			
			if (rbase != null)
			{
				rbase.SetControlByRule(ctls, false);
			}
		}
	}
}
