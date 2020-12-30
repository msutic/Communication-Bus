
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    public class WebClient : IWebClient
    {
        public string Request { get; set; }
        public string Verb { get; set; }
        public string Noun { get; set; }
        public string JsonRequest { get; set; }
        public IWebClient IWb; 

        public WebClient(string request)
        {
            this.Request = request;
        }

        public WebClient()
        {
            Request = "";
            Verb = "";
            Noun = "";
            JsonRequest = "";
        }

        public WebClient(IWebClient iwb)
        {
            IWb = iwb;
        }

        public bool ValidateRequest(string request)
        {
            string method = "";
            string table = "";
            string id = "";

            string error_msg;

            if (request == null)
            {
                error_msg = "Request cannot be empty.";
                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));

                return false;
            }

            if (request == "")
            {
                error_msg = "Request cannot be empty.";
                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                return false;
            }

            if(request.StartsWith(" "))
            {
                error_msg = "Request cannot start with a whitespace.";
                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                return false;
            }

            try
            {
                method = request.Split(' ')[0];
                table = request.Split(' ')[1].Split('/')[1];
                
            }
            catch
            {
                error_msg = "Wrong format. Try GET /resource/1 query fields";
                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                return false;
            }

            try
            {
                id = request.Split(' ')[1].Split('/')[2];
            }
            catch
            {
                if(method != "DELETE" && method != "GET")
                {
                    error_msg = "Wrong format. Try GET /resource/1 query fields";
                    Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                    return false;
                }
            }

            if (method == "")
            {
                error_msg = "Invalid method. Available methods: GET, POST, PATCH, DELETE. (must be all caps)!";
                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                return false;
            }

            if (method != "GET" && method != "POST" && method != "PATCH" && method != "DELETE")
            {
                error_msg = "Invalid method. Available methods: GET, POST, PATCH, DELETE. (must be all caps)!";
                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                return false;
            }
            
            if (table == "")
            {
                error_msg = "Invalid table.";
                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                return false;
            }
            if(table != "korisnici")
            {
                error_msg = "Table does not exist.";
                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                return false;
            }

            if (id == "")
            {
                var countX = request.Count(x => x == '/');
                if (countX > 1)
                {
                    error_msg = "Wrong format. Try GET /resource/1 query fields";
                    Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                    return false;
                }
                if (method != "DELETE" && method != "GET")
                {
                    error_msg = "Invalid id.";
                    Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                    return false;
                }
                
            }

            var countSpaces = request.Count(x => x == ' ');
            if (countSpaces == 1)
            {
                if (request.Split(' ')[0] == "POST")
                {
                    error_msg = "POST method must have query";
                    Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                    return false;
                } else if(request.Split(' ')[0] == "PATCH")
                {
                    error_msg = "PATCH method must have query";
                    Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                    return false;
                }
            }

            if(countSpaces == 2)
            {
                bool qy = false;
                string query = request.Split(' ')[2];
                //string[] pom;
                if (query.Contains("="))
                {
                    qy = true;
                }

                var countSemicolons = request.Count(x => x == ';');
                if (qy == true)
                {
                    
                    if (countSemicolons == 0)
                    {
                        if (request.Split(' ')[0] == "POST")
                        {
                            error_msg = "Both name and surname must exist!";
                            Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                            return false;
                        }
                        string lv;
                        string rv;
                        string attr = query;

                        lv = attr.Split('=')[0];
                        rv = attr.Split('=')[1];

                        if (lv == "name" || lv == "surname")
                        {
                            if (!rv.Substring(0, 1).Equals("\'") || !rv.Substring(rv.Length - 1, 1).Equals("\'"))
                            {
                                error_msg = "Query format error.";
                                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                                return false;
                            }
                        } else
                        {
                            error_msg = "Query must have name or surname, or both.";
                            Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                            return false;
                        }

                    }
                    else if (countSemicolons == 1)
                    {
                        string attr0 = query.Split(';')[0];
                        string attr1 = query.Split(';')[1];

                        string lv0;
                        string rv0;
                        string lv1;
                        string rv1;
                        try
                        {
                            lv0 = attr0.Split('=')[0];
                            lv1 = attr1.Split('=')[0];
                            rv0 = attr0.Split('=')[1];
                            rv1 = attr1.Split('=')[1];
                        }
                        catch
                        {
                            error_msg = "Query format error. Should be: name='yourName';surname='yourSurname'";
                            Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                            return false;
                        }
                        

                        if (lv0 == "name" || lv0 == "surname")
                        {
                            if (!rv0.Substring(0, 1).Equals("\'") || !rv0.Substring(rv0.Length - 1, 1).Equals("\'"))
                            {
                                error_msg = "Query format error. Should be: name='yourName';surname='yourSurname'";
                                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                                return false;
                            }
                        } else
                        {
                            error_msg = "Query format error. Should be: name='yourName';surname='yourSurname'";
                            Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                            return false;
                        }
                        if (lv1 == "name" || lv1 == "surname")
                        {
                            if(rv1 == null || rv1 == "" || rv0 == null || rv0 == "")
                            {
                                error_msg = "Query format error. Should be: name='yourName';surname='yourSurname'";
                                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                                return false;
                            }
                            if (!rv1.Substring(0, 1).Equals("\'") || !rv1.Substring(rv1.Length - 1, 1).Equals("\'"))
                            {
                                error_msg = "Query format error. Should be: name='yourName';surname='yourSurname'";
                                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                                return false;
                            }
                        } else
                        {
                            error_msg = "Query format error. Should be: name='yourName';surname='yourSurname'";
                            Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                            return false;
                        }
                    }
                }
                if(qy == false)
                {
                    if(method == "POST" || method == "PATCH" || method == "DELETE")
                    {
                        error_msg = "Query must exist for this method";
                        Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                        return false;
                    }
                    if (!query.Contains(";"))
                    {
                        if (query != "id" && query != "name" && query != "surname")
                        {
                            error_msg = "Invalid fields. Options: id, name and surname. (format: id;name)";
                            Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                            return false;
                        }
                        
                    } else
                    {
                        if (countSemicolons == 1)
                        {
                            if (query.Split(';')[0] != "name" && query.Split(';')[0] != "surname" && query.Split(';')[0] != "id")
                            {
                                error_msg = "AAAAAAAInvalid fields. Options: id, name and surname. (format: id;name)";
                                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                                return false;
                            }
                            if (query.Split(';')[1] != "name" && query.Split(';')[1] != "surname" && query.Split(';')[1] != "id")
                            {
                                error_msg = "EEEEEEInvalid fields. Options: id, name and surname. (format: id;name)";
                                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                                return false;
                            }
                        }else if(countSemicolons == 2)
                        {
                            if (query.Split(';')[0] != "name" && query.Split(';')[0] != "surname" && query.Split(';')[0] != "id")
                            {
                                error_msg = "AAAAAAAInvalid fields. Options: id, name and surname. (format: id;name)";
                                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                                return false;
                            }
                            if (query.Split(';')[1] != "name" && query.Split(';')[1] != "surname" && query.Split(';')[1] != "id")
                            {
                                error_msg = "EEEEEEInvalid fields. Options: id, name and surname. (format: id;name)";
                                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                                return false;
                            }
                            if (query.Split(';')[2] != "name" && query.Split(';')[2] != "surname" && query.Split(';')[2] != "id")
                            {
                                error_msg = "EEEEEEInvalid fields. Options: id, name and surname. (format: id;name)";
                                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                                return false;
                            }
                        }
                    }
                }
                
                
            } else if(countSpaces == 3)
            {
                if(method == "PATCH" || method == "DELETE")
                {
                    error_msg = "This method does not contain fields";
                    Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                    return false;
                }
                string fields = request.Split(' ')[3];

                if (!request.Contains("="))
                {
                    error_msg = "Invalid query";
                    Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                    return false;
                }

                if (!fields.Contains(";")) {
                    if (fields != "id" && fields != "name" && fields != "surname")
                    {
                        error_msg = "Fields format WRONG. Fields format should be: id;name;surname";
                        Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                        return false;
                    }
                        
                }
            }else if(countSpaces > 3)
            {
                error_msg = "Wrong format.";
                Console.WriteLine(SendResponse(ConvertResponseToJson(error_msg)));
                return false;
            }

            return true;
        }

        public string ConvertToJson(string req)
        {
            Verb = req.Split(' ')[0];
            Noun = req.Split(' ')[1];
            //Verb = method;
            //Noun = resource;
            JsonRequest = "\n{";
            JsonRequest += "\n  \"verb\": " + "\"" + Verb + "\",\n  " +
                "\"noun\": " + "\"" + Noun + "\"\n";
            JsonRequest += "}";
            return JsonRequest;
        }



        public string ConvertToJsonQuery(string req)
        {
            Verb = req.Split(' ')[0];
            Noun = req.Split(' ')[1];

            string query = req.Split(' ')[2];

            JsonRequest = "\n{";
            JsonRequest += "\n  \"verb\": " + "\"" + Verb + "\",\n  " +
                "\"noun\": " + "\"" + Noun + "\",\n  \"query\": " + "\"" + query + "\"\n";
            JsonRequest += "}";
            return JsonRequest;
        }

        public string ConvertToJsonFields(string req)
        {
            Verb = req.Split(' ')[0];
            Noun = req.Split(' ')[1];

            string fields = req.Split(' ')[2];

            JsonRequest = "\n{";
            JsonRequest += "\n  \"verb\": " + "\"" + Verb + "\",\n  " +
                "\"noun\": " + "\"" + Noun + "\",\n  \"fields\": " + "\"" + fields + "\"\n";
            JsonRequest += "}";
            return JsonRequest;
        }

        public string ConvertToJsonFull(string req)
        {
            Verb = req.Split(' ')[0];
            Noun = req.Split(' ')[1];

            string query = req.Split(' ')[2];
            string fields = req.Split(' ')[3];

            JsonRequest = "\n{";
            JsonRequest += "\n  \"verb\": " + "\"" + Verb + "\",\n  " +
                "\"noun\": " + "\"" + Noun + "\",\n  \"query\": " + "\"" + query + "\",\n  " +
                "\"fields\": " + "\"" + fields + "\"\n";
            JsonRequest += "}";
            return JsonRequest;
        }

        public string ConvertResponseToJson(string message)
        {
            return "\n{\n  \"STATUS\": \"BAD_FORMAT\",\n  \"STATUS_CODE\": \"5000\",\n  \"ERROR_MESSAGE\": " + message + "\n";
        }

        public string SendResponse(string response)
        {
            return response;
        }

        public string DetermineMethod(string req)
        {
            var countSpaces = req.Count(x => x == ' ');

            if(countSpaces == 1)
            {
                return "standard";
            } else if (countSpaces == 2)
            {
                if(req.Split(' ')[2].Contains("="))
                {
                    return "query";
                } else
                {
                    return "fields";
                }

            }
            else if(countSpaces == 3)
            {
                return "full";
            }

            return "";
        }
    }
}
