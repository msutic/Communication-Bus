using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    public interface IXmlToDB
    {
        string ConvertXmlToSQL(string xmlReq);

        string ConvertResponseToXml(string status, int statusCode, string payload);

        string DetermineAndConvertToSQL(string xmlReq);
        string ConvertToSqlQuery(string xmlReq);

        string ConvertToSqlFields(string xmlReq);

        string ConvertToSqlQueryFields(string xmlReq);
    }
}
