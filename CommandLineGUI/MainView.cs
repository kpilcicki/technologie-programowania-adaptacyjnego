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

        public ConsoleTreeView TreeViewConsole { get; set; }

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
            TreeViewConsole = new ConsoleTreeView();
            TreeViewConsole.QuitKeywords.AddRange(new string[] { "q", "Q", "quit" });

            DataContext.SetBinding(TreeViewConsole, "TreeViewItems", "MetadataHierarchy");
        }

        private void InitializeMenu()
        {
            Menu = new Menu();
            Menu.QuitKeywords.AddRange(new string[] {"q", "Q", "quit"});

            MenuItem loadMetadataFromFile = new MenuItem() {Option = "1", Header = "Load metadata from .dll or from other source"};
            Menu.MenuItems.Add(loadMetadataFromFile);
            DataContext.SetBinding(loadMetadataFromFile, "Command", "LoadMetadataCommand");

            MenuItem saveMetadata = new MenuItem() { Option = "2", Header = "Save loaded metadata" };
            Menu.MenuItems.Add(saveMetadata);
            DataContext.SetBinding(saveMetadata, "Command", "SaveMetadataCommand");

            Menu.MenuItems.Add(new MenuItem()
            {
                Option = "3", Header = "Display metadata",
                Command = new RelayCommand(TreeViewConsole.Display, () => TreeViewConsole.ItemsSource.Any())
            });
        }
    }
}
