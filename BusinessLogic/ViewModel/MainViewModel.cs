using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
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

        private bool _isExecuting;

        public IControllableCommand LoadMetadataCommand { get; }

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
            LoadMetadataCommand = new RelayCommand(Open, () => !_isExecuting);
        }

        private async void Open()
        {
            _isExecuting = true;
            LoadMetadataCommand.RaiseCanExecuteChanged();
            _logger.Trace($"Reading file path...");
            string filePath = _filePathGetter.GetFilePath();
            if (string.IsNullOrEmpty(filePath) || !filePath.EndsWith(".dll", StringComparison.InvariantCulture))
            {
                _logger.Trace($"Selected file was invalid!");
                return;
            }

            _logger.Trace($"Read file path: {filePath}");
            FilePath = filePath;

            await Task.Run(() =>
            {
                try
                {
                    _logger.Trace("Beginning reflection subroutine...");
                    _reflector = new Reflector(Assembly.LoadFrom(FilePath));
                    _logger.Trace("Reflection subroutine finished successfully!");
                }
                catch (Exception)
                {
                    // ignored
                }
            }).ConfigureAwait(true);

            MetadataHierarchy = new ObservableCollection<TreeViewItem>() { new AssemblyTreeItem(_reflector.AssemblyModel) };
            _logger.Trace("Successfully loaded root metadata item.");
            _isExecuting = false;
            LoadMetadataCommand.RaiseCanExecuteChanged();
        }
    }
}
