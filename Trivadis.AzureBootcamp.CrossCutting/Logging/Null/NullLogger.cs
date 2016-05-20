namespace Trivadis.AzureBootcamp.CrossCutting.Logging.Null
{
    internal class NullLogger : ILogger
    {
        public void Debug(string format, params object[] args)
        {
        }

        public void Error(string format, params object[] args)
        {
        }

        public void Info(string format, params object[] args)
        {
        }

        public void Warn(string format, params object[] args)
        {
        }
    }
}
