using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    [TestFixture]
    public class XmlAdapterTest
    {

        [Test]
        public void ConvertToXmlTest()
        {
            string input = "\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resurs/1\"\n}";
            string output = "<request>\n  <verb>GET</verb>\n  <noun>/resurs/1</noun>\n</request>";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            string rezFje = xmla.ConvertToXml(input);
            Assert.AreEqual(output, rezFje);

        }

        [Test]
        public void ConvertToXmlTestNull()
        {
            string input = null;


            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                string result = xmla.ConvertToXml(input);
            }

            );


        }

        [Test]
        public void ConvertToXmlTestEmpty()
        {
            string input = "";


            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string result = xmla.ConvertToXml(input);
            }

            );


        }

        [Test]
        public void FromXmlToJSONTest()
        {
            string input = "<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<id>1</id>\n  <name>rafa</name>\n  <surname>nadal</surname>\n</response>";


            //string output = "{\n  \"status\": \"SUCCESS\",\n  \"statusCode\": \"2000\",\n  \"id\": \"1\",\n  \"name\": \"rafa\",\n  \"surname\": \"nadal\"\n}";

            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"id\": " + "\"1\",\n  " +
                    "\"name\": " + "\"rafa\",\n  " +
                    "\"surname\": " + "\"nadal\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        [TestCase("<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<id>1</id>\n</response>")]
        public void FromXmlToJSONTest1(string input)
        {
            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"id\": " + "\"1\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        [TestCase("<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<name>Pera</name>\n</response>")]
        public void FromXmlToJSONTest2(string input)
        {
            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"name\": " + "\"Pera\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        [TestCase("<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<surname>Petrovic</surname>\n</response>")]
        public void FromXmlToJSONTest3(string input)
        {
            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"surname\": " + "\"Petrovic\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        [TestCase("<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<id>2</id>\n  <name>Pera</name>\n</response>")]
        public void FromXmlToJSONTest4(string input)
        {
            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"id\": " + "\"2\",\n  \"name\": \"Pera\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        [TestCase("<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<id>2</id>\n  <surname>Peric</surname>\n</response>")]
        public void FromXmlToJSONTest5(string input)
        {
            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"id\": " + "\"2\",\n  \"surname\": \"Peric\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        [TestCase("<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<name>Petar</name>\n  <surname>Peric</surname>\n</response>")]
        public void FromXmlToJSONTest6(string input)
        {
            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"name\": " + "\"Petar\",\n  \"surname\": \"Peric\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        [TestCase("<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<name>Marko</name>\n  <id>7</id>\n</response>")]
        public void FromXmlToJSONTest7(string input)
        {
            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"name\": " + "\"Marko\",\n  \"id\": \"7\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        [TestCase("<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<surname>Markovic</surname>\n  <id>7</id>\n</response>")]
        public void FromXmlToJSONTest8(string input)
        {
            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"surname\": " + "\"Markovic\",\n  \"id\": \"7\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        [TestCase("<response>\n  <status>SUCCESS</status>\n  <statusCode>2000</statusCode>\n  " +
                "<surname>Markovic</surname>\n  <name>Marko</name>\n</response>")]
        public void FromXmlToJSONTest9(string input)
        {
            string output = "{\n  \"status\": " + "\"SUCCESS\",\n  " +
                    "\"statusCode\": " + "\"2000\",\n  " +
                    "\"surname\": " + "\"Markovic\",\n  \"name\": \"Marko\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);
            Assert.AreEqual(output, xmla.FromXmlToJSON(input));
        }

        [Test]
        public void FromXmlToJSONErrorTest()
        {
            string input = "<response>\n  <status>REJECTED</status>\n  <statusCode>3000</statusCode>\n  <errorMessage>ERROR</errorMessage>\n</response>";

            string output = "{\n  \"status\": " + "\"REJECTED\",\n  " +
                    "\"statusCode\": " + "\"3000\"\n  " +
                    "\"error\": " + "\"ERROR\"\n}";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            Assert.AreEqual(output, xmla.FromXmlToJSON(input));

        }

        [Test]
        public void ConvertToXmlQueryTest()
        {
            string input = "{\n  \"verb\": " + "\"Post\",\n  " +
                "\"noun\": " + "\"/korisnici/1\" \n \"query\": " + "\"name='Roger'\"}";

            string output = "<request>\n  <verb>Post</verb>\n  <noun>/korisnici/1</noun>\n  <query>name='Roger'</query>\n</request>";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            string result = xmla.ConvertToXmlQuery(input);

            Assert.AreEqual(output, result);


        }

        [Test]
        public void ConvertToXmlQueryTestNull()
        {
            string input = null;

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                string result = xmla.ConvertToXmlQuery(input);
            }
                
            );
            

        }

        [Test]
        public void ConvertToXmlQueryTestEmpty()
        {
            string input = "";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string result = xmla.ConvertToXmlQuery(input);
            }

            );


        }

        [Test]
        public void ConvertToXmlFieldsTest()
        {
            string input = "{\n  \"verb\": " + "\"Post\",\n  " +
               "\"noun\": " + "\"/korisnici/1\" \n \"fields\": " + "\"name\"}";

            string output = "<request>\n  <verb>Post</verb>\n  <noun>/korisnici/1</noun>\n  <fields>name</fields>\n</request>";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            string result = xmla.ConvertToXmlFields(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ConvertToXmlFieldsTestNull()
        {
            string input = null;

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                string result = xmla.ConvertToXmlFields(input);
            }

            );


        }

        [Test]
        public void ConvertToXmlFieldsTestEmpty()
        {
            string input = "";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string result = xmla.ConvertToXmlFields(input);
            }

            );


        }



        [Test]
        public void ConvertToXmlQueryFieldsTest()
        {
            string input = "{\n  \"verb\": " + "\"Post\",\n  " +
                "\"noun\": " + "\"/korisnici/1\" \n \"query\": " + "\"name='Roger'\" \n \"fields\": " + "\"name\"}";

            string output = "<request>\n  <verb>Post</verb>\n  <noun>/korisnici/1</noun>\n  <query>name='Roger'</query>\n  <fields>name</fields>\n</request>";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            string result = xmla.ConvertToXmlQueryFields(input);

            Assert.AreEqual(output, result);


        }

        [Test]
        public void ConvertToXmlQueryFieldsTestNull()
        {
            string input = null;

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                string result = xmla.ConvertToXmlQueryFields(input);
            }

            );


        }

        [Test]
        public void ConvertToXmlQueryFieldsTestEmpty()
        {
            string input = "";

            var pom = new Mock<IXmlAdapter>();
            XmlAdapter xmla = new XmlAdapter(pom.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                string result = xmla.ConvertToXmlQueryFields(input);
            }

            );


        }

    }
}
