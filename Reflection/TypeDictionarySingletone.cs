using System.Collections.Generic;
using DataContract.Model;

namespace Reflection
{
    public sealed class DictionaryTypeSingleton
    {
        private static DictionaryTypeSingleton _instance = null;

        public static DictionaryTypeSingleton Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_padlock)
                {
                    return _instance ?? (_instance = new DictionaryTypeSingleton());
                }

            }
        }

        private static readonly object _padlock = new object();

        private readonly Dictionary<string, TypeModel> _data;

        private DictionaryTypeSingleton()
        {
            _data = new Dictionary<string, TypeModel>();
        }

        public void RegisterType(string name, TypeModel type)
        {
            _data.Add(name, type);
        }

        public bool ContainsKey(string name)
        {
            return _data.ContainsKey(name);
        }

        public TypeModel GetType(string key)
        {
            _data.TryGetValue(key, out TypeModel value);
            return value;
        }

        public void Clear()
        {
            _data.Clear();
        }
    }
}
