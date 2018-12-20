using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using BusinessLogic.Services;
using BusinessLogic.ViewModel;
using Reflection.Persistence;

namespace BusinessLogic
{
    public static class Composer
    {
        public static MainViewModel GetComposedMainViewModel(
            IUserInfo userInfo,
            IFilePathGetter filePathGetter,
            IFatalErrorHandler fatalErrorHandler)
        {
            MainViewModel mvm = new MainViewModel(userInfo, filePathGetter);

            try
            {
                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog("../../../plugins"));
                CompositionContainer container = new CompositionContainer(catalog);

                PersistenceManager pm = PersistenceManager.GetComposedPersistenceManager();

                container.ComposeParts(mvm);

                mvm.PersistenceService = pm;

                return mvm;
            }
            catch (Exception ex)
            {
                fatalErrorHandler.HandleFatalError(ex.Message);
            }

            return null;
        }
    }
}
