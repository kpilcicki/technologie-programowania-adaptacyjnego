using System;
using BusinessLogic.Services;

namespace FileLogger
{
    public class Logger : ILogger
    {
        public void Trace(string message)
        {
            System.Diagnostics.Trace.WriteLine(DateTime.Now + " | " + message);
        }
    }
}
