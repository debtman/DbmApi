namespace DbmClientAvl.ViewModels
{    
    public class MainWindowViewModel : ViewModelBase
    {
        
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "Welcome to Avalonia!";
        public ViewModelBase CurrentView { get; set; } 
#pragma warning restore CA1822 // Mark members as static
        public MainWindowViewModel()
        {
            var loginVM = new LoginViewModel();
            loginVM.LoginSucceeded += OnLoginSucceeded;
            CurrentView = loginVM;
        }

        private void OnLoginSucceeded()
        {
            CurrentView = new MainWindowViewModel();
            OnPropertyChanged(nameof(CurrentView));
        }

    }
}
