using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
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

        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }

        public PersistenceManager PersistenceService { get; set; } = new PersistenceManager();

        public IControllableCommand LoadMetadataCommand { get; }

        public IControllableCommand SaveMetadataCommand { get; }

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
            LoadMetadataCommand = new RelayCommand(Open, () => !IsBusy);
            SaveMetadataCommand = new RelayCommand(Save, () => !IsBusy && AssemblyModel != null);
        }

        private void Open()
        {
            try
            {
                IsBusy = true;

                Logger?.Trace($"Reading file path...");

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
                else if (filePath.EndsWith(".xml", StringComparison.InvariantCulture))
                {
                    Logger?.Trace($"Reading metadata from {filePath}; .xml");
                    AssemblyModel = PersistenceService?.Deserialize(filePath);
                    Logger?.Trace($"Successfully read metadata from {filePath}; .xml");
                }
                else
                {
                    _userInfo.PromptUser(
                        "Provided file has unknown extension. Program accepts files only with .dll and .xml extensions",
                        "File Error");
                    Logger?.Trace($"Provided file has unknown extension {Path.GetExtension(filePath)}");
                }
            }
            catch (AssemblyBlockedException e)
            {
                _userInfo.PromptUser("Unblock the selected assembly if you want to read its content!", "Expected Error");
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

        private void Save()
        {
            try
            {
                IsBusy = true;
                Logger?.Trace($"Serializing metadata...");

                string filePath = _filePathGetter.GetFilePath();
                if (filePath != null && filePath.EndsWith(".xml", StringComparison.InvariantCulture))
                {
                    PersistenceService?.Serialize(AssemblyModel, filePath);
                    Logger?.Trace($"Serialization of assembly: {AssemblyModel.Name} succeeded");
                    _userInfo.PromptUser("Saving succeeded", "Saving operation");
                }
                else
                {
                    Logger?.Trace($"Provided file is not .xml file");
                    _userInfo.PromptUser("Provided file is not .xml file", "Saving operation failure");
                }
            }

            // catch (Exception ex)
            // {
            //    Logger?.Trace($"Serialization of assembly: {AssemblyModel.Name} failed");
            //    _userInfo.PromptUser(ex.Message, "Serialization failed");
            //    Logger?.Trace(
            //        $"Serialization of assembly: {AssemblyModel.Name} failed: exception thrown: {ex.Message}");
            // }
            finally
            {
                IsBusy = false;
            }
        }
    }
}