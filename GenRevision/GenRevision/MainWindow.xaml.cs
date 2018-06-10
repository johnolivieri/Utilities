using System;
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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Visibility = Visibility.Hidden;
            }
        }

        public void BringToForeground()
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
            Focus();
        }

        private void OnShowWindow(object sender, EventArgs e)
        {
            BringToForeground();
        }
    }
}
