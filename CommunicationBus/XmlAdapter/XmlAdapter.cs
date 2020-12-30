using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CommunicationBus
{
    public class XmlAdapter : IXmlAdapter
    {
        public string jsonRquest { get; set; }

        public IXmlAdapter ixml;

        public XmlAdapter(string request)
        {
            jsonRquest = request;
        }

        public XmlAdapter()
        {
            jsonRquest = "";
        }

        public XmlAdapter(IXmlAdapter Ixml)
        {
            ixml = Ixml;
        }

        public string ConvertToXml(string jsonReq)
        {
            if (jsonReq == null)
            {
                throw new ArgumentNullException();
            }

            if (jsonReq == "")
            {
                throw new ArgumentException();
            }


            string[] delovi = jsonReq.Split(':');
            string verb1 = delovi[1];
            string noun1 = delovi[2];

            string verb = verb1.Split('"')[1];
            string noun = noun1.Split('"')[1];

            string rez = "<request>\n  <verb>" + verb + "</verb>\n  <noun>" + noun + "</noun>\n</request>";

            return rez;

        }

        public string FromXmlToJSON(string xmlResponse)
        {
            string status = xmlResponse.Split('>')[2].Split('<')[0];
            int statusCode = int.Parse(xmlResponse.Split('>')[4].Split('<')[0]);
            string jsonResponse = "{";

            if (status.Equals("SUCCESS"))
            {
                var count = xmlResponse.Count(x => x == '>');
                int ponavljanje = count - 6;
                ponavljanje = ponavljanje / 6;

                int getPonavljanje1 = count - 6;
                getPonavljanje1 = getPonavljanje1 / 2;

                int getPonavljanje2 = (count-6) / 4;


                if (xmlResponse.Contains("<id>") && !xmlResponse.Contains("<name>") && !xmlResponse.Contains("surname") ||
                    !xmlResponse.Contains("<id>") && xmlResponse.Contains("<name>") && !xmlResponse.Contains("surname") ||
                    !xmlResponse.Contains("<id>") && !xmlResponse.Contains("<name>") && xmlResponse.Contains("surname"))
                {
                    string attr = xmlResponse.Split('>')[5].Split('<')[1];
                    if (attr == "id")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                    "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < getPonavljanje1; ++i)
                        {
                            int id = int.Parse(xmlResponse.Split('>')[6 + i * 2].Split('<')[0]);
                            jsonResponse += "  \"id\": " + "\"" + id + "\"\n";
                        }

                        jsonResponse += "}";
                    }
                    else if (attr == "name")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                    "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < getPonavljanje1; ++i)
                        {
                            string name = xmlResponse.Split('>')[6 + i * 2].Split('<')[0];
                            jsonResponse += "  \"name\": " + "\"" + name + "\"\n";
                        }
                        jsonResponse += "}";
                    }
                    else if (attr == "surname")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                    "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < getPonavljanje1; ++i)
                        {
                            string surname = xmlResponse.Split('>')[6 + i * 2].Split('<')[0];
                            jsonResponse += "  \"surname\": " + "\"" + surname + "\"\n";
                        }
                        jsonResponse += "}";
                    }
                }
                else if (xmlResponse.Contains("<id>") && xmlResponse.Contains("<name>") && !xmlResponse.Contains("<surname>") ||
                   xmlResponse.Contains("<id>") && xmlResponse.Contains("<surname>") && !xmlResponse.Contains("<name>") ||
                   xmlResponse.Contains("<name>") && xmlResponse.Contains("<surname>") && !xmlResponse.Contains("<id>"))
                {
                    string attr1 = xmlResponse.Split('>')[5].Split('<')[1];
                    string attr2 = xmlResponse.Split('>')[7].Split('<')[1];

                    if (attr1 == "id" && attr2 == "name")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                    "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < getPonavljanje2; ++i)
                        {
                            int id = int.Parse(xmlResponse.Split('>')[6 + i * 4].Split('<')[0]);
                            string name = xmlResponse.Split('>')[8 + i * 4].Split('<')[0];
                            jsonResponse += "  \"id\": " + "\"" + id + "\",\n  " +
                            "\"name\": " + "\"" + name + "\"\n";
                        }
                        jsonResponse += "}";

                    }
                    if (attr1 == "id" && attr2 == "surname")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                    "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < getPonavljanje2; ++i)
                        {
                            int id = int.Parse(xmlResponse.Split('>')[6 + 4 * i].Split('<')[0]);
                            string surname = xmlResponse.Split('>')[8 + 4 * i].Split('<')[0];
                            jsonResponse += "  \"id\": " + "\"" + id + "\",\n  " +
                            "\"surname\": " + "\"" + surname + "\"\n";
                        }
                        jsonResponse += "}";
                    }
                    if (attr1 == "name" && attr2 == "surname")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                   "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < getPonavljanje2; ++i)
                        {
                            string name = xmlResponse.Split('>')[6 + 4 * i].Split('<')[0];
                            string surname = xmlResponse.Split('>')[8 + 4 * i].Split('<')[0];
                            jsonResponse += "  \"name\": " + "\"" + name + "\",\n  " +
                            "\"surname\": " + "\"" + surname + "\"\n";
                        }
                        jsonResponse += "}";
                    }
                    if (attr1 == "name" && attr2 == "id")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                   "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < getPonavljanje2; ++i)
                        {
                            int id = int.Parse(xmlResponse.Split('>')[8 + 4 * i].Split('<')[0]);
                            string name = xmlResponse.Split('>')[6 + 4 * i].Split('<')[0];
                            jsonResponse += "  \"name\": " + "\"" + name + "\",\n  " +
                            "\"id\": " + "\"" + id + "\"\n";
                        }
                        jsonResponse += "}";
                    }
                    if (attr1 == "surname" && attr2 == "id")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                   "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < getPonavljanje2; ++i)
                        {
                            int id = int.Parse(xmlResponse.Split('>')[8 + 4 * i].Split('<')[0]);
                            string surname = xmlResponse.Split('>')[6 + 4 * i].Split('<')[0];
                            jsonResponse += "  \"surname\": " + "\"" + surname + "\",\n  " +
                            "\"id\": " + "\"" + id + "\"\n";
                        }

                        jsonResponse += "}";
                    }
                    if (attr1 == "surname" && attr2 == "name")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                   "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < getPonavljanje2; ++i)
                        {
                            string name = xmlResponse.Split('>')[8 + 4 * i].Split('<')[0];
                            string surname = xmlResponse.Split('>')[6 + 4 * i].Split('<')[0];
                            jsonResponse += "  \"surname\": " + "\"" + surname + "\",\n  " +
                            "\"name\": " + "\"" + name + "\"\n";
                        }

                        jsonResponse += "}";
                    }
                }
                else if (xmlResponse.Contains("<id>") && xmlResponse.Contains("<name>") && xmlResponse.Contains("<surname>"))
                {
                    string attr1 = xmlResponse.Split('>')[5].Split('<')[1];
                    string attr2 = xmlResponse.Split('>')[7].Split('<')[1];
                    string attr3 = xmlResponse.Split('>')[9].Split('<')[1];

                    if (attr1 == "id" && attr2 == "name" && attr3 == "surname")
                    {
                        jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                            "\"statusCode\": " + "\"" + statusCode + "\",\n";
                        for (int i = 0; i < ponavljanje; i++)
                        {
                            int id = int.Parse(xmlResponse.Split('>')[6 + 6 * i].Split('<')[0]);
                            string name = xmlResponse.Split('>')[8 + 6 * i].Split('<')[0];
                            string surname = xmlResponse.Split('>')[10 + 6 * i].Split('<')[0];
                            jsonResponse += "  \"id\": " + "\"" + id + "\",\n  " +
                                "\"name\": " + "\"" + name + "\",\n  " +
                                "\"surname\": " + "\"" + surname + "\"\n";
                        }
                        jsonResponse += "}";
                    }
                }
            }
            else
            {
                string errorMsg = xmlResponse.Split('>')[6].Split('<')[0];
                jsonResponse += "\n  \"status\": " + "\"" + status + "\",\n  " +
                    "\"statusCode\": " + "\"" + statusCode + "\"\n  " +
                    "\"error\": " + "\"" + errorMsg + "\"\n";
                jsonResponse += "}";
            }
            

            return jsonResponse;

        }

        public string ConvertToXmlQuery(string jsonPost)
        {
            if (jsonPost == null)
            {
                throw new ArgumentNullException();
            }

            if (jsonPost == "")
            {
                throw new ArgumentException();
            }

            string[] elements = jsonPost.Split(':');

            string verb = elements[1].Split('"')[1];
            string noun = elements[2].Split('"')[1];
            string query = elements[3].Split('"')[1];

            return "<request>\n  <verb>" + verb + "</verb>\n  <noun>" + noun + "</noun>\n  <query>" + query + "</query>\n</request>";

        }

        public string ConvertToXmlFields(string jsonPost)
        {
            if (jsonPost == null)
            {
                throw new ArgumentNullException();
            }

            if (jsonPost == "")
            {
                throw new ArgumentException();
            }

            string[] elements = jsonPost.Split(':');

            string verb = elements[1].Split('"')[1];
            string noun = elements[2].Split('"')[1];
            string fields = elements[3].Split('"')[1];

            return "<request>\n  <verb>" + verb + "</verb>\n  <noun>" + noun + "</noun>\n  <fields>" + fields + "</fields>\n</request>";

        }

        public string ConvertToXmlQueryFields(string jsonPost)
        {

            if (jsonPost == null)
            {
                throw new ArgumentNullException();
            }

            if (jsonPost == "")
            {
                throw new ArgumentException();
            }

            string[] elements = jsonPost.Split(':');

            string verb = elements[1].Split('"')[1];
            string noun = elements[2].Split('"')[1];
            string query = elements[3].Split('"')[1];
            string fields = elements[4].Split('"')[1];

            return "<request>\n  <verb>" + verb + "</verb>\n  <noun>" + noun + "</noun>\n  <query>" + query + "</query>\n  <fields>" + fields + "</fields>\n</request>";

        }

        public string DetermineXmlConvert(string req, string attr)
        {
            if(attr == "query")
            {
                return ConvertToXmlQuery(req);
            } 
            if(attr == "fields")
            {
                return ConvertToXmlFields(req);
            }
            if(attr == "full")
            {
                return ConvertToXmlQueryFields(req);
            }
            if(attr == "standard")
            {
                return ConvertToXml(req);
            }

            return "";
        }
    }
}
