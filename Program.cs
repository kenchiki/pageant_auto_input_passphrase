/*
 * Created by SharpDevelop.
 * User: Nagai Kenji
 * Date: 2016/07/25
 * Time: 8:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
// STAThreadを扱うのに必要
using System;

// Applicationを扱うのに必要
using System.Windows.Forms;

// Tasksを扱うのに必要（別途Project->Add ReferenceからSystem.Threading.Tasksを追加する必要がある）
using System.Threading.Tasks;

// Debugを扱うのに必要（別途Project->Add ReferenceからSystem.Diagnostics.Debugを追加する必要がある）
using System.Diagnostics;

namespace pageant
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args) {
			startMacro();

			// Mainが最後まで行った際にアプリが終了してしまうのでApplication.Runで起動した状態をキープ
            Application.Run();
		}

		private static async void startMacro() {
			IniFileUtil ini = new IniFileUtil("./setting.ini");

			string pageantPath = ini.readValue("Pageant", "PageantPath");
			ExecuteUtil exe = new ExecuteUtil(pageantPath);
			
			// 多重起動させないため最初に秘密鍵引数なしで起動しておく（鍵の引数があると多重起動してしまうため）
			exe.exe();
			
			for (int i = 0; i < 10; i++) {
				// 秘密鍵が空だったら飛ばす
				string secretKeyPath = ini.readValue("Pageant", "SecretKey"+i);
				if (String.IsNullOrEmpty(secretKeyPath) == true) {
					continue;
				}

				// cmdでいう「"～\pageant.exe" "～/kenchiki_kumagoro.ppk"」を実行
				exe.exeArg(secretKeyPath);
				await Task.Delay(500);
				SendKeyUtil passphraseWindow = new SendKeyUtil("Pageant: Enter Passphrase");
				passphraseWindow.activeWindow();
				string passphrase = ini.readValue("Pageant", "Passphrase"+i);
				passphraseWindow.Type(passphrase);
				Debug.WriteLine("入力完了", "SecretKey"+i);
        	}
			Environment.Exit(0);// 終了
		}
		
		
	}
}
