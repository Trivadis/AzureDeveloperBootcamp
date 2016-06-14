using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceSimulator
{
    public class Device
    {
        private string _deviceKey;
        private string _deviceId;
        private string _iotHubUri;
        private DeviceClient _deviceClient;
        private ConsoleColor _defaultForegroundColor;
        private int _sleepInterval;
        private int _low;
        private int _high;
        private int _avg;


        public Device(string uri, string id, string key, int msgsPerSecond = 1)
        {
            _iotHubUri = uri;
            _deviceId = id;
            _deviceKey = key;
            _defaultForegroundColor = Console.ForegroundColor;
            _sleepInterval = 1000 / msgsPerSecond;

            int MAX = 200;
            int MIN = 0;

            Random rand = new Random();
            _low = rand.Next(0, MAX);
            _high = rand.Next(_low, MAX);

            int difference = _high - _low;


            _avg = _low + difference / 2;




            Console.WriteLine("Simulating Device '{0}'", _deviceId);
            _deviceClient = DeviceClient.Create(_iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(_deviceId, _deviceKey));
            ReceiveC2dAsync();
            SendMessages();

            Console.ReadLine();
        }


        static bool deviceShutdown = false;
        


        private void SendMessages()
        {
            while (!deviceShutdown)
            {
                Task.Factory.StartNew(() => SendDeviceToCloudMessagesAsync());
                Thread.Sleep(_sleepInterval);
            }
        }


        private async void SendDeviceToCloudMessagesAsync()
        {


            Random rand = new Random();


            //double currentWindSpeed = avgWindSpeed + rand.NextDouble() * 4 - 2;
            double currentWindSpeed = _avg + (rand.NextDouble() * (_high - _low));


            var telemetryDataPoint = new
            {
                deviceId = _deviceId,
                windSpeed = currentWindSpeed
            };
            var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            try
            {
                await _deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sent: {1}", Now(), messageString);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} > Error: {1}", Now(), e.Message);
                Console.ForegroundColor = _defaultForegroundColor;
            }
        }









        private async void ReceiveC2dAsync()
        {
            Console.WriteLine("\nReceiving cloud to device messages from service");
            while (true)
            {
                try
                {
                    string messageContent;

                    Message receivedMessage = await _deviceClient.ReceiveAsync();
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

                    await _deviceClient.CompleteAsync(receivedMessage);
                }
                catch (Exception)
                {

                   //do nothing
                }
            }
        }


        private string Now()
        {
            return DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
