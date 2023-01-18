using System;
using System.Collections.Generic;
using System.IO;

public class Logging
{
    public const int VERBOSE = 0;
    public const int DEBUG = 10;
    public const int INFO = 20;
    public const int WARN = 30;
    public const int ERROR = 40;
    public const int CRITICAL = 50;

    private static Dictionary<int, string> levelNames = new Dictionary<int, string>();

    private static int LogLevel;
    private static string? LogFile;

    public static void Config(string path = "logs", int logLevel = INFO)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        LogFile = Path.Combine(path, string.Concat("log_", DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss"), ".txt"));

        if (!File.Exists(LogFile))
        {
            File.Create(LogFile).Close();
        }

        LogLevel = logLevel;
        AddLogLevel(VERBOSE, "VERBOSE");
        AddLogLevel(DEBUG, "DEBUG");
        AddLogLevel(INFO, "INFO");
        AddLogLevel(WARN, "WARN");
        AddLogLevel(ERROR, "ERROR");
        AddLogLevel(CRITICAL, "CRITICAL");
    }

    public static void AddLogLevel(int level, string name)
    {
        levelNames.Add(level, name);
    }

    public static void Log(int level, string message)
    {
        if (string.IsNullOrEmpty(LogFile))
        {
            Config();
        }

        if (level > LogLevel)
        {
            if (levelNames.TryGetValue(level, out var name))
            {
                string stamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");

                File.AppendAllText(LogFile, string.Concat("[", name, "] ", stamp, ": ", message));
            }
        }
    }

    public static void Verbose(string message)
    {
        Log(VERBOSE, message);
    }

    public static void Debug(string message)
    {
        Log(DEBUG, message);
    }

    public static void Info(string message)
    {
        Log(INFO, message);
    }

    public static void Warn(string message)
    {
        Log(WARN, message);
    }

    public static void Error(string message)
    {
        Log(ERROR, message);
    }

    public static void Exception(Exception e, string message)
    {
        Log(ERROR, string.Concat(message, "\n Exception: ", e.Message, "\n Stacktrace: ", e.StackTrace));
    }

    public static void Critical(string message)
    {
        Log(CRITICAL, message);
    }
}
