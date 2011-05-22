using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;

using System.Xml;

namespace StatisServiceHost
{
    [ServiceContract]
    public interface ICrossDomainService
    {
        [OperationContract]
        [WebGet(UriTemplate = "ClientAccessPolicy.xml")]
        Message ProvidePolicyFile();
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CrossDomainService : ICrossDomainService
    {
        public Message ProvidePolicyFile()
        {
            var filestream = File.Open("ClientAccessPolicy.xml", FileMode.Open);
            // Either specify ClientAccessPolicy.xml file path properly
            // or put that in \Bin folder of the console application
            var reader = XmlReader.Create(filestream);
            var result = Message.CreateMessage(MessageVersion.None, "", reader);
            return result;
        }
    }
}
