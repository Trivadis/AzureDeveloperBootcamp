using System;

namespace Trivadis.AzureBootcamp.CrossCutting.Logging.Null
{
    internal class NullLoggerFactory : LoggerFactory
    {
        public override ILogger CreateLogger(string name)
        {
            return new NullLogger();
        }

        public override ILogger CreateLogger(Type type)
        {
            return new NullLogger();
        }
    }
}
