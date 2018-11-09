using System;
using System.Collections.ObjectModel;
using BusinessLogic.Constants;
using BusinessLogic.Model;
using CommandLineGUI.Base;

namespace CommandLineGUI.ViewTemplates
{
    public class ConsoleTreeViewItem : IDisplayable
    {
        public MetadataTreeItem TreeItem { get; set; }
        public bool IsExpanded { get; set; }
        public int Spacing { get; set; }
        public int Id { get; set; }

        public ConsoleTreeViewItem(MetadataTreeItem treeItem, int spacing)
        {
            TreeItem = treeItem;
            IsExpanded = false;
            Spacing = spacing;
        }

        public ObservableCollection<MetadataTreeItem> Expand()
        {
            IsExpanded = true;
            TreeItem.IsExpanded = true;
            return TreeItem.Children;
        }

        public void Display()
        {
            Console.Write(new string(' ', Spacing * 3));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{Id}. ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(IsExpanded ? "(-) " : "(+) ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{TypeToStringMap.GetStringFromType(TreeItem)} ");
            Console.ResetColor();
            Console.WriteLine(TreeItem.Name);
        }


    }
}