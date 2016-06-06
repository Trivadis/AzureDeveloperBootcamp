using Azure.WebJobs.Shared;
using Microsoft.Azure.WebJobs;

namespace Azure.WebJobs.Sdk
{
    class Program
    {
        static void Main()
        {
            JobHostConfiguration configuration = new JobHostConfiguration(Config.StorageAccount);
            var host = new JobHost(configuration);
            host.RunAndBlock();
        }
    }
}
