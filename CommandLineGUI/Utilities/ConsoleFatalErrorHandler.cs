using System;
using BusinessLogic.Services;

namespace CommandLineGUI.Utilities
{
    internal class ConsoleFatalErrorHandler : IFatalErrorHandler
    {
        public void HandleFatalError(string errorInfo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Fatal error occurred and application will be closed.\nError message: {errorInfo}");
            Console.ResetColor();
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}
