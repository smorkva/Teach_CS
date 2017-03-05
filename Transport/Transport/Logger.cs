using System;
using System.IO;

namespace Transport
{
    public sealed class Logger
    {
        private static Logger _instance { get; } = new Logger();

        private readonly string _file;

        private Logger()
        {
            _file = @"./log.txt";
        }

        public static void Log(string message)
        {
            using (var writer = new StreamWriter(_instance._file, true))
            {
                writer.WriteLine($"{DateTime.Now} | {message}");
            }
        }
    }
}
