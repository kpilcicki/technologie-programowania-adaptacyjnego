using System;
using System.Windows.Input;
using CommandLineGUI.Base;

namespace CommandLineGUI.Framework.Elements
{
    internal class MenuItem : IBindableTarget, IDisplayable
    {
        public MenuItem(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public char Option { get; set; }

        public string Header { get; set; }

        public ICommand Command { get; set; }

        public void Display()
        {
            if (Command != null)
            {
                Console.WriteLine($"{Option} - {Header}");
            }
        }

        public DataContext DataContext { get; }
    }
}