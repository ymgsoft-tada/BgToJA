using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace App
{
	/// <summary>
	/// [作成者 K.Tada]
	/// Apiに関するメソッドクラス群
	/// </summary>
	public class AppApi
	{
		#region *** Apiの定数群 ***
		/// <summary>
		/// 通常サイズで表示しフォーカスをあたえる
		/// </summary>
		public const int	SW_SHOW = 5;
		/// <summary>
		/// AllUsers\Documents
		/// </summary>
		public const Int32 CSIDL_COMMON_DOCUMENTS = 0x002e;
		/// <summary>
		/// フォルダの現在のパス名を返します。
		/// </summary>
		public const UInt32 SHGFP_TYPE_CURRENT = 0;
		/// <summary></summary>
		public const int SW_NORMAL = 1;
		#endregion

		#region *** Window関連 ***
		/// <summary>
		/// ウィンドウの使用可否を設定するAPI関数
		/// </summary>
		/// <param name="hWnd">ウインドウハンドル</param>
		/// <param name="enabled">使用可否</param>
		[DllImport("user32.dll")]
		public static extern bool EnableWindow(IntPtr hWnd, bool enabled);
		/// <summary>
		/// ウィンドウを最前面に表示するAPI関数
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);
		/// <summary>
		/// ウィンドウを探します。
		/// </summary>
		/// <param name="lpClassName">クラス名</param>
		/// <param name="lpWindowName">ウィンドウ名</param>
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);		
		/// <summary>
		/// ウインドウの可視状態を取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		[DllImport("user32.dll")]
		public static extern bool IsWindowVisible(IntPtr hWnd);
		/// <summary>
		/// ウィンドウを表示します。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <param name="nCmdShow">表示するウィンドウ</param>
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		/// <summary>
		/// CSIDL 値から、対応するフォルダのパス名を取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <param name="nFolder">取得するフォルダを識別</param>
		/// <param name="hToken">特定のユーザーを表すのに使用されるアクセストークン</param>
		/// <param name="dwFlags">どちらのパスが返されるかを指定するフラグを指定します。</param>
		/// <param name="pszPath">パス名を表わすヌル終端文字列を格納するバッファのアドレスを指定します。</param>
		/// <returns>0..成功</returns>
		[DllImport("shell32.dll")]
		public static extern Int32 SHGetFolderPath(IntPtr hWnd, Int32 nFolder,	IntPtr hToken, UInt32 dwFlags, System.Text.StringBuilder pszPath);			
		#endregion
	}
}
