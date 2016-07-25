// IntPtrを扱うのに必要
using System;

// SendKeysを扱うのに必要（別途Project->Add ReferenceからSystem.Windows.Formsを追加する必要がある）
using System.Windows.Forms;

// Processを扱うのに必要
using System.Diagnostics;

// DllImportを使用するのに必要
using System.Runtime.InteropServices;

// 文字入力
public class SendKeyUtil {
	// 外部プロセスのメイン・ウィンドウを起動するためのWin32 API
	[DllImport("user32.dll")]
	private static extern bool SetForegroundWindow(IntPtr hWnd);
	
    private Process process;
 
    public SendKeyUtil(string windowName) {
        this.process = getProcessByWindowName(windowName);
    }

    public void activeWindow() {
		if(this.process != null) {
			SetForegroundWindow(this.process.MainWindowHandle);
		}
    }
    
     public bool Type(string typeString) {
		if(this.process != null) {
			SendKeys.SendWait(typeString+"{ENTER}");
			return true;
		}
    	return false;
    }
       
	// ウィンドウ名からプロセスを取得
	private static Process getProcessByWindowName(string WindowName) {
		Process[] allProcesses = Process.GetProcesses();
//		Debug.WriteLine("{0}個見つかりました", allProcesses.Length);
		foreach (Process oneProcess in allProcesses)
		{
			
//			Debug.WriteLine("ID：", oneProcess.Id);
//			Debug.WriteLine("ウィンドウハンドル：", oneProcess.MainWindowHandle);
//			Debug.WriteLine("タイトル：", oneProcess.MainWindowTitle);
			if(oneProcess.MainWindowTitle.IndexOf(WindowName) > -1) {
				return oneProcess;
			}
		}
		return null;
	}
}