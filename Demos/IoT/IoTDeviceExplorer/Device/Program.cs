using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() < 3 || args.Count() > 4)
            {
                throw new Exception("Invalid arguments. Must specify: EventHubName (e.g. bootcampIoTHub.azure-devices.net)" +
                    ", DeviceId, DeviceKey, MsgsPerSecond (Optional)");
            }
            else
            {
                var hubName = args[0];
                var deviceId = args[1];
                var deviceKey1 = args[2];
                var msgsPerSecond = 1;

                if(args.Length == 4)
                {
                    msgsPerSecond = int.Parse(args[3]);
                }

                Console.Title = deviceId;
                


                Device d = new Device(hubName, deviceId, deviceKey1, msgsPerSecond);
            }
        }
    }
}
