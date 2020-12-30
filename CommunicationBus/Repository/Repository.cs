using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CommunicationBus
{
    public class Repository : IRepository
    {
        public string status { get; set; }
        public int statusCode { get; set; }
        public string payload { get; set; }

        public IRepository ir;

        public Repository()
        {
            status = "";
            statusCode = 0;
            payload = "";
        }

        public Repository(IRepository Ir)
        {
            ir = Ir;
        }

        public string createTable(string imeTabele, string imeBaze, string server, string port, string username, string pass)
        {
            if (imeTabele == null || imeBaze == null || server == null || port == null || username == null || pass == null)
            {
                throw new ArgumentNullException("Parametri ne smeju biti null!");
            }

            if (imeTabele == "" || imeBaze == "" || server == "" || port == "" || username == "" || pass == "")
            {
                throw new ArgumentException("Parametri ne smeju biti null!");
            }

            string connString = "Server=" + server + ";port=" + port + ";Database=" + imeBaze + ";Uid=" + username + ";Pwd=" + pass;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "CREATE TABLE " + imeTabele + " (id int not null, name varchar(15), surname varchar(15), primary key(id))";

            conn.Open();

            string greska = "error";
            string success = "success";
            string rez = success;

            try
            {
                command.ExecuteNonQuery();

            }
            catch
            {

                rez = greska;
            }

            conn.Close();
            return rez;
        }

        public string insertInDataBase(string query, string dataBaseName, string server, string port, string username, string pass)
        {
            if (query == null || dataBaseName == null || server == null || port == null || username == null || pass == null)
            {
                throw new ArgumentNullException("Parametri ne smeju biti null!");
            }

            if (query == "" || dataBaseName == "" || server == "" || port == "" || username == "" || pass == "")
            {
                throw new ArgumentException("Parametri ne smeju biti null!");
            }

            string connString = "Server=" + server + ";port=" + port + ";Database=" + dataBaseName + ";Uid=" + username + ";Pwd=" + pass;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = query;

            conn.Open();

            string greska = "error";
            
            string rez = "";

            if (!(query.Contains("id") && query.Contains("name") && query.Contains("surname")))
            {
                rez = greska;
                return rez;
            }

            try
            {
                command.ExecuteNonQuery();

                string pom1 = query.Split('(')[2];
                string id1 = pom1.Split(',')[0];
                string idf = id1.Split('\'')[1];
                string name1 = pom1.Split(',')[1];
                string namef = name1.Split('\'')[1];
                string surname1 = pom1.Split(',')[2];
                string surnamef = surname1.Split('\'')[1];

                rez = idf + " " + namef + " " + surnamef + " all";
            }
            catch
            {
                rez = greska;
            }

            conn.Close();
            return rez;


        }

        public string createDataBase(string dataBaseName, string server, string port, string username, string pass)
        {
            if ( dataBaseName == null || server == null || port == null || username == null || pass == null)
            {
                throw new ArgumentNullException("Parametri ne smeju biti null!");
            }

            if ( dataBaseName == "" || server == "" || port == "" || username == "" || pass == "")
            {
                throw new ArgumentException("Parametri ne smeju biti null!");
            }


            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + pass;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "CREATE SCHEMA " + dataBaseName;

            conn.Open();

            string greska = "error";
            string success = "success";
            string rez = success;

            try
            {
                command.ExecuteNonQuery();
            }
            catch 
            {
                //Console.WriteLine(e.ToString());
                rez = greska;
            }

            conn.Close();
            return rez;
        }

        public string connectToDataBaseGet(string query,string imeBaze,string server,string port, string username, string pass)
        {

            

            if (query == null || imeBaze == null || server == null || port == null || username == null || pass == null)
            {
                throw new ArgumentNullException("Parametri ne smeju biti null!");
            }

            if (query == "" || imeBaze == "" || server == "" || port == "" || username == "" || pass == "")
            {
                throw new ArgumentException("Parametri ne smeju biti null!");
            }

            string begin = query.Split(' ')[0];

            if (begin.ToLower() != "select")
            {
                throw new ArgumentException("Za GET metod se koristi SELECT naredba!");
            }


            string connString = "Server="+server+";port="+port+";Database="+imeBaze+";Uid="+username+";Pwd="+pass;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = query;

            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();

            string ime="";
            string prezime="";
            string id = "";
            string rez = "";
            string rez2 = "";

            string parameters = query.Split(' ')[1];

            if (parameters == "*")
            {
                while (reader.Read())
                {
                    id = reader["id"].ToString();
                    ime = reader["name"].ToString();
                    prezime = reader["surname"].ToString();

                    rez += id + " " + ime + " " + prezime+" ";

                }
                rez2 = "all";
            }else if (parameters == "id")
            {
                while (reader.Read())
                {
                    id = reader["id"].ToString();
                    rez += id + " ";
                }
                rez2 = "i";
            }else if (parameters == "name")
            {
                while (reader.Read())
                {
                    ime = reader["name"].ToString();
                    rez += ime + " ";
                }
                rez2 = "n";
            }
            else if (parameters == "surname")
            {
                while (reader.Read())
                {
                    prezime = reader["surname"].ToString();
                    rez += prezime + " ";
                }
                rez2 = "s";
            }
            else
            {

                string[] fields = parameters.Split(',');

                if (fields.Length == 3)
                {
                    while (reader.Read())
                    {
                        id = reader["id"].ToString();
                        ime = reader["name"].ToString();
                        prezime = reader["surname"].ToString();
                        rez += id + " " + ime + " " + prezime + " ";
                    }
                    rez2 = "all";
                }if (fields.Length == 2)
                {
                    if (fields[0]=="id" && fields[1] == "name")
                    {
                        while (reader.Read())
                        {
                            id = reader["id"].ToString();
                            ime = reader["name"].ToString();
                            rez += id + " " + ime + " ";
                        }
                        rez2 = "in";
                    } else if (fields[1] == "id" && fields[0] == "name")
                    {
                        while (reader.Read())
                        {
                            id = reader["id"].ToString();
                            ime = reader["name"].ToString();
                            rez += ime + " " + id + " ";
                        }
                        rez2 = "ni";
                    }
                    else if (fields[0] == "id" && fields[1] == "surname")
                    {
                        while (reader.Read())
                        {
                            id = reader["id"].ToString();
                            prezime = reader["surname"].ToString();
                            rez += id + " " + prezime + " ";
                        }
                        rez2 = "is";
                    }
                    else if (fields[1] == "id" && fields[0] == "surname")
                    {
                        while (reader.Read())
                        {
                            id = reader["id"].ToString();
                            prezime = reader["surname"].ToString();
                            rez += prezime + " " + id + " ";
                        }
                        rez2 = "si";
                    }
                    else if (fields[0] == "name" && fields[1] == "surname")
                    {
                        while (reader.Read())
                        {
                            ime = reader["name"].ToString();
                            prezime = reader["surname"].ToString();
                            rez += ime + " " + prezime + " ";
                        }
                        rez2 = "ns";
                    }
                    else if (fields[1] == "name" && fields[0] == "surname")
                    {
                        while (reader.Read())
                        {
                            ime = reader["name"].ToString();
                            prezime = reader["surname"].ToString();
                            rez += prezime + " " + ime + " ";
                        }
                        rez2 = "sn";
                    }
                }

            }

            

            return rez+rez2;

        }


        public string updateInDataBase(string query, string dataBaseName, string server, string port, string username, string pass)
        {
            if (query == null || dataBaseName == null || server == null || port == null || username == null || pass == null)
            {
                throw new ArgumentNullException("Parametri ne smeju biti null!");
            }

            if (query == "" || dataBaseName == "" || server == "" || port == "" || username == "" || pass == "")
            {
                throw new ArgumentException("Parametri ne smeju biti null!");
            }

            string begin = query.Split(' ')[0];

            if (begin.ToLower() != "update")
            {
                throw new ArgumentException("Mora da pocinje sa UPDATE!");
            }


            string connString = "Server=" + server + ";port=" + port + ";Database=" + dataBaseName + ";Uid=" + username + ";Pwd=" + pass;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = query;

            conn.Open();

            string greska = "error";
            
            string rez = "";

            try
            {
                command.ExecuteNonQuery();

                string pom1 = query.Replace("id", "|");
                string pom2 = pom1.Split('|')[1];
                string id = pom2.Split('=')[1];

                string imeTabale = query.Split(' ')[1];

                command.CommandText = "Select * from "+imeTabale +" where id=" + id;

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string id1 = reader["id"].ToString();
                    string ime = reader["name"].ToString();
                    string prezime = reader["surname"].ToString();
                    rez = id1 + " " + ime + " " + prezime + " all";
                }
               
            }
            catch
            {
                rez = greska;
            }

            conn.Close();
            return rez;
        }

        public string deleteInDataBase(string query, string dataBaseName, string server, string port, string username, string password)
        {
            if (query == null || dataBaseName == null || server == null || port == null || username == null || password == null)
            {
                throw new ArgumentNullException("Parametri ne smeju biti null!");
            }

            if (query == "" || dataBaseName == "" || server == "" || port == "" || username == "" || password == "")
            {
                throw new ArgumentException("Parametri ne smeju biti prazni stringovi!");
            }

            string begin = query.Split(' ')[0];
            if (begin.ToLower() != "delete")
            {
                throw new ArgumentException("Delete metod mora da pocinje sa BEGIN!");
            }

            string[] delovi = query.Split(' ');
            int brojDelova = delovi.Length;
            string uslov = delovi[brojDelova - 1];

            string[] polja = uslov.Split(',');

            string connString = "Server=" + server + ";port=" + port + ";Database=" + dataBaseName + ";Uid=" + username + ";Pwd=" + password;

            string id = "";
            string name = "";
            string surname = "";
            // string pom = "";

            string rez = "";

            string imeTabele = delovi[2];

            string zahtev = " ";
            for (int i = 4; i < brojDelova; i++)
            {
                zahtev += delovi[i] + " ";
            }

            //string uslov1 = uslov.Replace(",", " and ");

            string select = "Select * from " + imeTabele + " where" + zahtev;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            conn.Open();


            command.CommandText = select;

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id = reader["id"].ToString();
                name = reader["name"].ToString();
                surname = reader["surname"].ToString();
                rez += id + " " + name + " " + surname + " ";
            }

            conn.Close();
            string connString1 = "Server=" + server + ";port=" + port + ";Database=" + dataBaseName + ";Uid=" + username + ";Pwd=" + password;
            MySqlConnection conn1 = new MySqlConnection(connString1);
            MySqlCommand command1 = conn1.CreateCommand();

            conn1.Open();


            command1.CommandText = query;



            rez += "all";

            string output = "";
            int a = 0;

            //command.CommandText = query;


            try
            {
                a =  command1.ExecuteNonQuery();
                output = rez;
            }
            catch 
            {

                output = "error";
                //output = e.ToString();
            }

           conn1.Close();

            if (a == 0)
            {
                output = "error";
            }

            return output;

        }



        public void setResponseParams(string response)
        {
            if (response=="error" || response=="i" || response == "n" || response == "s" || response == "in" || response == "is" || response == "ni" || response == "si" || response == "ns" || response == "sn" || response == "all")
            {
                status = "REJECTED";
                statusCode = 3000;
                payload = "ERROR";
            }
            else
            {
                status = "SUCCESS";
                statusCode = 2000;
                payload = response;
            }
        }
    }
}
