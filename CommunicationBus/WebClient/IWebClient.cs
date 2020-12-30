using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    public interface IWebClient
    {
        string Request { get; set; }
        string Verb { get; set; }
        string Noun { get; set; }

        string JsonRequest { get; set; }

        string ConvertToJson(string req);
        string ConvertToJsonQuery(string req);
        string ConvertToJsonFields(string req);
        string ConvertToJsonFull(string req);
        string SendResponse(string response);
        bool ValidateRequest(string request);
        string ConvertResponseToJson(string message);
        string DetermineMethod(string req);
    }
}
