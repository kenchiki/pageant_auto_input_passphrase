// ProcessStartInfo,Processを扱うのに必要
using System.Diagnostics;

// プログラム起動
public class ExecuteUtil {
    private Process process;
 
    public ExecuteUtil(string filePath) {
        this.process = new Process();
        this.process.StartInfo.FileName = filePath;
    }

    public void exe() {
    	this.process.StartInfo.Arguments = "";
		this.process.Start();
    }
    
    public void exeArg(string arg) {
//    	""で囲まなければパスに空白が入っているとエラーになる
    	this.process.StartInfo.Arguments = "\"" + arg + "\"";
//    	ちなみにSystem.Diagnostics.Process.Start(ProcessStartInfo)のstaticで起動すると複数起動できるらしい
		this.process.Start();

    }
}