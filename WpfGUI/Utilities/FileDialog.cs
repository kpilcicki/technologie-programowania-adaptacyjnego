using BusinessLogic.Services;
using Microsoft.Win32;
using ServiceContract.Services;

namespace WpfGUI.Utilities
{
    internal class FileDialog : IFilePathGetter
    {
        public string GetFilePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            return fileDialog.ShowDialog() == true ? fileDialog.FileName : string.Empty;
        }
    }
}