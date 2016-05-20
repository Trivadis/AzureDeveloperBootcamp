using System;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.CrossCutting.Default
{
    internal class DefaultLoggerFactory : LoggerFactory
    {
        public override ILogger CreateLogger(string name)
        {
            return new DefaultLogger(name);
        }

        public override ILogger CreateLogger(Type type)
        {
            return new DefaultLogger(type);
        }
    }
}
