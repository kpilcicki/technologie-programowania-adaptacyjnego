using BusinessLogic.ViewModel;
using CommandLineGUI.Base;
using CommandLineGUI.ViewTemplates;

namespace CommandLineGUI
{
    internal class MainView : IDisplayable
    {
        public DataContext DataContext { get; }

        public Menu Menu { get; set; }

        public MainView(MainViewModel viewModel)
        {
            DataContext = new DataContext(viewModel);
            
        }

        public void Display()
        {
            Menu.Display();
        }

        private void InitializeMenu()
        {
            Menu = new Menu();

            Menu.QuitKeywords.AddRange(new string[] {"q", "Q", "quit"});

            Menu.MenuItems.Add(new MenuItem() {Option = "1", Header = "Load metadata from .dll file"});
        }
    }
}
