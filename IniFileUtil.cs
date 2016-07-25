// DllImportを使用するのに必要
using System.Runtime.InteropServices;

// StringBuilderを扱うのに必要
using System.Text;

// INIファイルを読み書き
/*
  * 使い方
  *       ini.writeValue("Pageant", "PageantPath", "C:\\～\\putty\\pageant.exe");
  *       for (int i = 0; i < 10; i++) {
  *               ini.writeValue("Pageant", "SecretKey"+i, "sshのフォルダパス");
  *               ini.writeValue("Pageant", "Passphrase"+i, "sshのパスフレーズ");
  *       }
  *       Console.WriteLine(ini.readValue("Pageant", "PageantPath"));
  *       for (int i = 0; i < 10; i++) {
  *               Console.WriteLine(ini.readValue("Pageant", "SecretKey"+i));
  *               Console.WriteLine(ini.readValue("Pageant", "Passphrase"+i));
  *       }
  * */
public class IniFileUtil {
    // GetPrivateProfileStringを扱うのに必要
    [DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileString(
        string lpApplicationName, 
        string lpKeyName, 
        string lpDefault, 
        StringBuilder lpReturnedstring, 
        int nSize, 
        string lpFileName);
 
    // WritePrivateProfileStringを扱うのに必要
    [DllImport("kernel32.dll")]
    private static extern int WritePrivateProfileString(
        string lpApplicationName,
        string lpKeyName, 
        string lpstring, 
        string lpFileName);
 
    private string filePath;
 
    // ファイルが存在しない場合は初回書き込み時に作成されます。
    public IniFileUtil(string filePath) {
        this.filePath = filePath;
    }
  
    public void writeValue(string section, string key, string keyValue) {
        WritePrivateProfileString(section, key, keyValue, this.filePath);
    }

    public string readValue(string section, string key) {
        StringBuilder sb = new StringBuilder(256);
        GetPrivateProfileString(section, key, string.Empty, sb, sb.Capacity, this.filePath);
        return sb.ToString();
    }
}