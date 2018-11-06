using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BusinessLogic.API;
using BusinessLogic.Base;
using BusinessLogic.Model;
using DataContract.API;
using DataContract.Model;
using Reflection.Exceptions;

namespace BusinessLogic.ViewModel
{
    public class MainViewModel : ValidatableBindableBase
    {
        private readonly IFilePathGetter _filePathGetter;
        private readonly IMetadataStorageProvider _metadataProvider;
        private readonly IMapper<AssemblyMetadataStorage, MetadataItem> _mapper;
        private readonly IUserInfo _userInfo;

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                SetPropertyAndValidate(ref _filePath, value);
                LoadMetadataCommand.RaiseCanExecuteChanged();
            }
        }

        public IControllableCommand GetFilePathCommand { get; }

        public IControllableCommand LoadMetadataCommand { get; }

        private List<MetadataItem> _treeItems;

        public List<MetadataItem> TreeItems
        {
            get => _treeItems;
            set => SetProperty(ref _treeItems, value);
        }

        public MainViewModel(
            IFilePathGetter filePathGetter,
            IMetadataStorageProvider metadataProvider,
            IMapper<AssemblyMetadataStorage, MetadataItem> mapper,
            IUserInfo userInfo)
        {
            _filePathGetter = filePathGetter;
            _metadataProvider = metadataProvider;
            _mapper = mapper;
            _userInfo = userInfo;
            GetFilePathCommand = new RelayCommand(GetFilePath);
            LoadMetadataCommand = new SimpleAsyncCommand(LoadMetadata, () => File.Exists(FilePath) && FilePath.EndsWith(".dll", System.StringComparison.InvariantCulture));
            TreeItems = new List<MetadataItem>();
        }

        private void GetFilePath()
        {
            FilePath = _filePathGetter.GetFilePath();
        }

        private async Task LoadMetadata()
        {
            await Task.Run(() =>
            {
                try
                {
                    AssemblyMetadataStorage storage = _metadataProvider.GetMetadataStorage(FilePath);
                    TreeItems = new List<MetadataItem>() { _mapper.Map(storage) };
                }
                catch (AssemblyBlockedException)
                {
                    _userInfo.PromptUser("Unblock the selected assembly if you want to read its content!", "Expected Error.");
                }
                catch (ReflectionException e)
                {
                    _userInfo.PromptUser($"Something unexpected happened.\nError message: {e.Message}", "Unexpected ERROR");
                }
            }).ConfigureAwait(false);

            // toDo.Start();
            // await toDo.ConfigureAwait(false);
        }
    }
}