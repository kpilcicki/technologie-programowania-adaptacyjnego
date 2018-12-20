using System;
using BusinessLogic.Services;

namespace CommandLineGUI.Utilities
{
    internal class ConsoleFilePathGetter : IFilePathGetter
    {
        public string GetFilePath()
        {
            Console.WriteLine("Provide absolute path for file");
            string filePath = Console.ReadLine();
            return filePath;
        }
    }
}
