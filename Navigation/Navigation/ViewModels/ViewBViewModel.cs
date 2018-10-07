using System.Threading.Tasks;
using Navigation.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;

namespace Navigation.ViewModels
{
    public class ViewBViewModel : BindableBase, INavigationAware
    {
        INavigationService _navigationService;

        private string _title = "View B";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand NavigateCommand { get; private set; }

        IEventAggregator _ea;
        public ViewBViewModel(INavigationService navigationService, IEventAggregator ea)
        {
            _ea = ea;
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(async() => await Navigate());
        }

        private async Task Navigate()
        {
            _ea.GetEvent<MyEvent>().Publish("hello");
            await _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {}

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
                Title = (string)parameters["id"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {}
    }
}
