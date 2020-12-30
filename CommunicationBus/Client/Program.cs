using CommunicationBus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = "../../../DataBaseMaker/Data/Data.txt";
            string parameters=File.ReadAllText(path);

            string dataBaseName = parameters.Split(' ')[0];
            string server = parameters.Split(' ')[1];
            string port = parameters.Split(' ')[2];
            string username = parameters.Split(' ')[3];
            string password = parameters.Split(' ')[4];

            Console.WriteLine(dataBaseName);
            Console.WriteLine(server);
            Console.WriteLine(port);
            Console.WriteLine(username);
            Console.WriteLine(password);

            string req;
            do
            {


                WebClient wb = new WebClient();
                do
                {
                    Console.Write("Request input: ");
                    req = Console.ReadLine();
                } while (!wb.ValidateRequest(req));

                string rez = wb.DetermineMethod(req);

                string json = "";
                if (rez == "query")
                {
                    json = wb.ConvertToJsonQuery(req);
                }
                else if (rez == "fields")
                {
                    json = wb.ConvertToJsonFields(req);
                }
                else if (rez == "full")
                {
                    json = wb.ConvertToJsonFull(req);
                }
                else if (rez == "standard")
                {
                    json = wb.ConvertToJson(req);
                }

                ComBus cb = new ComBus();

                string dbFormat = cb.DetermineMethod(json, rez);

                string xmlF = cb.xmlFormat;

                Repository r = new Repository();
                string begin = dbFormat.Split(' ')[0];
                string resultDB = "";

                if (begin.ToLower() == "select")
                {
                    resultDB = r.connectToDataBaseGet(dbFormat, dataBaseName, server, port, username, password);

                }
                else if (begin.ToLower() == "insert")
                {
                    resultDB = r.insertInDataBase(dbFormat, dataBaseName, server, port, username, password);
                }
                else if (begin.ToLower() == "update")
                {
                    resultDB = r.updateInDataBase(dbFormat, dataBaseName, server, port, username, password);
                }
                else if (begin.ToLower() == "delete")
                {
                    resultDB = r.deleteInDataBase(dbFormat, dataBaseName, server, port, username, password);
                }

                r.setResponseParams(resultDB);

                string ajoj = cb.db.ConvertResponseToXml(r.status, r.statusCode, r.payload);

                string dobar = wb.SendResponse(cb.xml.FromXmlToJSON(ajoj));

                Console.WriteLine(dobar);
            } while (true);
            Console.Read();
        }
    }
}
