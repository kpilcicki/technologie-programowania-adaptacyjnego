using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model;
using CommandLineGUI.Base;

namespace CommandLineGUI.ViewTemplates
{
    public class TreeViewItemConsole : IDisplayable
    {
        public TreeViewItem TreeItem { get; set; }
        public bool IsExpanded { get; set; }
        public int Indent { get; set; }
        public int Index { get; set; }

        public TreeViewItemConsole(TreeViewItem treeItem, int indent)
        {
            TreeItem = treeItem;
            IsExpanded = false;
            Indent = indent;
        }

        public ObservableCollection<TreeViewItem> Expand()
        {
            IsExpanded = true;
            TreeItem.IsExpanded = true;
            return TreeItem.Children;
        }

        public void Display()
        {
            Console.Write(new string(' ', Indent * 3));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{Index}. ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(IsExpanded ? "(-) " : "(+) ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"<<{TreeItem.ItemType}>> ");
            Console.ResetColor();
            Console.WriteLine(TreeItem.Name);
        }
    }
}