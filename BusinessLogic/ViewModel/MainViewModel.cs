using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BusinessLogic.API;
using BusinessLogic.Base;
using BusinessLogic.Model;
using DataContract.API;
using DataContract.Model;
using Reflection;
using Reflection.Exceptions;

namespace BusinessLogic.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly IFilePathGetter _filePathGetter;
        private readonly ILogger _logger;
        private readonly IUserInfo _userInfo;
        private readonly ISerializer _serializer;

        private readonly object _syncLock = new object();

        private bool _isExecuting;

        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }

            private set
            {
                _isExecuting = value;
                LoadMetadataCommand.RaiseCanExecuteChanged();
            }
        }

        public IControllableCommand LoadMetadataCommand { get; }

        public IControllableCommand SaveMetadataCommand { get;  }

        private Reflector _reflector;

        private ObservableCollection<MetadataTreeItem> _metadataHierarchy;

        public ObservableCollection<MetadataTreeItem> MetadataHierarchy
        {
            get => _metadataHierarchy;
            private set => SetProperty(ref _metadataHierarchy, value);
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private string _saveFilePath;

        public string SaveFilePath
        {
            get => _saveFilePath;
            set => SetProperty(ref _saveFilePath, value);
        }

        public MainViewModel(
            IUserInfo userInfo,
            IFilePathGetter filePathGetter,
            ISerializer serializer,
            ILogger logger
            )
        {
            _logger = logger;
            _filePathGetter = filePathGetter;
            _userInfo = userInfo;
            _serializer = serializer;
            MetadataHierarchy = new ObservableCollection<MetadataTreeItem>();
            LoadMetadataCommand = new RelayCommand(Open, () => !_isExecuting);
            SaveMetadataCommand = new RelayCommand(Save, () => !_isExecuting);
        }

        private async void Open()
        {
            lock (_syncLock)
            {
                if (IsExecuting) return;
                IsExecuting = true;
            }

            _logger.Trace($"Reading file path...");
            string filePath = _filePathGetter.GetFilePath();
            if (string.IsNullOrEmpty(filePath) || (!filePath.EndsWith(".dll", StringComparison.InvariantCulture) && !filePath.EndsWith(".xml", StringComparison.InvariantCulture)))
            {
                _userInfo.PromptUser("Selected file was invalid!", "File Error");
                _logger.Trace($"Selected file was invalid!");
                IsExecuting = false;
                return;
            }

            _logger.Trace($"Read file path: {filePath}");
            FilePath = filePath;

            await Task.Run(() =>
            {
                try
                {
                    if (filePath.EndsWith(".dll", StringComparison.InvariantCulture))
                    {
                        _logger.Trace("Beginning reflection subroutine...");
                        _reflector = new Reflector(FilePath);
                        _logger.Trace("Reflection subroutine finished successfully!");
                    }
                    else if (filePath.EndsWith(".xml", StringComparison.InvariantCulture))
                    {
                        AssemblyModel model = _serializer.Deserialize<AssemblyModel>(filePath);
                        _reflector.AssemblyModel = model;
                    }
                }
                catch (AssemblyBlockedException e)
                {
                    _userInfo.PromptUser("Unblock the selected assembly if you want to read its content!", "Expected Error");
                    _logger.Trace($"AssemblyBlockedException thrown, message: {e.Message}");
                }
                catch (ReflectionException e)
                {
                    _userInfo.PromptUser($"Something unexpected happened.\nError message: {e.Message}", "Unexpected ERROR");
                    _logger.Trace($"ReflectionException thrown, message: {e.Message}");
                }
            }).ConfigureAwait(true);

            if (_reflector == null)
            {
                IsExecuting = false;
                return;
            }

            MetadataHierarchy = new ObservableCollection<MetadataTreeItem>() { new AssemblyTreeItem(_reflector.AssemblyModel) };
            _logger.Trace("Successfully loaded root metadata item.");
            IsExecuting = false;
        }

        private async void Save()
        {
            lock (_syncLock)
            {
                if (IsExecuting) return;
                IsExecuting = true;
            }

            _logger.Trace($"Serializing metadata...");

            await Task.Run(() =>
            {
                try
                {
                    _serializer.Serialize(_reflector.AssemblyModel, SaveFilePath);
                    _userInfo.PromptUser("Serialization suceeded", "Serialization suceeded");
                }
                catch (Exception ex)
                {
                    _userInfo.PromptUser(ex.Message, "Serialization failed");
                }
            }).ConfigureAwait(true);

            IsExecuting = false;
        }
    }
}
