using System;
using System.ComponentModel.Composition;
using BusinessLogic.Services;

namespace FileLogger
{
    [Export(typeof(ILogger))]
    public class Logger : ILogger
    {
        public void Trace(string message)
        {
            System.Diagnostics.Trace.WriteLine(DateTime.Now + " | " + message);
        }
    }
}
