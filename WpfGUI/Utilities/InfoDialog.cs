using System.Windows;
using BusinessLogic.API;

namespace WpfGUI.Utilities
{
    public class InfoDialog : IUserInfo
    {
        public void PromptUser(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }
    }
}