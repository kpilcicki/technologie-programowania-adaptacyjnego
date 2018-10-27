using BusinessLogic.Services;
using Microsoft.Win32;

namespace WpfGUI
{
    internal class FileDialog : IFilePathGetter
    {
        public string GetFilePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                return fileDialog.FileName;
            }
            else return "";
        }
    }
}