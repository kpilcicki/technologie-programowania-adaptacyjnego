using System.Linq;
using BusinessLogic.Base;
using BusinessLogic.ViewModel;
using CommandLineGUI.Base;
using CommandLineGUI.ViewTemplates;

namespace CommandLineGUI
{
    internal class MainView : IDisplayable
    {
        public DataContext DataContext { get; }

        public Menu Menu { get; set; }

        public TreeViewConsole TreeViewConsole { get; set; }

        public MainView(MainViewModel viewModel)
        {
            DataContext = new DataContext(viewModel);
            InitializeTreeView();
            InitializeMenu();

        }

        public void Display()
        {
            Menu.Display();
        }

        private void InitializeTreeView()
        {
            TreeViewConsole = new TreeViewConsole();
            TreeViewConsole.QuitKeywords.AddRange(new string[] { "q", "Q", "quit" });

            DataContext.SetBinding(TreeViewConsole, "TreeViewItems", "MetadataHierarchy");
        }

        private void InitializeMenu()
        {
            Menu = new Menu();
            Menu.QuitKeywords.AddRange(new string[] {"q", "Q", "quit"});

            MenuItem loadMetadataFromFile = new MenuItem() {Option = "1", Header = "Load metadata from .dll file"};
            Menu.MenuItems.Add(loadMetadataFromFile);
            DataContext.SetBinding(loadMetadataFromFile, "Command", "LoadMetadataCommand");

            Menu.MenuItems.Add(new MenuItem()
            {
                Option = "2", Header = "Display metadata",
                Command = new RelayCommand(TreeViewConsole.Display, () => TreeViewConsole.HierarchicalDataCollection.Any())
            });
        }
    }
}
