using System.IO;
public class FileLog
{
    /// <summary>
    /// ログファイルに追記
    /// </summary>
    /// <param name="filename">ファイル名</param>
    /// <param name="text">追記するテキスト</param>
    public static void AppendLog(string filename, string text)
    {
        StreamWriter sw = null;
        try
        {
            completeDirectory(Path.GetDirectoryName(filename));
            sw = new StreamWriter(filename, true, System.Text.Encoding.UTF8);
            sw.Write(text);
        }
        finally
        {
            sw?.Close();
        }
    }
    /// <summary>
    /// 指定ディレクトリが存在しない場合、上から辿って作成する
    /// </summary>
    /// <param name="dir">指定ディレクトリ</param>
    /// <returns>true..作成した</returns>
    static bool completeDirectory(string dir)
    {
        if (string.IsNullOrEmpty(dir) == true)
        {
            return false;
        }
        if (Directory.Exists(dir) == false)
        {
            completeDirectory(Path.GetDirectoryName(dir));
            Directory.CreateDirectory(dir);
            return true;
        }

        return false;
    }
}