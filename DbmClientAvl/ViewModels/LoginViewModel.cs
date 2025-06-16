using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbmClientAvl.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty] private string _errorMessage = "";
        [ObservableProperty] private string _username = "";
        [ObservableProperty] private string _password = "";
        public event Action? LoginSucceeded;


        [RelayCommand]
        private async Task Login()
        {
            
        }

    }
}
