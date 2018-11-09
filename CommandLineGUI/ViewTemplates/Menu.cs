using System;
using System.Collections.Generic;
using CommandLineGUI.Base;

namespace CommandLineGUI.ViewTemplates
{
    internal class Menu : IDisplayable
    {
        public List<MenuItem> MenuItems { get; }

        public List<string> QuitKeywords { get; }

        private string _message = string.Empty;
        private bool _continueExecuting;

        public Menu()
        {
            MenuItems = new List<MenuItem>();
            QuitKeywords = new List<string>();
            _continueExecuting = true;
        }

        public void Display()
        {
            while (_continueExecuting)
            {
                if (!string.IsNullOrEmpty(_message))
                {
                    Console.WriteLine(_message);
                }
                Console.WriteLine($"Choose option or {string.Join("/", QuitKeywords)} to quit");
                DisplayElements();
                string choice = Console.ReadLine();
                ProcessSelectedOption(choice);
                Console.Clear();
            }        
        }

        private void DisplayElements()
        {
            foreach (MenuItem mi in MenuItems)
            {
                mi.Display();
            }
        }

        private void ProcessSelectedOption(string choice)
        {
            MenuItem selectedOption = MenuItems.Find(mi => mi.Option == choice);
            if (selectedOption != null)
            {
                if (selectedOption.Command.CanExecute(null))
                {
                    _message = string.Empty;
                    selectedOption.Command.Execute(null);
                }
                else
                {
                    _message = "Action is not allowed at the moment!!";
                }
            }
            else
            {
                if (QuitKeywords.Exists(keyword => keyword == choice))
                {
                    _continueExecuting = false;
                }
                else
                {
                    _message = "Incorrect option!!";
                }
                
            }
        }
    }
}
