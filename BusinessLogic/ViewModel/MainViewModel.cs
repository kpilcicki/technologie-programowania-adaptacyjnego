using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using BusinessLogic.API;
using BusinessLogic.Base;
using BusinessLogic.Model;
using DataContract.API;
using Reflection;

namespace BusinessLogic.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly IFilePathGetter _filePathGetter;
        private readonly ILogger _logger;

        public ICommand LoadMetadataCommand { get; }

        private Reflector _reflector;

        private ObservableCollection<TreeViewItem> _metadataHierarchy;

        public ObservableCollection<TreeViewItem> MetadataHierarchy
        {
            get => _metadataHierarchy;
            set => SetProperty(ref _metadataHierarchy, value);
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public MainViewModel(IFilePathGetter filePathGetter, ILogger logger)
        {
            _logger = logger;
            _filePathGetter = filePathGetter;
            MetadataHierarchy = new ObservableCollection<TreeViewItem>();
            LoadMetadataCommand = new RelayCommand(Open);
        }

        private void Open()
        {
            string filePath = _filePathGetter.GetFilePath();
            if (string.IsNullOrEmpty(filePath) || !filePath.EndsWith(".dll", StringComparison.InvariantCulture)) return;
            FilePath = filePath;
            try
            {
                _reflector = new Reflector(Assembly.LoadFrom(FilePath));
            }
            catch (Exception)
            {
                // ignored
            }

            MetadataHierarchy.Clear();
            MetadataHierarchy = new ObservableCollection<TreeViewItem>() { new AssemblyTreeItem(_reflector.AssemblyModel) };
        }
    }
}
