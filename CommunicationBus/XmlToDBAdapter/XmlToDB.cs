using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    public class XmlToDB : IXmlToDB
    {
        public IXmlToDB ixmldb;


        public XmlToDB(IXmlToDB Ixmldb)
        {
            ixmldb = Ixmldb;
        }
        public XmlToDB()
        {

        }

        public string ConvertXmlToSQL(string xmlReq)
        {
            string[] tokens = xmlReq.Split('>');

            string verb = tokens[2].Split('<')[0];
            string noun = tokens[4].Split('<')[0];
            string resource = tokens[4].Split('/')[1];
            int id;
            try
            {
                id = int.Parse(tokens[4].Split('/')[2].Split('<')[0]);
            }
            catch
            {
                resource = resource.Split('<')[0];
                Console.WriteLine(resource);
                return $"SELECT * FROM {resource}";
            }
            
            
            return $"SELECT * FROM {resource} WHERE id={id}";
        }

        public string DetermineAndConvertToSQL(string xmlReq)
        {
            if(xmlReq == "")
            {
                throw new ArgumentException("Request cannot be empty.");
            }

            string method = xmlReq.Split('>')[2].Split('<')[0];
            var count = xmlReq.Count(x => x == '>');
            string[] tokens = xmlReq.Split('>');
            string table = tokens[4].Split('/')[1];

            if (tokens[4].Split('<')[0] == "")
            {
                throw new ArgumentException("Noun is empty");
            }

            string sql = "";

            if (method.ToLower().Equals("get"))
            {
                if (count == 6)
                {
                    sql = ConvertXmlToSQL(xmlReq);
                } else if (count == 8)
                {

                    if (tokens[5].Split('<')[1].Equals("query") && tokens[6].Split('<')[1].Equals("/query"))
                        sql = ConvertToSqlQuery(xmlReq);
                    else if (tokens[5].Split('<')[1].Equals("fields") && tokens[6].Split('<')[1].Equals("/fields"))
                        sql = ConvertToSqlFields(xmlReq);
                    else
                        throw new ArgumentException("Invalid argument.");
                } else if (count == 10)
                {
                    if (tokens[5].Split('<')[1].Equals("query") && tokens[6].Split('<')[1].Equals("/query") && tokens[7].Split('<')[1].Equals("fields") && tokens[8].Split('<')[1].Equals("/fields"))
                        sql = ConvertToSqlQueryFields(xmlReq);
                    else
                        throw new ArgumentException("Invalid arguments.");
                } else
                {
                    throw new IndexOutOfRangeException("Invalid format.");
                }
            }
            else if (method.ToLower().Equals("post"))
            {
                string id = tokens[4].Split('/')[2].Split('<')[0];
                string query = tokens[6].Split('<')[0];

                string nameVal = query.Split(';')[0].Split('=')[1];
                string surnameVal = query.Split(';')[1].Split('=')[1];

                sql = $"INSERT INTO {table} (id,name,surname) VALUES ('{id}',{nameVal},{surnameVal})";
            } else if (method.ToLower().Equals("patch"))
            {
                int id = int.Parse(tokens[4].Split('/')[2].Split('<')[0]);
                string query = tokens[6].Split('<')[0];
                var countArg = query.Count(x => x == ';');

                if (countArg == 0)
                {
                    if (query.Split('=')[0] == "name")
                    {
                        string nameVal = query.Split('=')[1];
                        sql = $"UPDATE {table} SET name={nameVal} WHERE id={id}";
                    } else if (query.Split('=')[0] == "surname")
                    {
                        string surnameVal = query.Split('=')[1];
                        sql = $"UPDATE {table} SET surname={surnameVal} WHERE id={id}";
                    }
                } else if (countArg == 1)
                {
                    if (query.Split(';')[0].Split('=')[0] == "name" && query.Split(';')[1].Split('=')[0] == "surname")
                    {
                        string nameVal = query.Split(';')[0].Split('=')[1];
                        string surnameVal = query.Split(';')[1].Split('=')[1];
                        sql = $"UPDATE {table} SET name={nameVal},surname={surnameVal} WHERE id={id}";
                    }
                    if (query.Split(';')[0].Split('=')[0] == "surname" && query.Split(';')[1].Split('=')[0] == "name")
                    {
                        string nameVal = query.Split(';')[1].Split('=')[1];
                        string surnameVal = query.Split(';')[0].Split('=')[1];
                        sql = $"UPDATE {table} SET name={nameVal},surname={surnameVal} WHERE id={id}";
                    }
                }

            } else if (method.ToLower().Equals("delete"))
            {
                
                
                string noun = xmlReq.Split('>')[4].Split('<')[0];
                int id = -1;
                if (noun.Contains($"/{table}/"))
                {
                    id = int.Parse(tokens[4].Split('/')[2].Split('<')[0]);
                }
                
                if(id == -1)
                {
                    string myTable = noun.Split('/')[1].Split('<')[0];
                    string query = tokens[6].Split('<')[0];
                    var countSemiColons = query.Count(x => x == ';');
                    if (countSemiColons == 0)
                    {
                        string val = query.Split('=')[1];
                        if(query.Split('=')[0] == "name")
                        {
                            sql = $"DELETE FROM {myTable} WHERE name={val}";
                        } else if (query.Split('=')[0] == "surname")
                        {
                            sql = $"DELETE FROM {myTable} WHERE surname={val}";
                        }
                    } else if(countSemiColons == 1)
                    {
                        string val0 = query.Split(';')[0];
                        string val1 = query.Split(';')[1];
                        if (val0.Split('=')[0] == "name" && val1.Split('=')[0] == "surname")
                        {
                            sql = $"DELETE FROM {myTable} WHERE name={val0.Split('=')[1]} AND surname={val1.Split('=')[1]}";
                        } else if (val0.Split('=')[0] == "surname" && val1.Split('=')[0] == "name")
                        {
                            sql = $"DELETE FROM {myTable} WHERE name={val1.Split('=')[1]} AND surname={val0.Split('=')[1]}";
                        }
                    }
                    
                } else
                {
                    if (count == 6)
                    {
                        sql = $"DELETE FROM {table} WHERE id={id}";
                    }
                    else if (count == 8)
                    {
                        string query = tokens[6].Split('<')[0];
                        var countArgs = query.Count(x => x == ';');

                        if (countArgs == 0)
                        {
                            string val = query.Split('=')[1];
                            if (query.Split('=')[0] == "name")
                            {
                                sql = $"DELETE FROM {table} WHERE id={id} AND name={val}";
                            }
                            else if (query.Split('=')[0] == "surname")
                            {
                                sql = $"DELETE FROM {table} WHERE id={id} AND surname={val}";
                            }
                        }
                        else if (countArgs == 1)
                        {
                            if (query.Split(';')[0].Split('=')[0] == "name" && query.Split(';')[1].Split('=')[0] == "surname")
                            {
                                string nameVal = query.Split(';')[0].Split('=')[1];
                                string surnameVal = query.Split(';')[1].Split('=')[1];
                                sql = $"DELETE FROM {table} WHERE id={id} AND name={nameVal} AND surname={surnameVal}";
                            }
                            if (query.Split(';')[0].Split('=')[0] == "surname" && query.Split(';')[1].Split('=')[0] == "name")
                            {
                                string nameVal = query.Split(';')[1].Split('=')[1];
                                string surnameVal = query.Split(';')[0].Split('=')[1];
                                sql = $"DELETE FROM {table} WHERE id={id} AND name={nameVal} AND surname={surnameVal}";
                            }
                        }
                    }
                }
                
            } else
            {
                throw new ArgumentException("Invalid method.");
            }

            return sql;
        }

        public string ConvertToSqlQuery(string xmlReq)
        {
            string[] tokens = xmlReq.Split('>');

            string verb = tokens[2].Split('<')[0];
            string noun = tokens[4].Split('<')[0];
            string resource = tokens[4].Split('/')[1];
            int id = 0;
            bool signal = false;
            try
            {
                id = int.Parse(tokens[4].Split('/')[2].Split('<')[0]);
            }
            catch
            {
                resource = resource.Split('<')[0];
                signal = true;
            }
            
            string query = tokens[6].Split('<')[0];

            string sql = "";

            var count = query.Count(x => x == ';');

            if(count == 0)
            {
                string attribute = query.Split('=')[0];
                string value = query.Split('=')[1];

                if (signal)
                {
                    sql = $"SELECT * FROM {resource} WHERE {attribute}={value}";
                } else
                {
                    sql = $"SELECT * FROM {resource} WHERE id={id} AND {attribute}={value}";
                }
                

            } else if(count == 1)
            {
                string attribute0 = query.Split(';')[0].Split('=')[0];
                string value0 = query.Split(';')[0].Split('=')[1];

                string attribute1 = query.Split(';')[1].Split('=')[0];
                string value1 = query.Split(';')[1].Split('=')[1];

                if (signal)
                {
                    sql = $"SELECT * FROM {resource} WHERE {attribute0}={value0} AND {attribute1}={value1}";

                }
                else
                {
                    sql = $"SELECT * FROM {resource} WHERE id={id} AND {attribute0}={value0} AND {attribute1}={value1}";

                }
            } 

            return sql;
        }

        public string ConvertToSqlFields(string xmlReq)
        {
            string[] tokens = xmlReq.Split('>');

            string verb = tokens[2].Split('<')[0];
            string noun = tokens[4].Split('<')[0];
            string resource = tokens[4].Split('/')[1];
            bool signal = false;
            int id = 0;
            try
            {
                id = int.Parse(tokens[4].Split('/')[2].Split('<')[0]);
            }
            catch
            {
                signal = true;
                resource = resource.Split('<')[0];
            }
            
            string fields = tokens[6].Split('<')[0];

            string sql = "";

            var count = fields.Count(x => x == ';');

            if (count == 0)
            {
                string attribute = fields;

                if (signal)
                {
                    sql = $"SELECT {attribute} FROM {resource}";
                } else
                {
                    sql = $"SELECT {attribute} FROM {resource} WHERE id={id}";
                }
                

            }
            else if (count == 1)
            {
                string attribute0 = fields.Split(';')[0];
                string attribute1 = fields.Split(';')[1];

                if (signal)
                {
                    sql = $"SELECT {attribute0},{attribute1} FROM {resource}";
                } else
                {
                    sql = $"SELECT {attribute0},{attribute1} FROM {resource} WHERE id={id}";
                }
                
            }
            else if (count == 2)
            {
                string attribute0 = fields.Split(';')[0];
                string attribute1 = fields.Split(';')[1];
                string attribute2 = fields.Split(';')[2];

                if (signal)
                {
                    sql = $"SELECT {attribute0},{attribute1},{attribute2} FROM {resource}";
                } else
                {
                    sql = $"SELECT {attribute0},{attribute1},{attribute2} FROM {resource} WHERE id={id}";
                }
                
            }

            return sql;
        }

        public string ConvertToSqlQueryFields(string xmlReq)
        {
            string[] tokens = xmlReq.Split('>');

            string verb = tokens[2].Split('<')[0];
            string noun = tokens[4].Split('<')[0];
            string resource = tokens[4].Split('/')[1];
            int id = 0;
            bool signal = false;
            try
            {
                id = int.Parse(tokens[4].Split('/')[2].Split('<')[0]);
            }
            catch
            {
                signal = true;
                resource = resource.Split('<')[0];
            }
            string query = tokens[6].Split('<')[0];
            string fields = tokens[8].Split('<')[0];

            string sql = "";

            var countQuery = query.Count(x => x == ';');
            var countFields = fields.Count(y => y == ';');


            if (countFields == 0)
            {
                string attribute = fields;

                sql = $"SELECT {attribute} FROM {resource} ";
            }
            else if (countFields == 1)
            {
                string attribute0 = fields.Split(';')[0];
                string attribute1 = fields.Split(';')[1];

                sql = $"SELECT {attribute0},{attribute1} FROM {resource} ";
            }
            else if (countFields == 2)
            {
                string attribute0 = fields.Split(';')[0];
                string attribute1 = fields.Split(';')[1];
                string attribute2 = fields.Split(';')[2];

                sql = $"SELECT {attribute0},{attribute1},{attribute2} FROM {resource} ";
            }



            if (countQuery == 0)
            {
                string attribute = query.Split('=')[0];
                string value = query.Split('=')[1];

                if (signal)
                {
                    sql += $"WHERE {attribute}={value}";
                }
                else
                {
                    sql += $"WHERE id={id} AND {attribute}={value}";
                }
                
            }
            else if (countQuery == 1)
            {
                string attribute0 = query.Split(';')[0].Split('=')[0];
                string value0 = query.Split(';')[0].Split('=')[1];

                string attribute1 = query.Split(';')[1].Split('=')[0];
                string value1 = query.Split(';')[1].Split('=')[1];

                if (signal)
                {
                    sql += $"WHERE {attribute0}={value0} AND {attribute1}={value1}";
                } else
                {
                    sql += $"WHERE id={id} AND {attribute0}={value0} AND {attribute1}={value1}";
                }
                
            }
            else if (countQuery == 2)
            {
                string attribute0 = query.Split(';')[0].Split('=')[0];
                string value0 = query.Split(';')[0].Split('=')[1];

                string attribute1 = query.Split(';')[1].Split('=')[0];
                string value1 = query.Split(';')[1].Split('=')[1];

                string attribute2 = query.Split(';')[2].Split('=')[0];
                string value2 = query.Split(';')[2].Split('=')[1];

                if (signal)
                {
                    sql += $"WHERE {attribute0}={value0} AND {attribute1}={value1} AND {attribute2}={value2}";
                } else
                {
                    sql += $"WHERE id={id} AND {attribute0}={value0} AND {attribute1}={value1} AND {attribute2}={value2}";
                }
                
            }

            return sql;
        }

        public string ConvertResponseToXml(string status, int statusCode, string payload)
        {
            if (status=="" || payload == "")
            {
                throw new ArgumentException("Status i payload ne smeju biti prazni stringovi!");
            }

            if (status == null || payload == null)
            {
                throw new ArgumentNullException("Status i payload ne smeju biti null!");
            }

            string[] temp = payload.Split(' ');

            int duzina = temp.Length;
            string marker = temp[duzina - 1];
            string rez = "";

            if (status=="SUCCESS" && marker=="all")
            {
                rez= "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                //int part = (duzina - 1) / 3;
                for (int i = 0; i < duzina-1; i=i+3)
                {
                    string id = temp[i];
                    string name = temp[i+1];
                    string surname = temp[i+2];

                    rez += "\n  <id>" + id + "</id>\n  <name>" + name + "</name>\n  <surname>" + surname + "</surname>";

                }
                rez += end;

                return rez;
            }else if (status == "SUCCESS" && marker == "i")
            {
                rez = "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                for (int i = 0; i < duzina - 1; i++)
                {
                    string id = temp[i];
                    

                    rez += "\n  <id>" + id + "</id>";

                }
                rez += end;

                return rez;
            }
            else if (status == "SUCCESS" && marker == "n")
            {
                rez = "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                for (int i = 0; i < duzina - 1; i++)
                {
                    string name = temp[i];


                    rez += "\n  <name>" + name + "</name>";

                }
                rez += end;

                return rez;
            }
            else if (status == "SUCCESS" && marker == "s")
            {
                rez = "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                for (int i = 0; i < duzina - 1; i++)
                {
                    string surname = temp[i];


                    rez += "\n  <surname>" + surname + "</surname>";

                }
                rez += end;

                return rez;
            }
            else if (status == "SUCCESS" && marker == "in")
            {
                rez = "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                for (int i = 0; i < duzina - 1; i=i+2)
                {
                    string id = temp[i];
                    string name = temp[i + 1];


                    rez += "\n  <id>" + id + "</id>\n  <name>" + name + "</name>";

                }
                rez += end;

                return rez;
            }
            else if (status == "SUCCESS" && marker == "ni")
            {
                rez = "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                for (int i = 0; i < duzina - 1; i = i + 2)
                {
                    string name = temp[i];
                    string id = temp[i+1];

                    rez += "\n  <name>" + name + "</name>\n  <id>" + id + "</id>";

                }
                rez += end;

                return rez;
            }
            else if (status == "SUCCESS" && marker == "is")
            {
                rez = "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                for (int i = 0; i < duzina - 1; i = i + 2)
                {
                    string id = temp[i];
                    string surname = temp[i + 1];

                    rez += "\n  <id>" + id + "</id>\n  <surname>" + surname + "</surname>";

                }
                rez += end;

                return rez;
            }
            else if (status == "SUCCESS" && marker == "si")
            {
                rez = "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                for (int i = 0; i < duzina - 1; i = i + 2)
                {
                    string surname = temp[i];
                    string id = temp[i+1];

                    rez += "\n  <surname>" + surname + "</surname>\n  <id>" + id + "</id>";

                }
                rez += end;

                return rez;
            }
            else if (status == "SUCCESS" && marker == "ns")
            {
                rez = "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                for (int i = 0; i < duzina - 1; i = i + 2)
                {
                    string name = temp[i];
                    string surname = temp[i + 1];

                    rez += "\n  <name>" + name + "</name>\n  <surname>" + surname + "</surname>";

                }
                rez += end;

                return rez;
            }
            else if (status == "SUCCESS" && marker == "sn")
            {
                rez = "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>";
                string end = "\n</response>";
                for (int i = 0; i < duzina - 1; i = i + 2)
                {
                    string surname = temp[i];
                    string name = temp[i+1];

                    rez += "\n  <surname>" + surname + "</surname>\n  <name>" + name + "</name>";

                }
                rez += end;

                return rez;
            }
            else
            {
                return "<response>\n  <status>" + status + "</status>\n  <statusCode>" + statusCode.ToString() + "</statusCode>\n  <errorMessage>"+payload+"</errorMessage>\n</response>";
            }
        }

    }
}
