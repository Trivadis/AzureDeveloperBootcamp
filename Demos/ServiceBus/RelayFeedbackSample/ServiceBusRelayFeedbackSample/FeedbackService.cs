using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusRelayFeedbackSample
{
    [ServiceBehavior(Name = "FeebackService", Namespace = "http://samples.microsoft.com/ServiceModel/Relay/")]
    class FeedbackService : IFeedbackContract
    {

        public string Feedback(string input)
        {
            String reply = "You said: " + input;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(reply);
            Console.ResetColor();
            return reply;
        }
    }
}
