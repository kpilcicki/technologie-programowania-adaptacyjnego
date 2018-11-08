using System;
using System.Windows.Input;
using CommandLineGUI.Base;

namespace CommandLineGUI.ViewTemplates
{
    internal class MenuItem : IDisplayable
    {
        public DataContext DataContext { get; }

        public string Option { get; set; }

        public string Header { get; set; }

        public ICommand Command { get; set; }

        public MenuItem(DataContext dataContext)
        {
            DataContext = dataContext;
        }
       
        public void Display()
        {
            Console.WriteLine($"{Option} - {Header}");
        }

    }
}
