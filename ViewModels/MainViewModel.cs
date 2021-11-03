using ClothesForHandsMVVM.Commands;
using ClothesForHandsMVVM.Models;
using System.Windows.Input;

namespace ClothesForHandsMVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel;
        private RelayCommand _changeViewModelCommand;
        private RelayCommand _navigateToAddEditMaterialViewModelCommand;
        
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

        public ICommand NavigateToAddEditMaterialViewModelCommand
        {
            get
            {
                if (_navigateToAddEditMaterialViewModelCommand == null)
                {
                    _navigateToAddEditMaterialViewModelCommand = new RelayCommand(param => SelectedViewModel = new AddEditMaterialViewModel(param as Material));
                }
                return _navigateToAddEditMaterialViewModelCommand;
            }
        }
        public ICommand NavigateToMaterialViewModelCommand
        {
            get
            {
                if (_navigateToAddEditMaterialViewModelCommand == null)
                {
                    _navigateToAddEditMaterialViewModelCommand = new RelayCommand(param => SelectedViewModel = new MaterialViewModel());
                }
                return _navigateToAddEditMaterialViewModelCommand;
            }
        }


        public MainViewModel()
        {
            SelectedViewModel = new MaterialViewModel();
        }
    }
}
