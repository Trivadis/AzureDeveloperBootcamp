using System.IO;
using Azure.WebJobs.Shared;
using Microsoft.Azure.WebJobs;

namespace Azure.WebJobs.Sdk
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            DiskSizeCalculator calculator = new DiskSizeCalculator(Config.StorageAccount);
            calculator.Calculate("disksizes");

            log.WriteLine(message);
        }
    }
}
