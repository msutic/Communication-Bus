using CommunicationBus;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace DataBaseMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ok;

            do
            {
                string dataBaseName = "";
                Console.WriteLine("Ako zelite da napravite novu bazu u koju cete smestati podatke unesite 1");
                Console.WriteLine("Ako vec imate bazu u koju cete smestati podatke unesite 2");
                string izbor = Console.ReadLine();

                Console.WriteLine("Unesite ip adresu Vaseg servera");
                string server = Console.ReadLine();

                Console.WriteLine("Unesite Vas port");
                string port = Console.ReadLine();

                Console.WriteLine("Unesite Vas username");
                string username = Console.ReadLine();

                Console.WriteLine("Unesite Vasu sifru");
                string password = Console.ReadLine();

                


                try
                {
                    Repository r = new Repository();

                    if (izbor == "1")
                    {
                        
                        string resultCreateDataBase = "";
                        while (resultCreateDataBase != "success")
                        {
                            Console.WriteLine("Unesite ime baze koju zelite da napravite");
                            dataBaseName = Console.ReadLine();
                            resultCreateDataBase = r.createDataBase(dataBaseName, server, port, username, password);
                            if (resultCreateDataBase == "success")
                            {
                                Console.WriteLine("Uspesno kreirana baza " + resultCreateDataBase);
                            }
                            else
                            {
                                Console.WriteLine("Baza sa unetim imenom vec postoji!");

                            }
                        }


                    }
                    else
                    {
                        string rezPostoji = "";

                        while (rezPostoji != "error")
                        {
                            Console.WriteLine("Unesite ime baze koju zelite da koristite");
                            dataBaseName = Console.ReadLine();
                            rezPostoji = r.createDataBase(dataBaseName, server, port, username, password);
                            if (rezPostoji == "success")
                            {
                                Console.WriteLine("Baza sa unetim imenom ne postoji!");

                                string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + password;

                                MySqlConnection conn = new MySqlConnection(connString);
                                MySqlCommand command = conn.CreateCommand();

                                command.CommandText = "DROP DATABASE " + dataBaseName;

                                conn.Open();
                                command.ExecuteNonQuery();
                                conn.Close();

                            }
                        }

                    }

                    string napravioTabelu = r.createTable("korisnici", dataBaseName, server, port, username, password);

                    if (napravioTabelu == "success")
                    {
                        Console.WriteLine("Uspesno napravljena tabela korisnici!");
                    }
                    else
                    {
                        Console.WriteLine("Tabela korisnici vec postoji!");
                    }

                    r.insertInDataBase("Insert into korisnici (id,name,surname) values ('1','Roger','Federer')", dataBaseName, server, port, username, password);
                    r.insertInDataBase("Insert into korisnici (id,name,surname) values ('2','Rafael','Nadal')", dataBaseName, server, port, username, password);
                    r.insertInDataBase("Insert into korisnici (id,name,surname) values ('3','Novak','Djokovic')", dataBaseName, server, port, username, password);

                    Console.WriteLine("Podaci smesteni u tabelu korisnici u bazi " + dataBaseName);

                    string zaUpis = dataBaseName + " " + server + " " + port + " " + username + " " + password;

                    string path = "../../Data/Data.txt";
                    File.WriteAllText(path, zaUpis);

                    ok = true;
                }
                catch
                {
                    ok = false;
                    Console.WriteLine("Podaci nisu ispravni!");
                }

            } while (!ok);
            

            

            Console.ReadLine();

        }
    }
}
