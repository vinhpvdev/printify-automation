namespace PrintifyAutomation
{
    public class AutomationStepException : Exception
    {
        public string Step { get; }

        public AutomationStepException(string step, string message, Exception innerException)
            : base($"{step} failed: {message}", innerException)
        {
            Step = step;
        }
    }
}
