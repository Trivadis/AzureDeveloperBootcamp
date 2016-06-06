using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading;

namespace SimulatedDevice
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "tvdiot.azure-devices.net";
        static string deviceKey = "chCHLuAr+SqIGJGIUpwsnyu2xMQkujYuI2hsh1DOK+0=";
        static bool deviceShutdown = false;
        static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("Gerry", deviceKey));
            SendDeviceToCloudMessagesAsync();
            ReceiveC2dAsync();
            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {
            double avgWindSpeed = 10; // m/s
            Random rand = new Random();

            while (!deviceShutdown)
            {
                double currentWindSpeed = avgWindSpeed + rand.NextDouble() * 4 - 2;

                var telemetryDataPoint = new
                {
                    deviceId = "Gerry",
                    windSpeed = currentWindSpeed
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                Thread.Sleep(1000);
            }
        }

        private static async void ReceiveC2dAsync()
        {
            Console.WriteLine("\nReceiving cloud to device messages from service");
            while (true)
            {
                string messageContent;

                Message receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;

                messageContent = Encoding.ASCII.GetString(receivedMessage.GetBytes());

                if (messageContent.Contains("Shutdown"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Shutting down device ...");
                    Console.ResetColor();
                    deviceShutdown = true;
                }
                else if (messageContent.Contains("Startup"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Starting up device ...");
                    Console.ResetColor();
                    deviceShutdown = false;
                    SendDeviceToCloudMessagesAsync();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Received message: {0}", messageContent);
                    Console.ResetColor();

                }

                await deviceClient.CompleteAsync(receivedMessage);
            }
        }
    }
}
