using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusRelayFeedbackSample
{
    [ServiceContract]
    interface IFeedbackContract
    {
        [OperationContract]
        [WebGet(UriTemplate = "/{input}")]
        String Feedback(String input);
    }
}
