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

        private void OnShowWindow(object sender, System.EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }

            Activate();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            vm.StopTimer();
        }
    }
}
