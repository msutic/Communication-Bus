using CommunicationBus;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlToDBAdapterTest
{
    [TestFixture]
    public class XmlToDBTest
    {
        [Test]
        [TestCase("<request>\n  <verb>POST</verb>\n <noun>/resource/1</noun>\n  <query>name='pera';surname='peric'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestOk1(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "INSERT INTO resource (id,name,surname) VALUES ('1','pera','peric')";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }
        

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n <noun>/resource/1</noun>\n  <query>name='pera';surname='petrovic'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestOk2(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT * FROM resource WHERE id=1 AND name='pera' AND surname='petrovic'";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }
        

        [Test]
        [TestCase("<request>\n  <verb>PATCH</verb>\n  <noun>/resource/1</noun>\n  <query>name='pera';surname='peric'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestOk4(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "UPDATE resource SET name='pera',surname='peric' WHERE id=1";
            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>PATCH</verb>\n  <noun>/resource/1</noun>\n  <query>surname='peric';name='pera'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestOk10(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "UPDATE resource SET name='pera',surname='peric' WHERE id=1";
            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>PATCH</verb>\n  <noun>/resource/1</noun>\n  <query>name='pera'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestOk5(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "UPDATE resource SET name='pera' WHERE id=1";
            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>PATCH</verb>\n  <noun>/resource/1</noun>\n  <query>surname='petrovic'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestOk9(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "UPDATE resource SET surname='petrovic' WHERE id=1";
            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>DELETE</verb>\n <noun>/resource/1</noun>\n</request>")]
        public void DetermineAndConvertToSQLTestDelete0(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "DELETE FROM resource WHERE id=1";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>DELETE</verb>\n <noun>/resource/1</noun>\n  <query>name='pera'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestDelete1(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "DELETE FROM resource WHERE id=1 AND name='pera'";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>DELETE</verb>\n <noun>/resource/1</noun>\n  <query>surname='peric'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestDelete2(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "DELETE FROM resource WHERE id=1 AND surname='peric'";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>DELETE</verb>\n <noun>/resource/1</noun>\n  <query>name='pera';surname='peric'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestDelete3(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "DELETE FROM resource WHERE id=1 AND name='pera' AND surname='peric'";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>DELETE</verb>\n <noun>/resource</noun>\n  <query>name='pera'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestDelete4(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "DELETE FROM resource WHERE name='pera'";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>DELETE</verb>\n <noun>/resource</noun>\n  <query>name='pera';surname='peric'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestDelete5(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "DELETE FROM resource WHERE name='pera' AND surname='peric'";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>DELETE</verb>\n <noun>/resource</noun>\n  <query>surname='peric'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestDelete6(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "DELETE FROM resource WHERE surname='peric'";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>DELETE</verb>\n <noun>/resource</noun>\n  <query>surname='peric';name='pera'</query>\n</request>")]
        public void DetermineAndConvertToSQLTestDelete7(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "DELETE FROM resource WHERE name='pera' AND surname='peric'";

            string actual = toDB.DetermineAndConvertToSQL(xmlReq);
            Assert.AreEqual(expected, actual);
        }


        [Test]
        [TestCase("<request>\n  <verb>zz</verb>\n <noun>/resource/1</noun>\n  <query>name='pera';surname='peric'</query>\n</request>")]
        [TestCase("<request>\n  <verb>zz</verb>\n <noun></noun>\n  <query>name='pera';type=1</query>\n</request>")]
        [TestCase("<request>\n  <verb></verb>\n <noun></noun>\n  <query>name='pera';surname='peric'</query>\n</request>")]
        [TestCase("")]
        public void DetermineAndConvertToSQLFail(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                toDB.DetermineAndConvertToSQL(xmlReq);
            });
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n <noun>/resource/1</noun>\n  <query>name='pera';surname='peric'</query><a></a>\n</request>")]
        [TestCase("<request>\n  <verb>GET</verb>\n <noun>/resource/1</noun>\n  <query>name='pera';surname='peric'</qu>\n</request>")]
        public void DetermineAndConvertToSQLFail2(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                toDB.DetermineAndConvertToSQL(xmlReq);
            });
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource/1</noun>\n</request>")]
        public void XmlToDB(string xmlReq)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);
            string expected = "SELECT * FROM resource WHERE id=1";
            string actual = toDB.ConvertXmlToSQL(xmlReq);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource/1</noun>\n  <query>name='pera';surname='peric'</query>\n</request>")]
        public void XmlToDBQuery(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT * FROM resource WHERE id=1 AND name='pera' AND surname='peric'";
            string actual = toDB.ConvertToSqlQuery(xml);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource</noun>\n  <query>name='pera'</query>\n</request>")]
        public void XmlToDBQuery1(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT * FROM resource WHERE name='pera'";
            string actual = toDB.ConvertToSqlQuery(xml);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource/1</noun>\n  <query>name='pera'</query>\n</request>")]
        public void XmlToDBQuery2(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT * FROM resource WHERE id=1 AND name='pera'";
            string actual = toDB.ConvertToSqlQuery(xml);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource</noun>\n  <query>name='pera';surname='peric'</query>\n</request>")]
        public void XmlToDBQuery3(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT * FROM resource WHERE name='pera' AND surname='peric'";
            string actual = toDB.ConvertToSqlQuery(xml);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource/1</noun>\n  <fields>id;name;surname</fields>\n</request>")]
        public void XmlToDBFields(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT id,name,surname FROM resource WHERE id=1";
            string actual = toDB.ConvertToSqlFields(xml);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource/1</noun>\n  <fields>id</fields>\n</request>")]
        public void XmlToDBFields1(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT id FROM resource WHERE id=1";
            string actual = toDB.ConvertToSqlFields(xml);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource</noun>\n  <fields>name</fields>\n</request>")]
        public void XmlToDBFields2(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT name FROM resource";
            string actual = toDB.ConvertToSqlFields(xml);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource</noun>\n  <fields>name;surname</fields>\n</request>")]
        public void XmlToDBFields3(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT name,surname FROM resource";
            string actual = toDB.ConvertToSqlFields(xml);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource/1</noun>\n  <fields>name;surname</fields>\n</request>")]
        public void XmlToDBFields4(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT name,surname FROM resource WHERE id=1";
            string actual = toDB.ConvertToSqlFields(xml);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("<request>\n  <verb>GET</verb>\n  <noun>/resource/1</noun>\n  <query>name='mika';surname='mikic'</query>\n  <fields>id;name</fields>\n</request>")]
        public void XmlToDBQueryFields(string xml)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB toDB = new XmlToDB(pom.Object);

            string expected = "SELECT id,name FROM resource WHERE id=1 AND name='mika' AND surname='mikic'";
            string actual = toDB.ConvertToSqlQueryFields(xml);

            Assert.AreEqual(expected, actual);
        }

        

        [Test]
        [TestCase("REJECTED", 3000, "ERROR")]
        public void cenvertResponseToXmlOk1(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>REJECTED</status>\n  <statusCode>3000</statusCode>\n  <errorMessage>ERROR</errorMessage>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 Roger Federer 2 Rafael Nadal all")]
        public void cenvertResponseToXmlOk2(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <name>Roger</name>\n  <surname>Federer</surname>\n  <id>2</id>\n  <name>Rafael</name>\n  <surname>Nadal</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 Roger Federer 2 Rafael Nadal 3 Novak Djokovic all")]
        public void cenvertResponseToXmlOk3(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <name>Roger</name>\n  <surname>Federer</surname>\n  <id>2</id>\n  <name>Rafael</name>\n  <surname>Nadal</surname>\n  <id>3</id>\n  <name>Novak</name>\n  <surname>Djokovic</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 Roger Federer all")]
        public void cenvertResponseToXmlOk4(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <name>Roger</name>\n  <surname>Federer</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }


        [Test]
        [TestCase("SUCCESS", 2000, "1 i")]
        public void cenvertResponseToXmlOk5(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 2 i")]
        public void cenvertResponseToXmlOk6(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <id>2</id>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 2 3 i")]
        public void cenvertResponseToXmlOk7(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <id>2</id>\n  <id>3</id>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Roger n")]
        public void cenvertResponseToXmlOk8(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <name>Roger</name>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Roger Rafa n")]
        public void cenvertResponseToXmlOk9(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <name>Roger</name>\n  <name>Rafa</name>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Roger Rafa Novak n")]
        public void cenvertResponseToXmlOk10(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <name>Roger</name>\n  <name>Rafa</name>\n  <name>Novak</name>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Federer s")]
        public void cenvertResponseToXmlOk11(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <surname>Federer</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Federer Nadal s")]
        public void cenvertResponseToXmlOk12(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <surname>Federer</surname>\n  <surname>Nadal</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Federer Nadal Djokovic s")]
        public void cenvertResponseToXmlOk13(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <surname>Federer</surname>\n  <surname>Nadal</surname>\n  <surname>Djokovic</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 Roger in")]
        public void cenvertResponseToXmlOk14(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <name>Roger</name>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 Roger 2 Nadal in")]
        public void cenvertResponseToXmlOk15(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);


            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <name>Roger</name>\n  <id>2</id>\n  <name>Nadal</name>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 Roger 2 Nadal 3 Novak in")]
        public void cenvertResponseToXmlOk16(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);


            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <name>Roger</name>\n  <id>2</id>\n  <name>Nadal</name>\n  <id>3</id>\n  <name>Novak</name>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Roger 1 ni")]
        public void cenvertResponseToXmlOk17(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <name>Roger</name>\n  <id>1</id>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }


        [Test]
        [TestCase("SUCCESS", 2000, "Roger 1 Rafael 2 ni")]
        public void cenvertResponseToXmlOk18(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <name>Roger</name>\n  <id>1</id>\n  <name>Rafael</name>\n  <id>2</id>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }


        [Test]
        [TestCase("SUCCESS", 2000, "Roger 1 Rafael 2 Novak 3 ni")]
        public void cenvertResponseToXmlOk19(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <name>Roger</name>\n  <id>1</id>\n  <name>Rafael</name>\n  <id>2</id>\n  <name>Novak</name>\n  <id>3</id>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }


        [Test]
        [TestCase("SUCCESS", 2000, "1 Federer is")]
        public void cenvertResponseToXmlOk20(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <surname>Federer</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 Federer 2 Nadal is")]
        public void cenvertResponseToXmlOk21(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <surname>Federer</surname>\n  <id>2</id>\n  <surname>Nadal</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "1 Federer 2 Nadal 3 Djokovic is")]
        public void cenvertResponseToXmlOk22(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <id>1</id>\n  <surname>Federer</surname>\n  <id>2</id>\n  <surname>Nadal</surname>\n  <id>3</id>\n  <surname>Djokovic</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Federer 1 si")]
        public void cenvertResponseToXmlOk23(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <surname>Federer</surname>\n  <id>1</id>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Federer 1 Nadal 2 si")]
        public void cenvertResponseToXmlOk24(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <surname>Federer</surname>\n  <id>1</id>\n  <surname>Nadal</surname>\n  <id>2</id>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Federer 1 Nadal 2 Djokovic 3 si")]
        public void cenvertResponseToXmlOk25(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <surname>Federer</surname>\n  <id>1</id>\n  <surname>Nadal</surname>\n  <id>2</id>\n  <surname>Djokovic</surname>\n  <id>3</id>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Roger Federer ns")]
        public void cenvertResponseToXmlOk26(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <name>Roger</name>\n  <surname>Federer</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Roger Federer Rafael Nadal ns")]
        public void cenvertResponseToXmlOk27(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <name>Roger</name>\n  <surname>Federer</surname>\n  <name>Rafael</name>\n  <surname>Nadal</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Roger Federer Rafael Nadal Novak Djokovic ns")]
        public void cenvertResponseToXmlOk28(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <name>Roger</name>\n  <surname>Federer</surname>\n  <name>Rafael</name>\n  <surname>Nadal</surname>\n  <name>Novak</name>\n  <surname>Djokovic</surname>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Federer Roger sn")]
        public void cenvertResponseToXmlOk29(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <surname>Federer</surname>\n  <name>Roger</name>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase("SUCCESS", 2000, "Federer Roger Nadal Rafael sn")]
        public void cenvertResponseToXmlOk30(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <surname>Federer</surname>\n  <name>Roger</name>\n  <surname>Nadal</surname>\n  <name>Rafael</name>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }


        [Test]
        [TestCase("SUCCESS", 2000, "Federer Roger Nadal Rafael Djokovic Novak sn")]
        public void cenvertResponseToXmlOk31(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            string output = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  <surname>Federer</surname>\n  <name>Roger</name>\n  <surname>Nadal</surname>\n  <name>Rafael</name>\n  <surname>Djokovic</surname>\n  <name>Novak</name>\n</response>";

            string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);

            Assert.AreEqual(output, fResult);

        }

        [Test]
        [TestCase(null, 2000, "Federer Roger Nadal Rafael Djokovic Novak sn")]
        [TestCase("SUCCESS", 2000, null)]
        public void cenvertResponseToXmlNotOk1(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);
            });


        }

        [Test]
        [TestCase("", 2000, "Federer Roger Nadal Rafael Djokovic Novak sn")]
        [TestCase("SUCCESS", 2000, "")]
        public void cenvertResponseToXmlNotOk2(string status, int statusCode, string payload)
        {
            var pom = new Mock<IXmlToDB>();
            XmlToDB xml2DB = new XmlToDB(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string fResult = xml2DB.ConvertResponseToXml(status, statusCode, payload);
            });


        }

    }
}
