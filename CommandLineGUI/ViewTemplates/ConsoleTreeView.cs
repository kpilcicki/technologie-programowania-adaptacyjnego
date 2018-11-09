using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic.Model;
using CommandLineGUI.Base;

namespace CommandLineGUI.ViewTemplates
{
    public class ConsoleTreeView : IDisplayable
    {
        private ObservableCollection<MetadataTreeItem> _treeViewItems;

        public ObservableCollection<MetadataTreeItem> TreeViewItems
        {
            get => _treeViewItems;
            set
            {
                ItemsSource.Clear();
                _treeViewItems = value;
                if (_treeViewItems?.Count > 0)
                    ItemsSource.Add(new ConsoleTreeViewItem(_treeViewItems[0], 0));
            }
        }

        public ObservableCollection<ConsoleTreeViewItem> ItemsSource { get; }

        public List<string> QuitKeywords { get; }

        private string _message = "";

        public ConsoleTreeView()
        {
            ItemsSource = new ObservableCollection<ConsoleTreeViewItem>();
            QuitKeywords = new List<string>();
        }

        public void Expand(int id)
        {
            ConsoleTreeViewItem item = ItemsSource[id];
            if (!item.IsExpanded)
            {
                int i = 1;
                ObservableCollection<MetadataTreeItem> items = item.Expand();
                foreach (MetadataTreeItem treeViewItem in items)
                {
                    ItemsSource.Insert(id + i,
                        new ConsoleTreeViewItem(treeViewItem, item.Spacing + 1));
                    i++;
                }
            }
            else
            {
                for (int i = item.TreeItem.Children.Count; i > 0; i--)
                {
                    if (ItemsSource[id + i].IsExpanded)
                    {
                        Expand(id + i);
                        ItemsSource.RemoveAt(id + i);
                    }
                    else
                    {
                        ItemsSource.RemoveAt(id + i);
                    }
                }

                item.IsExpanded = false;
            }
        }

        public void Display()
        {
            bool inTree = true;
            while (inTree)
            {
                if (!string.IsNullOrEmpty(_message)) Console.WriteLine(_message);
                Console.WriteLine($"Choose node to expand or {string.Join("/", QuitKeywords)} to quit tree view");
                DisplayElements();
                string choice = Console.ReadLine();
                ProcessInput(choice, ref inTree);
                Console.Clear();
            }
        }

        private void DisplayElements()
        {
            int id = 0;
            foreach (ConsoleTreeViewItem consoleItem in ItemsSource)
            {
                consoleItem.Id = id++;
                consoleItem.Display();
            }
        }

        private void ProcessInput(string choice, ref bool stayInTree)
        {
            if (!Int32.TryParse(choice, out int parsedNumber) || parsedNumber < 0 ||
                parsedNumber > ItemsSource.Count - 1)
            {
                if (QuitKeywords.Exists(word => word == choice))
                {
                    stayInTree = false;
                    return;
                }

                _message = "Incorrect option!!";
                return;
            }

            _message = string.Empty;

            Expand(parsedNumber);
        }

    }   
}