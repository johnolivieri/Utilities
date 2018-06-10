using System;
using System.Threading;
using System.Windows;

namespace GenRevision
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string UniqueEventName = "{5230D403-8980-46EE-BBB9-E71239F50FAD}";
        private const string UniqueMutexName = "{A5219A08-8177-4578-BA3B-05BFA97BDCBF}";

        private EventWaitHandle _eventWaitHandle;
        private Mutex _mutex;

        /// <summary>
        /// This code is taken from the post found at https://stackoverflow.com/questions/14506406/wpf-single-instance-best-practices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            _mutex = new Mutex(true, UniqueMutexName, out bool isOwned);
            _eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, UniqueEventName);

            if (isOwned)
            {
                // Spawn a thread which will be waiting for our event
                var thread = new Thread(() =>
                {
                    while (_eventWaitHandle.WaitOne())
                    {
                        Current.Dispatcher.BeginInvoke((Action) (() => ((MainWindow) Current.MainWindow).BringToForeground()));
                    }
                })
                {

                    // It is important mark it as background otherwise it will prevent app from exiting.
                    IsBackground = true
                };

                thread.Start();
                return;
            }

            // Notify other instance so it could bring itself to foreground.
            _eventWaitHandle.Set();

            // Terminate this instance.
            Shutdown();
        }
    }
}
