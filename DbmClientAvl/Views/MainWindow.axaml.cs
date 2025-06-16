using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using DbmClientAvl.ViewModels;

namespace DbmClientAvl.Views
{
    public partial class MainWindow : Window
    {
        public object CurrentView { get; }
        public MainWindow()
        {            
            InitializeComponent();            
        }
                
                
    }
}