using System.Collections.ObjectModel;

namespace BusinessLogic.Model
{
    public abstract class MetadataTreeItem
    {
        private bool _wasBuilt;
        private bool _isExpanded;

        public ObservableCollection<MetadataTreeItem> Children { get; }

        protected MetadataTreeItem()
        {
            Children = new ObservableCollection<MetadataTreeItem>() { null };
            _wasBuilt = false;
        }

        public bool IsExpanded
        {
            get => _isExpanded;

            set
            {
                _isExpanded = value;
                if (_wasBuilt)
                    return;
                Children.Clear();
                BuildTreeView();
                _wasBuilt = true;
            }
        }

        protected abstract void BuildTreeView();
    }
}
