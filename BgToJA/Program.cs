using ComponentDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	static class Program
	{

		private static System.Threading.Mutex _mutex;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//■ 二重起動をチェックする。
			_mutex = new System.Threading.Mutex(false, AppConst.AppName);

			// ミューテックスの所有権を要求する。
			if (_mutex.WaitOne(0, false) == false)
			{
				// 既に開いているプロセスをアクティブに。
				ShowPrevProcess();
				return;
			}

			//エラーログ
			ErrLog.ErrorLogFilePath = @"err.log";
			ErrLog.Initialize();

			// ルール初期化
			AppDbRule.Initialize();

			Form frm = null;

			try
			{
				frm = new FormMain();
				Application.Run(frm);
			}
			catch(Exception ex)
			{
				MessageBox.Show(
					frm, 
					"予期せぬエラーが発生したため、アプリケーションを終了します。", 
					"エラー", 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Error);

				ErrLog.WriteException(ex);
			}
		}

		/// <summary>
		/// 既に開いているプロセスをアクティブに。
		/// </summary>
		static bool ShowPrevProcess()
		{
			Process hThisProcess	= Process.GetCurrentProcess();
			Process[] hProcesses	= Process.GetProcessesByName(hThisProcess.ProcessName);
			int iThisProcessId		= hThisProcess.Id;
			
			foreach (Process hProcess in hProcesses)
			{
				if (hProcess.Id != iThisProcessId)
				{
					AppApi.ShowWindow(hProcess.MainWindowHandle, AppApi.SW_NORMAL);
					AppApi.SetForegroundWindow(hProcess.MainWindowHandle);
					return true;
				}
			}
			
			return false;
		}
	}
}
