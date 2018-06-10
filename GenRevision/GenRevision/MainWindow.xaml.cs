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

        private void OnShowWindow(object sender, System.EventArgs e)
        {
            Visibility = Visibility.Visible;
            Activate();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }
    }
}
