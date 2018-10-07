using System.Threading.Tasks;
using Navigation.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;

namespace Navigation.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        readonly INavigationService _navigationService;

        private string _title = "MainPage";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set {

                SetProperty(ref _isActive, value);
            }
        }

        public DelegateCommand NavigateCommand { get; private set; } 

        public MainPageViewModel(INavigationService navigationService, IEventAggregator ea)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(async () => 
                                                  await Navigate()).ObservesCanExecute(() => IsActive);

            ea.GetEvent<MyEvent>().Subscribe(Handled);
        }

        private void Handled(string obj)
        {
            Title = obj;
        }

        private async Task Navigate()
        {
            await _navigationService.NavigateAsync("ViewA");
        }
    }
}
