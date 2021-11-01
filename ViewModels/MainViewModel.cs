using ClothesForHandsMVVM.Commands;
using System.Windows.Input;

namespace ClothesForHandsMVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel;
        private RelayCommand _changeViewModelCommand;
        public ICommand ChangeViewModelCommand
        {
            get
            {
                if (_changeViewModelCommand == null)
                {
                    _changeViewModelCommand = new RelayCommand(param => SelectedViewModel = (ViewModelBase)param);
                }
                return _changeViewModelCommand;
            }
        }
        public ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel; set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            SelectedViewModel = new MaterialViewModel();
        }
    }
}
