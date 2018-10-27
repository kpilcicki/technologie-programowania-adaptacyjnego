using BusinessLogic.Base;
using BusinessLogic.Services;
using System.Windows.Input;

namespace BusinessLogic.ViewModel
{
    public class MainViewModel : ValidatableBindableBase
    {
        private readonly IFilePathGetter _filePathGetter;
        public MainViewModel(IFilePathGetter filePathGetter) 
        {
            _filePathGetter = filePathGetter;

            GetFilePathCommand = new RelayCommand(() => FilePath = _filePathGetter.GetFilePath());
        }

        private string _filePath;
        public string FilePath { get => _filePath; set => SetPropertyAndValidate(ref _filePath, value); }

        public ICommand GetFilePathCommand { get; private set; }


    }
}
