using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using BusinessLogic.Base;
using BusinessLogic.Model;
using BusinessLogic.Services;
using Reflection;
using Reflection.Exceptions;
using Reflection.Model;
using Reflection.Persistence;

namespace BusinessLogic.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly IFilePathGetter _filePathGetter;
        private readonly IUserInfo _userInfo;
        private readonly Reflector _reflector;
        private AssemblyModel _assemblyModel;

        public AssemblyModel AssemblyModel
        {
            get => _assemblyModel;
            private set
            {
                _assemblyModel = value;
                MetadataHierarchy = new ObservableCollection<MetadataTreeItem>()
                {
                    new AssemblyTreeItem(_assemblyModel)
                };
            }
        }

        [Import(typeof(ILogger), AllowDefault = false)]
        public ILogger Logger { get; set; }

        public PersistenceManager PersistenceService { get; set; }

        public AsyncCommand LoadMetadataCommand { get; }

        public AsyncCommand LoadMetadataFromDataSource { get; }

        public AsyncCommand SaveMetadataCommand { get; set; }

        private ObservableCollection<MetadataTreeItem> _metadataHierarchy;

        public ObservableCollection<MetadataTreeItem> MetadataHierarchy
        {
            get => _metadataHierarchy;
            private set
            {
                SetProperty(ref _metadataHierarchy, value);
                SaveMetadataCommand.RaiseCanExecuteChanged();
            }
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                LoadMetadataCommand.RaiseCanExecuteChanged();
                LoadMetadataFromDataSource.RaiseCanExecuteChanged();
                SaveMetadataCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel(
            IUserInfo userInfo,
            IFilePathGetter filePathGetter)
        {
            _reflector = new Reflector();
            _filePathGetter = filePathGetter ?? throw new ArgumentNullException(nameof(filePathGetter));
            _userInfo = userInfo ?? throw new ArgumentNullException(nameof(userInfo));

            LoadMetadataCommand = new AsyncCommand(ReadMetadata, () => !IsBusy, new DisplayErrorHandler(_userInfo));
            LoadMetadataFromDataSource = new AsyncCommand(ReadFromDataSource, () => !IsBusy, new DisplayErrorHandler(_userInfo));
            SaveMetadataCommand = new AsyncCommand(Save, () => !IsBusy && AssemblyModel != null, new DisplayErrorHandler(_userInfo));

            MetadataHierarchy = new ObservableCollection<MetadataTreeItem>();
        }

        private async Task ReadMetadata()
        {
            try
            {
                IsBusy = true;

                Task<AssemblyModel> metadataReading = Task.Run(() =>
                {
                    Logger.Trace("Reading metadata from .dll file command fired");
                    string filePath = _filePathGetter.GetFilePath();
                    Logger.Trace($"Provided filepath: {filePath}");

                    if (string.IsNullOrEmpty(filePath) || !filePath.EndsWith(".dll", StringComparison.InvariantCulture))
                    {
                        Logger.Trace($"Provided filepath {filePath} was invalid");
                        throw new Exception("Provided filepath was invalid. Only .dll files are allowed.");
                    }
                    else
                    {
                        Logger.Trace($"Provided filepath {filePath} was valid");
                        FilePath = filePath;

                        Logger.Trace($"Reading metadata from file: {filePath}");
                        AssemblyModel assemblyModel = _reflector.ReflectDll(filePath);
                        Logger.Trace($"Successfully read metadata from .dll file: {filePath}");
                        return assemblyModel;
                    }
                });

                await metadataReading;
                AssemblyModel = metadataReading.Result;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ReadFromDataSource()
        {
            try
            {
                IsBusy = true;

                Task<AssemblyModel> metadataReading = Task.Run(() =>
                {
                        try
                        {
                            Logger.Trace($"Reading metadata from data source;");
                            AssemblyModel assemblyModel = PersistenceService.Deserialize();
                            Logger.Trace($"Successfully read metadata from data source;");
                            return assemblyModel;
                        }
                        catch (PersistenceException e)
                        {
                            Logger.Trace($"{typeof(PersistenceException)} thrown, message: {e.Message}");
                            throw new Exception($"An error occurred during reading from data source. {e.Message}", e);
                        }
                });
                await metadataReading;
                AssemblyModel = metadataReading.Result;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Save()
        {
            try
            {
                IsBusy = true;
                await Task.Run(() =>
                {
                    try
                    {
                        Logger.Trace($"Saving metadata started");
                        PersistenceService.Serialize(AssemblyModel);
                        Logger.Trace($"Saving process of assembly: {AssemblyModel.Name} succeeded");
                    }
                    catch (PersistenceException ex)
                    {
                       Logger.Trace($"{typeof(PersistenceException)} thrown; message: {ex.Message}");
                       throw new Exception("Saving to data source failed");
                    }
                });
                _userInfo.PromptUser("Saving succeeded", "Saving operation");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}