using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClothesForHandsMVVM.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ViewModelBase _currentPage;
        public ViewModelBase SelectedViewModel
        {
            get => _currentPage; set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
