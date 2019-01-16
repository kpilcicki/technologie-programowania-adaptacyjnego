using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using BusinessLogic.Base;
using BusinessLogic.Model;
using BusinessLogic.Services;
using DataTransferGraph.Exception;
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

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;

            private set
            {
                _isBusy = value;
                LoadMetadataCommand.RaiseCanExecuteChanged();
                SaveMetadataCommand.RaiseCanExecuteChanged();
            }
        }

        [Import(typeof(ILogger), AllowDefault = false)]
        public ILogger Logger { get; set; }

        public PersistenceManager PersistenceService { get; set; }

        public IControllableCommand LoadMetadataCommand { get; }

        public IControllableCommand SaveMetadataCommand { get; }

        public IControllableCommand LoadMetadataFromDataSource { get; }

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
                SaveMetadataCommand.RaiseCanExecuteChanged();
            }
        }

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

        public MainViewModel(
            IUserInfo userInfo,
            IFilePathGetter filePathGetter)
        {
            _reflector = new Reflector();
            _filePathGetter = filePathGetter ?? throw new ArgumentNullException(nameof(filePathGetter));
            _userInfo = userInfo ?? throw new ArgumentNullException(nameof(userInfo));

            MetadataHierarchy = new ObservableCollection<MetadataTreeItem>();
            LoadMetadataCommand = new RelayCommand(ReadMetadata, () => !IsBusy);
            SaveMetadataCommand = new RelayCommand(Save, () => !IsBusy && AssemblyModel != null);
            LoadMetadataFromDataSource = new RelayCommand(ReadFromDataSource, () => !IsBusy);
        }

        private void ReadMetadata()
        {
            try
            {
                IsBusy = true;

                Logger?.Trace($"Reading file path for .dll file...");

                string filePath = _filePathGetter.GetFilePath();

                if (string.IsNullOrEmpty(filePath))
                {
                    _userInfo.PromptUser("Provided filepath was empty", "Filepath Error");
                    Logger?.Trace($"Provided filepath was null or empty");
                    return;
                }

                FilePath = filePath;

                if (filePath.EndsWith(".dll", StringComparison.InvariantCulture))
                {
                    Logger?.Trace($"Reading metadata from {filePath}; .dll");
                    AssemblyModel = _reflector.ReflectDll(filePath);
                    Logger?.Trace($"Successfully read metadata from {filePath}; .dll");
                }
                else
                {
                    _userInfo.PromptUser(
                        "Provided file has unknown extension. Program accepts files only with .dll extensions",
                        "File Error");
                    Logger?.Trace($"Provided file has unknown extension {Path.GetExtension(filePath)}");
                }
            }
            catch (AssemblyBlockedException e)
            {
                _userInfo.PromptUser("Unblock the selected assembly if you want to read its content!",
                    "Expected Error");
                Logger?.Trace($"AssemblyBlockedException thrown, message: {e.Message}");
            }
            catch (ReflectionException e)
            {
                _userInfo.PromptUser($"Something unexpected happened.\nError message: {e.Message}", "Unexpected Error");
                Logger?.Trace($"ReflectionException thrown, message: {e.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ReadFromDataSource()
        {
            try
            {
                IsBusy = true;
                Logger?.Trace($"Reading metadata from data source;");
                AssemblyModel = PersistenceService?.Deserialize();
                Logger?.Trace($"Successfully read metadata from data source;");
            }
            catch (ReadingMetadataException e)
            {
                _userInfo.PromptUser($"An error occurred during reading from data source. {e.Message}",
                    "Unexpected exception");
                Logger?.Trace($"ReadingMetadataException thrown, message: {e.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Save()
        {
            try
            {
                IsBusy = true;
                Logger?.Trace($"Serializing metadata...");

                PersistenceService?.Serialize(AssemblyModel);
                Logger?.Trace($"Serialization of assembly: {AssemblyModel.Name} succeeded");
                _userInfo.PromptUser("Saving succeeded", "Saving operation");
            }
            catch (SavingMetadataException ex)
            {
               Logger?.Trace($"Serialization of assembly: {AssemblyModel.Name} failed");
               _userInfo.PromptUser(ex.Message, "Serialization failed");
               Logger?.Trace(
                   $"Serialization of assembly: {AssemblyModel.Name} failed: exception thrown: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}