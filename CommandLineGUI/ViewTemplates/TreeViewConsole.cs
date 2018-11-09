using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using BusinessLogic.Model;
using CommandLineGUI.Base;

namespace CommandLineGUI.ViewTemplates
{
    public class TreeViewConsole : IDisplayable
    {
        private ObservableCollection<TreeViewItem> _treeViewItems;

        public ObservableCollection<TreeViewItem> TreeViewItems
        {
            get => _treeViewItems;
            set
            {
                HierarchicalDataCollection.Clear();
                _treeViewItems = value;
                if (_treeViewItems?.Count > 0)
                    HierarchicalDataCollection.Add(new TreeViewItemConsole(_treeViewItems[0], 0));
            }
        }

        public ObservableCollection<TreeViewItemConsole> HierarchicalDataCollection { get; }

        public List<string> QuitKeywords { get; }

        private string _message = "";

        public TreeViewConsole()
        {
            HierarchicalDataCollection = new ObservableCollection<TreeViewItemConsole>();
            QuitKeywords = new List<string>();
        }

        public void Expand(int index)
        {
            TreeViewItemConsole item = HierarchicalDataCollection[index];
            if (!item.IsExpanded)
            {
                int i = 1;
                ObservableCollection<TreeViewItem> items = item.Expand();
                foreach (TreeViewItem treeViewItem in items)
                {
                    HierarchicalDataCollection.Insert(index + i,
                        new TreeViewItemConsole(treeViewItem, item.Indent + 1));
                    i++;
                }
            }
            else
            {
                for (int i = item.TreeItem.Children.Count; i > 0; i--)
                {
                    if (HierarchicalDataCollection[index + i].IsExpanded)
                    {
                        Expand(index + i);
                        HierarchicalDataCollection.RemoveAt(index + i);
                    }
                    else
                    {
                        HierarchicalDataCollection.RemoveAt(index + i);
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
            int index = 0;
            foreach (TreeViewItemConsole itemConsole in HierarchicalDataCollection)
            {
                itemConsole.Index = index++;
                itemConsole.Display();
            }
        }

        private void ProcessInput(string choice, ref bool stayInTree)
        {
            if (!Int32.TryParse(choice, out int parsedTemp) || parsedTemp < 0 ||
                parsedTemp > HierarchicalDataCollection.Count - 1)
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

            Expand(parsedTemp);
        }

    }   
}