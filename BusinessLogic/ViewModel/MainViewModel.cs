using System.Windows.Input;
using BusinessLogic.API;
using BusinessLogic.Base;
using BusinessLogic.Model;

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

        public MetadataItem RootMetadataItem { get; private set; }
    }
}
