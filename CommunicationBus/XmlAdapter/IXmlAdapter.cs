using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    public interface IXmlAdapter
    {

        string jsonRquest { get; set; }

        string ConvertToXml(string jsonReq);

        string FromXmlToJSON(string xmlResponse);

        string ConvertToXmlQuery(string jsonPost);

        string ConvertToXmlFields(string jsonPost);

        string ConvertToXmlQueryFields(string jsonPost);

        string DetermineXmlConvert(string req, string attr);


    }
}
