using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using BusinessLogic.Services;

namespace BusinessLogic.ViewModel
{
    public static class Composer
    {
        public static MainViewModel GetComposedMainViewModel(
            IUserInfo userInfo,
            IFilePathGetter filePathGetter)
        {
            MainViewModel mvm = new MainViewModel(userInfo, filePathGetter);

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("../../../FileSerializer/bin/Debug"));
            catalog.Catalogs.Add(new DirectoryCatalog("../../../FileLogger/bin/Debug"));
            CompositionContainer container = new CompositionContainer(catalog);

            container.ComposeParts(mvm);

            return mvm;
        }
    }
}
