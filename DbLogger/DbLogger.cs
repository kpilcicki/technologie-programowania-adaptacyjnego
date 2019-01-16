using System;
using System.ComponentModel.Composition;
using BusinessLogic.Services;

namespace DbLogger
{
    [Export(typeof(ILogger))]
    public class DbLogger : ILogger
    {
        public void Trace(string message)
        {
            using (LoggerContext ctx = new LoggerContext())
            {
                ctx.Logs.Add(new LogItem() { Message = message, Time = DateTime.Now });
                ctx.SaveChanges();
            }
        }
    }
}
