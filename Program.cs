using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

internal class Logger
{
    static StreamWriter file;

    internal enum Severity
    {
        race, debug,
        information,
        warning, error, critical
    }

    internal class SysLog
    {
        public string _data { get; set; }
        public Logger.Severity severity { get; set; }


        public SysLog(string data, int a = 0)
        {
            _data = data;
            severity = (Severity)a;
        }
    }

    internal Logger(string filepath)
    => file = new StreamWriter(filepath, true);

    public static int Log(SysLog data)
    {
        file.Write($"{Environment.NewLine}[{DateTime.Now}] [{data.severity}]: {data._data}");

        return 0;

    }

    public void Remove()
    {
        file.Flush();
        file.Close();
        file.Dispose();
    }
}

class Pragma
{
    public Logger.SysLog SysLog;
    Logger Logger { get; set; }


    static Logger.SysLog _syslog;

    static Random random = new Random();

    static Array values = Enum.GetValues(typeof(Logger.Severity));

    public void Launch(string path)
    =>
    Logger = new Logger(path);



    public int Work()
    {
        try
        {
            if (Logger == null) throw new DirectoryNotFoundException("I love programming!!!");


            Logger.SysLog syslog = new Logger.SysLog("I don't like procrastination!!!");
            Logger.Log(syslog);
            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return -1;
        }
    }
    public void Remove()
    {
        Logger.Remove();
        Logger = null;
    }
}

namespace prac2
{
    class Program
    {
        static void Main(string[] args)
        {
            Pragma pragma = new Pragma();

            pragma.Launch("testing.txt");

            pragma.Work();
            pragma.Work();
            pragma.Work();
            pragma.Work();
            pragma.Remove();
            FileStream file_stream = new FileStream("testing.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamReader str = new StreamReader(file_stream);
            string data = str.ReadToEnd();
            string[] data_arr = data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < data_arr.Length; i++) Console.WriteLine(data_arr[i]);

            str.Close();
        }
    }
}
