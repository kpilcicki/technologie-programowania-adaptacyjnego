using System;
using System.Collections.Generic;
using System.Windows.Input;
using BusinessLogic.API;
using BusinessLogic.Base;
using BusinessLogic.Model;
using BusinessLogic.Services;
using DataContract.API;
using DataContract.Model;

namespace BusinessLogic.ViewModel
{
    public class MainViewModel : ValidatableBindableBase
    {
        private readonly IFilePathGetter _filePathGetter;

        public MainViewModel(IFilePathGetter filePathGetter)
        {
            _filePathGetter = filePathGetter;

            GetFilePathCommand = new RelayCommand(() => FilePath = _filePathGetter.GetFilePath());
            GetMetadata = new RelayCommand(() =>
            {
                Reflector.Reflector reflect = new Reflector.Reflector();
                AssemblyMetadataStorage storage = reflect.GetAssemblyMetadata(FilePath);
                IMapper<AssemblyMetadataStorage, Dictionary<string, MetadataItem>> mapper = new MetadataItemMapper();
                var dict = mapper.Map(storage);
                Console.WriteLine("fajno");
            });
        }

        private string _filePath;

        public string FilePath { get => _filePath; set => SetPropertyAndValidate(ref _filePath, value); }

        public ICommand GetFilePathCommand { get; private set; }

        public MetadataItem RootMetadataItem { get; private set; }

        public ICommand GetMetadata { get; private set; }
    }
}
