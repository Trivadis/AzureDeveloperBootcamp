using System;
using Azure.WebJobs.Shared;

namespace Azure.WebJobs.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting....");

            try
            {
                var machinename = System.Environment.MachineName.ToLower();
                DiskSizeCalculator calculator = new DiskSizeCalculator(Config.StorageAccount);
                calculator.Calculate(string.Format("filesizes{0}", machinename));
            }
            catch(Exception ex)
            {
                System.Console.Error.WriteLine(ex.ToString());
            }

            System.Console.WriteLine("Completed!");

        }
    }
}
