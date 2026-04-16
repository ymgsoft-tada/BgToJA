using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Forms;

namespace App
{
	/// <summary>
	/// [作成者 K.Tada]
	/// カスタムインストールクラス
	/// </summary>
	[System.ComponentModel.RunInstaller(true)]
	public class AppPostInstall : System.Configuration.Install.Installer
	{
		/// <summary>
		/// アンインストール後のイベント
		/// </summary>
		protected override void OnAfterUninstall(System.Collections.IDictionary savedState)
		{
		}

		/// <summary>
		/// ロールバック後のイベント
		/// </summary>
		/// <param name="savedState"></param>
		protected override void OnAfterRollback(System.Collections.IDictionary savedState)
		{
			base.OnAfterRollback(savedState);
		}

		/// <summary>
		/// インストール後のイベント
		/// </summary>
		protected override void OnAfterInstall(System.Collections.IDictionary savedState)
		{
			base.OnAfterInstall(savedState);
			
			string dir		 = Path.GetDirectoryName(this.Context.Parameters["dir"]);
			string regkey    = this.Context.Parameters["regkey"];
			string enckey    = this.Context.Parameters["enckey"];
			
			//MessageBox.Show(dir);
			//MessageBox.Show(regkey);

			
			// インストールフォルダとレジストリの権限設定
			AddAccessDirectory(dir);
			AddAccessRegistry(regkey);
			AddAccessRegistry(enckey);
			
			//RegCommon.SetMasterKey(AppLicense.RegMasterKey);
			
			//// 情報がなければ体験版の情報を作成。
			//if (RegCommon.Read(AppLicense.RegTestMode) == null)
			//{
			//	AppLicense.Initialize();
			//	AppLicense.SetLicense(DateTime.Now.AddDays(AppLicense.RegTestDays));
			//}

			//¶ 2017/12/21 kj　以下のファイルはC1(Ver2017)からファイルバージョンが旧Dll(Ver2014)よりも低いナンバリングになっているため、強制上書きする。
			// パッチ処理
			//List<string> dlls = new List<string>()
			//{
			//	"C1.C1Excel.2.dll",
			//	"C1.C1Report.2.dll",
			//	"C1.Win.C1Report.2.dll",
			//};

			//foreach (string dll in dlls)
			//{
			//	string srcpath = string.Format(@"{0}\patch\{1}", dir, dll);
			//	string dstpath = string.Format(@"{0}\{1}", dir, dll);

			//	//MessageBox.Show(srcpath);
			//	//MessageBox.Show(dstpath);

			//	// 対象ファイルが存在する場合に上書き
			//	if (File.Exists(dstpath) == true)
			//	{
			//		FileInfo src_fi = new FileInfo(srcpath);
			//		FileInfo dst_fi = new FileInfo(dstpath);

			//		if (src_fi.Length != dst_fi.Length)
			//		{
			//			File.Copy(srcpath, dstpath, true);
			//		}
			//	}
			//}
			//¶ 2017/12/21 kj
		}

		/// <summary>
		/// 指定したフォルダを削除します。
		/// </summary>
		/// <param name="path"></param>
		void removeDir(string path)
		{
			if (Directory.Exists(path) == true)
			{
				Directory.Delete(path, true);
			}
		}

		#region *** Private Method ***
		/// <summary>
		/// インストールフォルダに対し、フルコントロール権限を付与します。
		/// </summary>
		/// <param name="path">インストールフォルダ</param>
		bool AddAccessDirectory(string path)
		{
			DirectoryInfo		info = new DirectoryInfo(path);			
			DirectorySecurity	sec = info.GetAccessControl();
			
			// フォルダ下に対し、Everyoneフルコントロールとする。
			FileSystemAccessRule rule;
			
			rule = new FileSystemAccessRule(
							"Everyone",
							FileSystemRights.FullControl, 
							InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
							PropagationFlags.None,
							AccessControlType.Allow
							);
			try
			{
				sec.AddAccessRule(rule);
				info.SetAccessControl(sec);
			}
			catch(Exception ex)
			{
//				MessageBox.Show(ex.Message);
				Debug.WriteLine(ex.Message);
				return false;
			}
			
			return true;
		}

		/// <summary>
		/// HKLM下のレジストリに、フルコントロールの権限を付与します。
		/// </summary>
		bool AddAccessRegistry(string path)
		{
			if (string.IsNullOrEmpty(path) == false)
			{
				RegistryKey			regkey	= Registry.LocalMachine.CreateSubKey(path);		
				RegistrySecurity	rs		= new RegistrySecurity();			
				RegistryAccessRule	rule	= new RegistryAccessRule(
													"Everyone", 
													RegistryRights.FullControl, 
													InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, 
													PropagationFlags.None, 
													AccessControlType.Allow
													);
				try
				{
					rs.AddAccessRule(rule);
					regkey.SetAccessControl(rs);
				}
				catch(Exception ex)
				{
	//				MessageBox.Show(ex.Message);
					Debug.WriteLine(ex.Message);
					return false;
				}
			}
			
			return true;
		}
		#endregion

	}
}
