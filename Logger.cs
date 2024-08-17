namespace PrintifyAutomation
{
    public class Logger
    {
        private static string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLogs.txt");
        private static string failedProductFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorProducts.txt");
        public static void LogException(string productName, Exception ex)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine("-------------------------------------------------------------");
                    writer.WriteLine(productName);
                    writer.WriteLine("Date and Time: " + DateTime.Now);
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Stack Trace: " + ex.StackTrace);
                    writer.WriteLine("-------------------------------------------------------------");
                }
            }
            catch (Exception logEx)
            {
                // Handle any errors that might occur during logging
                Console.WriteLine("Error logging exception: " + logEx.Message);
            }
        }
        public static void LogProductThatFailedOnProcess(string productName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(failedProductFilePath, true))
                {
                    writer.WriteLine(productName);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
