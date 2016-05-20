using System;

namespace Trivadis.AzureBootcamp.CrossCutting.Logging
{
    public abstract class LoggerFactory
    {
        public abstract ILogger CreateLogger(Type type);
        public abstract ILogger CreateLogger(String name);
    }
}
