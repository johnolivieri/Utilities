using System.ComponentModel;
using System.Windows;
using GenRevision.ViewModels;

namespace GenRevision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = DataContext as MainViewModel;
            vm.ShowWindow += OnShowWindow;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            vm.StopTimer();
        }

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Visibility = Visibility.Hidden;
            }
        }

        private void OnShowWindow(object sender, System.EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
            Focus();
        }
    }
}
