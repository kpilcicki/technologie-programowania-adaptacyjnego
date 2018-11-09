using System;
using System.Windows.Input;
using CommandLineGUI.Base;

namespace CommandLineGUI.ViewTemplates
{
    internal class MenuItem : IDisplayable
    {
        public string Option { get; set; }

        public string Header { get; set; }

        public ICommand Command { get; set; }
       
        public void Display()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{Option}");
            Console.ResetColor();
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Header}");
            Console.ResetColor();
        }

    }
}
