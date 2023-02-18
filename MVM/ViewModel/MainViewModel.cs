using ST10092081POEBudgetApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10092081POEBudgetApp.MVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        //Relay commands for all the radio buttons
        public RelayCommand MenuViewCommand { get; set; }

        public RelayCommand HomeLoanViewCommand { get; set; }

        public RelayCommand RentPropertyViewCommand { get; set; }

        public RelayCommand SavingsViewCommand { get; set; }

        public RelayCommand VehiclePurchaseViewCommand { get; set; }



        public MenuViewModel MenuVM { get; set; }

        public HomeLoanViewModel HomeLoanVM { get; set; }

        public RentPropertyViewModel RentPropertyVM { get; set; }

        public SavingsViewModel SavingsVM { get; set; }

        public VehiclePurchaseViewModel VehiclePurchaseVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MainViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            MenuVM = new MenuViewModel();
            HomeLoanVM = new HomeLoanViewModel();
            RentPropertyVM = new RentPropertyViewModel();
            SavingsVM = new SavingsViewModel();
            VehiclePurchaseVM = new VehiclePurchaseViewModel();

            CurrentView = MenuVM;


            MenuViewCommand = new RelayCommand(o =>
            {
                CurrentView = MenuVM;
            });

            HomeLoanViewCommand = new RelayCommand(o =>
            {
               CurrentView = HomeLoanVM;
            });

            RentPropertyViewCommand = new RelayCommand(o =>
            {
                CurrentView = RentPropertyVM;
            });

            SavingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SavingsVM;
            });

            VehiclePurchaseViewCommand = new RelayCommand(o =>
            {
                CurrentView = VehiclePurchaseVM;
            });
        }
    }
}
