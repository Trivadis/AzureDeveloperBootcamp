using System;

namespace Trivadis.AzureBootcamp.CrossCutting.Logging
{
    public static class LogManager
    {
        private static LoggerFactory _logger = new Default.DefaultLoggerFactory();

        public static LoggerFactory Factory
        {
            get { return _logger; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                _logger = value;
            }
        }

        public static ILogger GetLogger(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return Factory.CreateLogger(name);
        }

        public static ILogger GetLogger(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return Factory.CreateLogger(type);
        }
    }
}
