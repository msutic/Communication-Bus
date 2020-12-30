using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    public class ComBus : IComBus
    {
        public XmlAdapter xml { get; set; }
        public XmlToDB db { get; set; }
        public string dbFormat { get; set; }
        public string xmlFormat { get; set; }

        public IComBus myComBus;

        public ComBus()
        {
            xml = new XmlAdapter();
            db = new XmlToDB();
            dbFormat = "";
            xmlFormat = "";
        }

        public ComBus(IComBus iComBus)
        {
            xml = new XmlAdapter();
            db = new XmlToDB();
            myComBus = iComBus;
        }

        public string DetermineMethod(string requestJson, string attr)
        {
            var countArgs = requestJson.Count(x => x == ':');
            string[] tokens = requestJson.Split('"');
            string method = tokens[3];
            string resource = tokens[7];

            xmlFormat = xml.DetermineXmlConvert(requestJson, attr);
            dbFormat = db.DetermineAndConvertToSQL(xmlFormat);

            return dbFormat;
        }
        
    }
}
