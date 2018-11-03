using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using BusinessLogic.API;
using BusinessLogic.Base;
using BusinessLogic.Model;
using BusinessLogic.Services;
using DataContract.API;
using DataContract.Model;

namespace BusinessLogic.ViewModel
{
    public class MainViewModel : ValidatableBindableBase, INotifyPropertyChanged
    {
        private readonly IFilePathGetter _filePathGetter;

        private MetadataItem _treeRoot;

        public MetadataItem TreeRoot
        {
            get => _treeRoot;
            set
            {
                _treeRoot = value;
                NotifyPropertyChanged("TreeRoot");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public ObservableCollection<MetadataItem> TreeItems { get; set; }

        public MainViewModel(IFilePathGetter filePathGetter)
        {
            _filePathGetter = filePathGetter;
            TreeItems = new ObservableCollection<MetadataItem>();

            GetFilePathCommand = new RelayCommand(() => FilePath = _filePathGetter.GetFilePath());
            GetMetadata = new RelayCommand(() =>
            {
                Reflector.Reflector reflect = new Reflector.Reflector();
                AssemblyMetadataStorage storage = reflect.GetAssemblyMetadata(FilePath);
                IMapper<AssemblyMetadataStorage, MetadataItem> mapper = new MetadataItemMapper();
                TreeRoot = mapper.Map(storage);
                TreeItems.Add(TreeRoot);
            });
        }

        private string _filePath;

        public string FilePath { get => _filePath; set => SetPropertyAndValidate(ref _filePath, value); }

        public ICommand GetFilePathCommand { get; private set; }

        public MetadataItem RootMetadataItem { get; private set; }

        public ICommand GetMetadata { get; private set; }
    }
}
