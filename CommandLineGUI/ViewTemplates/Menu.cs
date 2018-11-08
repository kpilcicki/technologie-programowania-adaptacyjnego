using System;
using System.Collections.Generic;
using CommandLineGUI.Base;

namespace CommandLineGUI.ViewTemplates
{
    internal class Menu : IDisplayable
    {
        public List<MenuItem> MenuItems { get; }

        public List<string> QuitKeywords { get; }

        private bool _incorrectInput;
        private bool _continueExecuting;

        public Menu()
        {
            MenuItems = new List<MenuItem>();
            QuitKeywords = new List<string>();
            _continueExecuting = true;
            _incorrectInput = false;
        }

        public void Display()
        {
            while (_continueExecuting)
            {
                if (_incorrectInput)
                {
                    Console.WriteLine("You have chosen incorrect option!!!");
                    _incorrectInput = false;
                }

                Console.WriteLine("Choose option:");
                DisplayElements();
                string choice = Console.ReadLine();
                ProcessSelectedOption(choice);
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
            MenuItem selectedOption = MenuItems.Find(mi => mi.Header == choice);
            if (selectedOption != null)
            {
                _incorrectInput = false;
                selectedOption.Command.Execute(null);
            }
            else
            {
                if (QuitKeywords.Exists(keyword => keyword == choice))
                {
                    _incorrectInput = false;
                    _continueExecuting = false;
                }
                _incorrectInput = true;
            }
        }
    }
}
