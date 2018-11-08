using System;
using System.Collections.Generic;
using CommandLineGUI.Base;

namespace CommandLineGUI.Framework.Elements
{
    internal class Menu : IBindableTarget, IDisplayable
    {
        private readonly List<MenuItem> _menuItems = new List<MenuItem>();

        public Menu(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public void Display()
        {
            Console.WriteLine("Choose option to execute:");
            foreach (MenuItem menuItem in _menuItems)
            {
                menuItem.Display();
            }
        }

        public void InputLoop()
        {
            bool correctOption = false;
            while (!correctOption)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();
                MenuItem chosenItem = _menuItems.Find(opt => opt.Option == key.KeyChar);
                if (chosenItem != null)
                {
                    if (!chosenItem.Command.CanExecute(null))
                    {
                        Console.WriteLine("Can not perform action now");
                    }
                    else
                    {
                        chosenItem.Command?.Execute(null);
                        correctOption = true;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect option! Choose again: ");
                    Display();
                }
            }
        }

        public void Add(MenuItem menuItem)
        {
            _menuItems.Add(menuItem);
        }

        public DataContext DataContext { get; }
    }
}