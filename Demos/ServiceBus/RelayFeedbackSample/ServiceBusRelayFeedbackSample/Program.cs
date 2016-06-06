using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusRelayFeedbackSample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region mysecret

            string issuer = "RootManageSharedAccessKey";
            string secret = @"<secret>";
            string servicenamespace = "<servicenamespace>";

            #endregion

            // Credentials for enpoint

            TransportClientEndpointBehavior sas = new TransportClientEndpointBehavior();
            var provider = TokenProvider.CreateSharedAccessSignatureTokenProvider(issuer, secret);
            var token = new TransportClientEndpointBehavior(provider);

            // A
            var address = ServiceBusEnvironment.CreateServiceUri
                ("https", servicenamespace, "feedback");

            WebServiceHost host = new WebServiceHost(typeof(FeedbackService), address);
            // B
            var binding = new WebHttpRelayBinding();
            binding.Security.RelayClientAuthenticationType =
                RelayClientAuthenticationType.None;

            // C
            var contract = typeof(IFeedbackContract);

            var cloudEP = host.AddServiceEndpoint(
                contract,
                binding,
                address);
            cloudEP.Behaviors.Add(token);

            ServiceRegistrySettings settings = new ServiceRegistrySettings();
            settings.DiscoveryMode = DiscoveryType.Public;
            cloudEP.Behaviors.Add(settings);

            Console.WriteLine("Listening on:");
            host.Open();
            host.Description.Endpoints.ToList().ForEach(ep => Console.WriteLine(ep.Address));
            Console.WriteLine("\nSend your feedback:");

            Console.ReadLine();
            host.Close();

        }
    }
}
