using System.Diagnostics;

namespace PrintifyAutomation
{
    public class BrowserLauncher
    {
        public static void StartBrowserWithDebugging()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
                Arguments = "--remote-debugging-port=9444 --user-data-dir=\"D:\\selenium\\EdgeProfile\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);
        }
    }
}