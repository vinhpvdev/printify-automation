using System.Diagnostics;

namespace PrintifyAutomation
{
    public class BrowserLauncher
    {
        public static void StartBrowserWithDebugging()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                // Update the path to point to Chrome's executable
                FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                Arguments = "--remote-debugging-port=9445 --user-data-dir=\"D:\\selenium\\ChromeProfile\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);
        }
    }
}