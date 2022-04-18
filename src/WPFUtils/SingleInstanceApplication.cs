#if NET452
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace System.Windows
{
    public abstract partial class SingleInstanceApplication : Application
    {
        protected SingleInstanceApplication()
        {
            Initialize();
        }

        public virtual void Activate() { }

        protected abstract void Initialize();

        public static void Run<T>(string[] args) where T : SingleInstanceApplication, new()
        {
            SingleInstanceManager<T> manager = new SingleInstanceManager<T>();
            manager.Run(args);
        }
    }

    public class SingleInstanceManager<T> : WindowsFormsApplicationBase
    where T : SingleInstanceApplication, new()
    {
        T app;
        public SingleInstanceManager()
        {
            this.IsSingleInstance = true;
        }

        protected override bool OnStartup(Microsoft.VisualBasic.ApplicationServices.StartupEventArgs eventArgs)
        {
            app = new T();
            app.Run();
            return base.OnStartup(eventArgs);
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            base.OnStartupNextInstance(eventArgs);
            app.Activate();
        }
    }

    public static class SingleInstanceManager
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        public static bool IsRunning()
        {
            var processName = Process.GetCurrentProcess().ProcessName;
            return Process.GetProcessesByName(processName).Length > 1;
        }

        public static void RaiseOtherProcess()
        {
            Process proc = Process.GetCurrentProcess();
            foreach (var process in Process.GetProcessesByName(proc.ProcessName))
            {
                if (proc.Id != process.Id)
                {
                    var hWnd = process.MainWindowHandle;
                    if (IsIconic(hWnd))
                    {
                        ShowWindowAsync(hWnd, 9);
                    }

                    SetForegroundWindow(hWnd);
                    break;
                }
            }
        }
    }
}
#endif