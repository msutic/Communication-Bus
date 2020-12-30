using Moq;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    [TestFixture]
    public class RepositoryTest
    {

        [Test]
        [TestCase("",0,"")]
        public void constEmptyOk1(string a, int b, string c)
        {
            Repository r = new Repository();

            Assert.AreEqual(r.status, a);
            Assert.AreEqual(r.statusCode, b);
            Assert.AreEqual(r.payload, c);
        }

        [Test]
        [TestCase("aaa", 5, "")]
        public void constEmptyOk6(string a, int b, string c)
        {
            Repository r = new Repository();

            Assert.AreNotEqual(r.status, a);
            Assert.AreNotEqual(r.statusCode, b);
            Assert.AreEqual(r.payload, c);
        }

        [Test]
        [TestCase("aaa", 0, "bbb")]
        public void constEmptyOk7(string a, int b, string c)
        {
            Repository r = new Repository();

            Assert.AreNotEqual(r.status, a);
            Assert.AreEqual(r.statusCode, b);
            Assert.AreNotEqual(r.payload, c);
        }

        [Test]
        [TestCase("", 5, "bbb")]
        public void constEmptyOk8(string a, int b, string c)
        {
            Repository r = new Repository();

            Assert.AreEqual(r.status, a);
            Assert.AreNotEqual(r.statusCode, b);
            Assert.AreNotEqual(r.payload, c);
        }


        [Test]
        [TestCase("", 0, "bbb")]
        public void constEmptyOk5(string a, int b, string c)
        {
            Repository r = new Repository();

            Assert.AreEqual(r.status, a);
            Assert.AreEqual(r.statusCode, b);
            Assert.AreNotEqual(r.payload, c);
        }


        [Test]
        [TestCase("", 5, "")]
        public void constEmptyOk4(string a, int b, string c)
        {
            Repository r = new Repository();

            Assert.AreEqual(r.status, a);
            Assert.AreNotEqual(r.statusCode, b);
            Assert.AreEqual(r.payload, c);
        }

        [Test]
        [TestCase("aaa", 0, "")]
        public void constEmptyOk3(string a, int b, string c)
        {
            Repository r = new Repository();

            Assert.AreNotEqual(r.status, a);
            Assert.AreEqual(r.statusCode, b);
            Assert.AreEqual(r.payload, c);
        }


        [Test]
        [TestCase("aaa", 5, "bbb")]
        public void constEmptyOk2(string a, int b, string c)
        {
            Repository r = new Repository();

            Assert.AreNotEqual(r.status, a);
            Assert.AreNotEqual(r.statusCode, b);
            Assert.AreNotEqual(r.payload, c);
        }



        [Test]
        [TestCase("error","REJECTED",3000,"ERROR")]
        [TestCase("1 Roger Federer","SUCCESS",2000,"1 Roger Federer")]
        [TestCase("1 Roger","SUCCESS",2000,"1 Roger")]
        [TestCase("1","SUCCESS",2000,"1")]
        public void setResponseParamsOk1(string input, string statusOutput, int statusCodeOutput, string payloadOutput)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.setResponseParams(input);
            Assert.AreEqual(r.status, statusOutput);
            Assert.AreEqual(r.statusCode, statusCodeOutput);
            Assert.AreEqual(r.payload, payloadOutput);
            
        }

        [Test]
        [TestCase("error", "REJECTED", 2000, "aaa")]
        [TestCase("1 Roger Federer", "SUCCESS", 3000, "ERROR")]
        [TestCase("1 Roger", "SUCCESS", 3000, "ERROR")]
        [TestCase("1", "SUCCESS", 3000, "ERROR")]
        public void setResponseParamsOk8(string input, string statusOutput, int statusCodeOutput, string payloadOutput)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.setResponseParams(input);
            Assert.AreEqual(r.status, statusOutput);
            Assert.AreNotEqual(r.statusCode, statusCodeOutput);
            Assert.AreNotEqual(r.payload, payloadOutput);

        }


        [Test]
        [TestCase("error", "SUCCESS", 3000, "aaa")]
        [TestCase("1 Roger Federer", "ERROR", 2000, "bbb")]
        [TestCase("1 Roger", "ERROR", 2000, "bbb")]
        [TestCase("1", "ERROR", 2000, "bbb")]
        public void setResponseParamsOk7(string input, string statusOutput, int statusCodeOutput, string payloadOutput)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.setResponseParams(input);
            Assert.AreNotEqual(r.status, statusOutput);
            Assert.AreEqual(r.statusCode, statusCodeOutput);
            Assert.AreNotEqual(r.payload, payloadOutput);

        }


        [Test]
        [TestCase("error", "REJECTED", 3000, "aaa")]
        [TestCase("1 Roger Federer", "SUCCESS", 2000, "Federer")]
        [TestCase("1 Roger", "SUCCESS", 2000, "Roger")]
        [TestCase("1", "SUCCESS", 2000, "Federer")]
        public void setResponseParamsOk2(string input, string statusOutput, int statusCodeOutput, string payloadOutput)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.setResponseParams(input);
            Assert.AreEqual(r.status, statusOutput);
            Assert.AreEqual(r.statusCode, statusCodeOutput);
            Assert.AreNotEqual(r.payload, payloadOutput);

        }

        [Test]
        [TestCase("error", "SUCCESS", 3000, "ERROR")]
        [TestCase("1 Roger Federer", "REJECTED", 2000, "1 Roger Federer")]
        [TestCase("1 Roger", "REJECTED", 2000, "1 Roger")]
        [TestCase("1", "REJECTED", 2000, "1")]
        public void setResponseParamsOk3(string input, string statusOutput, int statusCodeOutput, string payloadOutput)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.setResponseParams(input);
            Assert.AreNotEqual(r.status, statusOutput);
            Assert.AreEqual(r.statusCode, statusCodeOutput);
            Assert.AreEqual(r.payload, payloadOutput);

        }

        [Test]
        [TestCase("error", "SUCCESS", 2000, "ERROR")]
        [TestCase("1 Roger Federer", "REJECTED", 3000, "1 Roger Federer")]
        [TestCase("1 Roger", "REJECTED", 3000, "1 Roger")]
        [TestCase("1", "REJECTED", 3000, "1")]
        public void setResponseParamsOk5(string input, string statusOutput, int statusCodeOutput, string payloadOutput)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.setResponseParams(input);
            Assert.AreNotEqual(r.status, statusOutput);
            Assert.AreNotEqual(r.statusCode, statusCodeOutput);
            Assert.AreEqual(r.payload, payloadOutput);

        }

        [Test]
        [TestCase("error", "SUCCESS", 2000, "rf")]
        [TestCase("1 Roger Federer", "REJECTED", 3000, "error")]
        [TestCase("1 Roger", "REJECTED", 3000, "error")]
        [TestCase("1", "REJECTED", 3000, "error")]
        public void setResponseParamsOk6(string input, string statusOutput, int statusCodeOutput, string payloadOutput)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.setResponseParams(input);
            Assert.AreNotEqual(r.status, statusOutput);
            Assert.AreNotEqual(r.statusCode, statusCodeOutput);
            Assert.AreNotEqual(r.payload, payloadOutput);

        }

        [Test]
        [TestCase("error", "REJECTED", 2000, "ERROR")]
        [TestCase("1 Roger Federer", "SUCCESS", 3000, "1 Roger Federer")]
        [TestCase("1 Roger", "SUCCESS", 3000, "1 Roger")]
        [TestCase("1", "SUCCESS", 3000, "1")]
        public void setResponseParamsOk4(string input, string statusOutput, int statusCodeOutput, string payloadOutput)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.setResponseParams(input);
            Assert.AreEqual(r.status, statusOutput);
            Assert.AreNotEqual(r.statusCode, statusCodeOutput);
            Assert.AreEqual(r.payload, payloadOutput);

        }

        [Test]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "")]
        [TestCase("Select * from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "")]
        [TestCase("cao cao cao", "proba", "127.0.0.1", "3306", "sharegoat98", "")]

        public void insertInDataBaseBeginError(string query, string dataBase, string server, string port, string username, string passowrd)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.insertInDataBase(query, dataBase, server, port, username, passowrd);
            });

        }



        [Test]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')","proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        public void insertInDataBaseTestOk1(string query,string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "5 Viktor Troicki all";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase,server,port,username,passowrd);
            r.createTable("tabelaProba", dataBase,server,port,username,passowrd);
            string rez = r.insertInDataBase(query,dataBase,server,port,username,passowrd);
            string connString = "Server="+server+";port="+port+";Uid="+username+";Pwd="+passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE "+dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output,rez);
        }

        [Test]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        public void insertInDataBaseTestOk2(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "error";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase(query, dataBase, server, port, username, passowrd);
            string rez = r.insertInDataBase(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        

        [Test]
        [TestCase("Insert into tabelaProba (name,surname) values ('Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaP (id,name,ppp) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (polje1,polje2,polje3) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id) values ('5')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (name) values ('Viktor')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (surname) values ('Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name) values ('5','Viktor')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,surname) values ('5','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (name,surname) values ('Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void insertInDataBaseTestOk3(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "error";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            string rez = r.insertInDataBase(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase(null, "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", null, "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", null)]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", null, "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", null, "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", null, "Aleksasare5")]
        
       public void insertInDataBaseTestNull1(string query, string dataBase, string server, string port, string username, string passowrd)
        {


            
            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                string rez = r.insertInDataBase(query, dataBase, server, port, username, passowrd);
            });
               
        }


        [Test]
        [TestCase("", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "", "Aleksasare5")]

        public void insertInDataBaseTestEmpty(string query, string dataBase, string server, string port, string username, string passowrd)
        {


            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.insertInDataBase(query, dataBase, server, port, username, passowrd);
            });

        }

        [Test]
        [TestCase("probaTabela","proba","127.0.0.1","3306","sharegoat98","Aleksasare5")]
        public void createTableTestOk1(string imeTabele, string imeBaze, string server, string port, string username, string password)
        {
            string output = "success";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(imeBaze, server, port, username, password);
            string rez = r.createTable(imeTabele, imeBaze, server, port, username, password);
            
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + password;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + imeBaze;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase("", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("probaTabela", "", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("probaTabela", "proba", "", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("probaTabela", "proba", "127.0.0.1", "", "sharegoat98", "Aleksasare5")]
        [TestCase("probaTabela", "proba", "127.0.0.1", "3306", "", "Aleksasare5")]
        [TestCase("probaTabela", "proba", "127.0.0.1", "3306", "sharegoat98", "")]
        public void createTableTestEmpty(string imeTabele, string imeBaze, string server, string port, string username, string password)
        {


            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.createTable(imeTabele, imeBaze, server, port, username, password);
            });
            
        }

        [Test]
        [TestCase(null, "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("probaTabela", null, "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("probaTabela", "proba", null, "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("probaTabela", "proba", "127.0.0.1", null, "sharegoat98", "Aleksasare5")]
        [TestCase("probaTabela", "proba", "127.0.0.1", "3306", null, "Aleksasare5")]
        [TestCase("probaTabela", "proba", "127.0.0.1", "3306", "sharegoat98", null)]
        public void createTableTestNull(string imeTabele, string imeBaze, string server, string port, string username, string password)
        {


            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);


            Assert.Throws<ArgumentNullException>(() =>
            {
                string rez = r.createTable(imeTabele, imeBaze, server, port, username, password);
            });

        }

        [Test]
        [TestCase("probaTabela", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        public void createTableTestNotOk1(string imeTabele, string imeBaze, string server, string port, string username, string password)
        {
            string output = "error";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(imeBaze, server, port, username, password);
            r.createTable(imeTabele, imeBaze, server, port, username, password);
            string rez= r.createTable(imeTabele, imeBaze, server, port, username, password);

            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + password;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + imeBaze;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase("proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        public void createDataBaseTestOk1(string imeBaze, string server, string port, string username, string password)
        {
            string output = "success";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            string rez = r.createDataBase(imeBaze, server, port, username, password);
            

            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + password;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + imeBaze;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase("proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        public void createDataBaseTestNotOk(string imeBaze, string server, string port, string username, string password)
        {
            string output = "error";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(imeBaze, server, port, username, password);
            string rez = r.createDataBase(imeBaze, server, port, username, password);


            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + password;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + imeBaze;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        
        [TestCase( null, "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase( "proba", null, "3306", "sharegoat98", "Aleksasare5")]
        [TestCase( "proba", "127.0.0.1", null, "sharegoat98", "Aleksasare5")]
        [TestCase( "proba", "127.0.0.1", "3306", null, "Aleksasare5")]
        [TestCase( "proba", "127.0.0.1", "3306", "sharegoat98", null)]
        public void createDataBaseTestNull( string imeBaze, string server, string port, string username, string password)
        {


            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);


            Assert.Throws<ArgumentNullException>(() =>
            {
                string rez = r.createDataBase(imeBaze, server, port, username, password);
            });

        }

        [TestCase("", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("proba", "", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("proba", "127.0.0.1", "", "sharegoat98", "Aleksasare5")]
        [TestCase("proba", "127.0.0.1", "3306", "", "Aleksasare5")]
        [TestCase("proba", "127.0.0.1", "3306", "sharegoat98", "")]
        public void createDataBaseTestEmpty(string imeBaze, string server, string port, string username, string password)
        {


            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.createDataBase(imeBaze, server, port, username, password);
            });

        }

        [Test]
        [TestCase("Select * from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "")]
        [TestCase("cao cao cao", "proba", "127.0.0.1", "3306", "sharegoat98", "")]

        public void updateDataBaseBeginError(string query, string dataBase, string server, string port, string username, string passowrd)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.updateInDataBase(query, dataBase, server, port, username, passowrd);
            });

        }

        [Test]
        [TestCase("Update tabelaProba set name='Roger' where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        
        public void updateDataBaseTestOk1(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Roger Djokovic all";
            //string output = "success";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.updateInDataBase(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output,rez);
        }

        [Test]
        [TestCase("Update tabelaProba set surname='Federer' where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void updateDataBaseTestOk11(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Novak Federer all";
            

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.updateInDataBase(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output,rez);
        }


        [Test]
        [TestCase("Update tabelaProba set name='Roger',surname='Federer' where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void updateDataBaseTestOk12(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Roger Federer all";
            

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.updateInDataBase(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output,rez);
        }




        [Test]
        [TestCase("Update tabela1 set name='Djovak' where id=2", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabela1 set name='Djovak' where name='Djovak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabela1 set name='Djovak' where surname='Djovak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        public void updateDataBaseTestNotOk(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "error";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.updateInDataBase(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase(null, "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", null, "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", null, "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "127.0.0.1", null, "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "127.0.0.1", "3306", null, "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", null)]
        public void updateDataBaseTestNull(string query, string dataBase, string server, string port, string username, string passowrd)
        {


            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                string rez = r.updateInDataBase(query, dataBase, server, port, username, passowrd);
            });

        }


        [Test]
        [TestCase("", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "127.0.0.1", "", "sharegoat98", "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "127.0.0.1", "3306", "", "Aleksasare5")]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "")]
        public void updateDataBaseTestEmpty(string query, string dataBase, string server, string port, string username, string passowrd)
        {


            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.updateInDataBase(query, dataBase, server, port, username, passowrd);
            });

        }

        [Test]
        [TestCase("Select id,name,surname from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("cao cao cao", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        public void updateDataBaseTestBeginError(string query, string dataBase, string server, string port, string username, string passowrd)
        {


            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.updateInDataBase(query, dataBase, server, port, username, passowrd);
            });

        }


        [Test]
        [TestCase("Select * from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select * from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,name,surname from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select * from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select * from tabelaProba where id='1'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,name,surname from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select * from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,name,surname from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,name,surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk1(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Novak Djokovic all";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output,rez);
        }


        [Test]
        [TestCase("Select id,name from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,name from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,name from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,name from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk2(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Novak in";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }


        [Test]
        [TestCase("Select id,name from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,name from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk22(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Novak 2 Srdjan in";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('2','Srdjan','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output, rez);
        }

        [Test]
        [TestCase("Select name,id from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name,id from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk33(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Novak 1 Srdjan 2 ni";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('2','Srdjan','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output, rez);
        }

        [Test]
        [TestCase("Select name,surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name,surname from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk44(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Novak Djokovic Srdjan Djokovic ns";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('2','Srdjan','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output, rez);
        }

        [Test]
        [TestCase("Select surname,name from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname,name from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk55(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Djokovic Novak Djokovic Srdjan sn";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('2','Srdjan','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output, rez);
        }

        [Test]
        [TestCase("Select surname,id from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname,id from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk66(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Djokovic 1 Djokovic 2 si";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('2','Srdjan','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output, rez);
        }

        [Test]
        [TestCase("Select id,surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,surname from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk77(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Djokovic 2 Djokovic is";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('2','Srdjan','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output, rez);
        }


        [Test]
        [TestCase("Select id,surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,surname from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,surname from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id,surname from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk3(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Djokovic is";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase("Select name,surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name,surname from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name,surname from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name,surname from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk4(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Novak Djokovic ns";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase("Select name,id from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name,id from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name,id from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name,id from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk5(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Novak 1 ni";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase("Select surname,id from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname,id from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname,id from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname,id from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk6(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Djokovic 1 si";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase("Select surname,name from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname,name from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname,name from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname,name from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk7(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Djokovic Novak sn";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);


        }

        [Test]
        [TestCase("Select id from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select id from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk8(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 i";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }


        [Test]
        [TestCase("Select name from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select name from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk9(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Novak n";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }




        [Test]
        [TestCase("Select surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba where name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba where surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseTestOk10(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "Djokovic s";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(rez, output);
        }

        [Test]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("cao cao cao", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void getFromDataBaseBeginError(string query, string dataBase, string server, string port, string username, string passowrd)
        {

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            });

        }

        [Test]
        [TestCase(null, "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", null, "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", "proba", null, "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", "proba", "127.0.0.1", null, "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", "proba", "127.0.0.1", "3306", null, "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", null)]
        
        public void getFromDataBaseTestNull(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            });
        }


        [Test]
        [TestCase("", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", "", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", "proba", "", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", "proba", "127.0.0.1", "", "sharegoat98", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", "proba", "127.0.0.1", "3306", "", "Aleksasare5")]
        [TestCase("Select surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "")]

        public void getFromDataBaseTestEmpty(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.connectToDataBaseGet(query, dataBase, server, port, username, passowrd);
            });
        }

        [Test]
        [TestCase(null, "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", null, "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", "proba", null, "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", "proba", "127.0.0.1", null, "sharegoat98", "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", "proba", "127.0.0.1", "3306", null, "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", null)]

        public void deleteInTestNull(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                string rez = r.deleteInDataBase(query, dataBase, server, port, username, passowrd);
            });
        }


        [Test]
        [TestCase("", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", "", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", "proba", "", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", "proba", "127.0.0.1", "", "sharegoat98", "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", "proba", "127.0.0.1", "3306", "", "Aleksasare5")]
        [TestCase("Delete surname from tabelaProba", "proba", "127.0.0.1", "3306", "sharegoat98", "")]

        public void deleteInDataBaseTestEmpty(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.deleteInDataBase(query, dataBase, server, port, username, passowrd);
            });
        }


        [Test]
        [TestCase("Update tabelaProba set name='Djovak' where id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("Insert into tabelaProba (id,name,surname) values ('5','Viktor','Troicki')", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("cao cao cao", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void deleteInDataBaseTestBeginError(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string rez = r.deleteInDataBase(query, dataBase, server, port, username, passowrd);
            });
        }


        [Test]
        [TestCase("DELETE FROM tabelaProba WHERE name='Novak' AND id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE surname='Djokovic' AND id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE id=1", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE id=1 and name='Novak' and surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        
        public void deleteInDataBaseTestOk1(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Novak Djokovic all";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.deleteInDataBase(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output,rez);
        }

        [Test]
        [TestCase("DELETE FROM tabelaProba WHERE name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE name='Novak' and surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void deleteInDataBaseTestOk2(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "1 Novak Djokovic all";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.deleteInDataBase(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output, rez);
        }

        [Test]
        
        [TestCase("DELETE FROM tabelaProba WHERE surname='Federer'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE name='Roger' and surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE id=5", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE id=5 and name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE id=5 and surname='Djokovic'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE FROM tabelaProba WHERE id=5 and surname='Djokovic' and name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]
        [TestCase("DELETE froma tabelaProba where id=5 and surname='Djokovic' and name='Novak'", "proba", "127.0.0.1", "3306", "sharegoat98", "Aleksasare5")]

        public void deleteInDataBaseTestOk3(string query, string dataBase, string server, string port, string username, string passowrd)
        {
            string output = "error";

            var pom = new Mock<IRepository>();
            Repository r = new Repository(pom.Object);
            r.createDataBase(dataBase, server, port, username, passowrd);
            r.createTable("tabelaProba", dataBase, server, port, username, passowrd);
            r.insertInDataBase("Insert into tabelaProba (id,name,surname) values ('1','Novak','Djokovic')", dataBase, server, port, username, passowrd);
            string rez = r.deleteInDataBase(query, dataBase, server, port, username, passowrd);
            string connString = "Server=" + server + ";port=" + port + ";Uid=" + username + ";Pwd=" + passowrd;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "DROP DATABASE " + dataBase;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            Assert.AreEqual(output, rez);
        }
    }


   
}
