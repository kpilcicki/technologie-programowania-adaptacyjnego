using System;
using BusinessLogic.API;

namespace CommandLineGUI.Utilities
{
    internal class ConsoleFilePathGetter : IFilePathGetter
    {
        public string GetFilePath()
        {
            Console.WriteLine("Provide absolute path for .dll file");
            string filePath = Console.ReadLine();
            return filePath;
        }
    }
}
