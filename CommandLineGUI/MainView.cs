using System;
using BusinessLogic.Base;
using BusinessLogic.ViewModel;
using CommandLineGUI.Base;
using CommandLineGUI.Framework.Elements;

namespace CommandLineGUI
{
    internal class MainView
    {
        public Menu Menu { get; set; }

        public ConsoleTreeView ConsoleTreeView { get; set; }

        public MainView(MainViewModel viewModel)
        {
            DataContext dataContext = new DataContext(viewModel);
            ConsoleTreeView = new ConsoleTreeView(dataContext);
            ConsoleTreeView.DataContext.SetBinding(ConsoleTreeView, "MetadataItems", "TreeItems", "Loaded new .dll");
            InitializeMenu(dataContext);
        }

        public void Display()
        {
            while (true)
            {
                Menu.Display();
                Menu.InputLoop();
            }
        }

        private void InitializeMenu(DataContext dataContext)
        {
            Menu = new Menu(dataContext);

            MenuItem getFilePathItem = new MenuItem(dataContext) { Option = '1', Header = "Provide path for .dll file" };
            getFilePathItem.DataContext.SetBinding(getFilePathItem, "Command", "GetFilePathCommand");
            Menu.Add(getFilePathItem);

            MenuItem loadMetadataItem = new MenuItem(dataContext)
                { Option = '2', Header = "Load metadata of the chosen .dll file" };
            loadMetadataItem.DataContext.SetBinding(loadMetadataItem, "Command", "LoadMetadataCommand");
            Menu.Add(loadMetadataItem);

            Menu.Add(new MenuItem(dataContext)
            {
                Option = '3',
                Command = new RelayCommand(
                    () => { ConsoleTreeView.Display(); },
                    () => ConsoleTreeView.MetadataItems != null && ConsoleTreeView.MetadataItems.Count > 0),
                Header = "Display .dll metadata"
            });
            Menu.Add(new MenuItem(dataContext)
                { Option = 'q', Command = new RelayCommand(() => Environment.Exit(0)), Header = "Quit" });
        }
    }
}