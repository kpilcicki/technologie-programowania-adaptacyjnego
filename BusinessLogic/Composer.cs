using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using BusinessLogic.Services;
using BusinessLogic.ViewModel;
using Reflection.Persistence;

namespace BusinessLogic
{
    public class Composer : IDisposable
    {
        private static Composer _composer = null;

        public static Composer Instance
        {
            get
            {
                if (_composer != null) return _composer;

                lock (_padlock)
                {
                    return _composer ?? (_composer = new Composer());
                }
            }
        }

        private static readonly object _padlock = new object();

        private CompositionContainer _container;

        public Composer()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("../../../plugins"));
            _container = new CompositionContainer(catalog);
        }

        public MainViewModel GetComposedMainViewModel(
            IUserInfo userInfo,
            IFilePathGetter filePathGetter,
            IFatalErrorHandler fatalErrorHandler)
        {
            try
            {
                MainViewModel mvm = new MainViewModel(userInfo, filePathGetter)
                {
                    PersistenceService = ComposePersistenceManager()
                };
                _container.ComposeParts(mvm);

                return mvm;
            }
            catch (Exception ex)
            {
                fatalErrorHandler.HandleFatalError(ex.Message);
            }

            return null;
        }

        public PersistenceManager ComposePersistenceManager()
        {
            PersistenceManager pm = new PersistenceManager();
            _container.ComposeParts(pm);
            return pm;
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}
