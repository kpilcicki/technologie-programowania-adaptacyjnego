using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Model;
using CommandLineGUI.Base;

namespace CommandLineGUI.Framework.Elements
{
    internal class ConsoleTreeView : IBindableTarget, IDisplayable
    {
        private const string BaseGenerator = "A";

        private readonly List<MetadataItem> _history = new List<MetadataItem>();

        private List<MetadataItem> _items = new List<MetadataItem>();

        private Dictionary<string, MetadataItem> _currentlyVisible = new Dictionary<string, MetadataItem>();

        public List<MetadataItem> MetadataItems
        {
            get => _items;
            set
            {
                if (value != null && value.Count > 0)
                {
                    _history.Clear();
                    _currentlyVisible.Clear();
                    _items = value;

                    _currentlyVisible.Add(BaseGenerator, _items[0]);
                }
            }
        }

        public ConsoleTreeView(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public void Display()
        {
            bool insideTree = true;
            while (insideTree)
            {
                bool correctOption = false;
                while (!correctOption)
                {
                    Console.WriteLine(
                        "Choose node to expand, type \"back\" to return to previous parent, type \"quit\" to quit tree view: ");
                    DisplayElements();
                    string choice = Console.ReadLine() ?? string.Empty;
                    if (_currentlyVisible.ContainsKey(choice) && _currentlyVisible[choice].IsExpendable)
                    {
                        UpdateCurrentItems(_currentlyVisible[choice], false);
                        correctOption = true;
                    }
                    else if (choice == "back")
                    {
                        if (_history.Count > 1)
                        {
                            _history.RemoveAt(_history.Count - 1);
                        }

                        if (_history.Any())
                        {
                            MetadataItem currentParent = _history.Last();
                            UpdateCurrentItems(currentParent, true);
                        }
                        else
                        {
                            _currentlyVisible.Clear();
                            _currentlyVisible.Add("A", _items[0]);
                        }

                        correctOption = true;
                    }
                    else if (choice == "quit")
                    {
                        correctOption = true;
                        insideTree = false;
                    }
                }
            }
        }

        public void UpdateCurrentItems(MetadataItem currentParent, bool isBack)
        {
            char firstChar = (char)65;
            if (!isBack) _history.Add(currentParent);
            _currentlyVisible = currentParent.Children.ToDictionary(x => (firstChar++).ToString(), x => x);
        }

        public void DisplayElements()
        {
            foreach (KeyValuePair<string, MetadataItem> currentItem in _currentlyVisible)
            {
                Console.WriteLine(currentItem.Value.IsExpendable == false
                    ? $"- {currentItem.Value.Name}"
                    : $"{currentItem.Key} - {currentItem.Value.Name}");
            }
        }

        public DataContext DataContext { get; }
    }
}