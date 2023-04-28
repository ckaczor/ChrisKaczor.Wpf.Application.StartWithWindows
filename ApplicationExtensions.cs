using Microsoft.Win32;
using System;
using System.Reflection;

namespace ChrisKaczor.Wpf.Application
{
    public static class ApplicationExtensions
    {
        public static void SetStartWithWindows(this System.Windows.Application application, bool value)
        {
            var applicationName = Assembly.GetEntryAssembly()!.GetName().Name;

            SetStartWithWindows(application, applicationName!, value);
        }

        public static void SetStartWithWindows(this System.Windows.Application application, string applicationName, bool value)
        {
            var applicationPath = $"\"{Environment.ProcessPath}\"";

            SetStartWithWindows(application, applicationName, applicationPath, value);
        }

        public static void SetStartWithWindows(this System.Windows.Application _, string applicationName, string applicationPath, bool value)
        {
            // Open the run registry key
            var registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true) ?? throw new ApplicationException("Unable to open registry key");

            // Delete any existing key
            registryKey.DeleteValue(applicationName, false);

            // If auto start should not be on then we're done
            if (!value) return;

            // Set the registry key
            registryKey.SetValue(applicationName, applicationPath);
        }
    }
}