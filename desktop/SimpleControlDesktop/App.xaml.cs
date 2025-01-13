using SimpleControlDesktop.Stores;
using SimpleControlDesktop.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SimpleControlDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ModalNavigationStore.Instance.CurrentModalViewModel = null;
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
