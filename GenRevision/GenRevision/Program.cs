using System;
using System.Reflection;
using System.Threading;

namespace GenRevision
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            // Use the assembly GUID as the name of the mutex which we use to detect if an application instance is already running
            bool createdNew = false;
            string mutexName = Assembly.GetExecutingAssembly().GetType().GUID.ToString();
            using (Mutex mutex = new Mutex(false, mutexName, out createdNew))
            {
                if (!createdNew)
                {
                    // Only allow one instance
                    return;
                }

                App app = new App();
                app.InitializeComponent();
                app.Run();
            }
        }
    }
}
