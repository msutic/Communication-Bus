
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
    public class WebClientTest
    {

        [Test]
        [TestCase("", "", "", "")]
        public void WebClientTestOk0(string a, string b, string c, string d)
        {
            WebClient wb = new WebClient();
            Assert.AreEqual(a,wb.Request);
            Assert.AreEqual(b,wb.Verb);
            Assert.AreEqual(c,wb.Noun);
            Assert.AreEqual(d,wb.JsonRequest);
        }

        [Test]
        [TestCase("GET /korisnici/1")]
        [TestCase("POST /korisnici/2")]
        [TestCase("PATCH /korisnici/2")]
        [TestCase("GET /korisnici/1 name='pera';surname='peric'")]
        [TestCase("PATCH /korisnici/1 name='pera';surname='petrovic' id;name;surname")]
        [TestCase("GET /korisnici/1 name='mika';surname='mikic' id")]
        public void WebClientTestOk(string request)
        {
            WebClient webClient = new WebClient(request);
            Assert.AreEqual(request, webClient.Request);
        }

        [Test]
        [TestCase("")]
        [TestCase(" /resurs/1")]
        [TestCase("/resurs/1")]
        [TestCase("GET")]
        [TestCase("get ")]
        [TestCase("GET //1")]
        [TestCase("GET /resurs/ ")]
        [TestCase("POST")]
        [TestCase("GET /korisnici/1 e/")]
        [TestCase("GET /korisnici/1 e")]
        [TestCase("GET / /1")]
        [TestCase("XYZ /korisnici/1 e")]
        [TestCase("XYZ /korisnici/1 =e")]
        [TestCase("XYZ /korisnici/1 name=z")]
        [TestCase("XYZ /korisnici/1 name=''")]
        [TestCase("XYZ /korisnici/1 =")]
        [TestCase("XYZ /korisnici/1 /")]
        [TestCase("GET /korisnici/1 /;/")]
        [TestCase("GET /korisnici/1 /=;/")]
        [TestCase("GET /korisnici/1 name=pera")]
        [TestCase("GET /korisnici/1 5ggx='pera'")]
        [TestCase("GET /korisnici/1 name/pera")]
        [TestCase("GET /korisnici/1 name/pera;pre='zz'")]
        [TestCase("GET /korisnici/1 n44xe='pera';pre='zz'")]
        [TestCase("GET /korisnici/1 name=pera;surname=zz")]
        [TestCase("GET /korisnici/1 name='pera';surname='zz' idname")]
        [TestCase("GET /korisnici/1 name='pera';surname=zz")]
        [TestCase("GET /korisnici/1 name='pera';645fgsf=zz")]
        [TestCase("GET /korisnici/1 name='pera';645fgsf=zz id;name esdf")]
        [TestCase("POST /korisnici name='Petar';name='Petrovic'")]
        public void ValidateRequestTestBad(string request)
        {
            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(false, wc.ValidateRequest(request));
        }

        [Test]
        [TestCase(null)]
        public void ValidateRequestTestBad2(string request)
        {
            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(false, wc.ValidateRequest(request));
        }

        [Test]
        [TestCase("GET /korisnici/1")]
        [TestCase("POST /korisnici/1 name='pera';surname='peric'")]
        [TestCase("PATCH /korisnici/1 name='mika'")]
        [TestCase("POST /korisnici/1 name='pera';surname='peric'")]
        [TestCase("GET /korisnici/1 name='pera';surname='peric' id;name")]
        [TestCase("GET /korisnici/1 name='pera';surname='peric' id;name;surname")]
        [TestCase("GET /korisnici/1 id;name;surname")]
        [TestCase("GET /korisnici/1 id;surname;name")]
        [TestCase("GET /korisnici/1 name;id;surname")]
        [TestCase("GET /korisnici/1 name;surname;id")]
        [TestCase("GET /korisnici/1 surname;name;id")]
        [TestCase("GET /korisnici/1 surname;id;name")]
        [TestCase("GET /korisnici/1 id")]
        [TestCase("GET /korisnici")]
        public void ValidateRequestTestOk(string request)
        {
            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(true, wc.ValidateRequest(request));
        }
        



        //GET METOD
        [Test]
        [TestCase("GET /korisnici/1")]
        public void ConvertToJsonTest(string request)
        {
            string output = "\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/korisnici/1\"\n}";
            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(output, wc.ConvertToJson(request));
        }

        
        [Test]
        [TestCase("GET /korisnici/1 name='pera';surname='peric'")]
        public void ConvertToJsonQueryTest(string request)
        {
            string output = "\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/korisnici/1\",\n  \"query\": \"name='pera';surname='peric'\"\n}";

            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(output, wc.ConvertToJsonQuery(request));
        }

        [Test]
        [TestCase("GET /korisnici/1 id;name;surname")]
        public void ConvertToJsonFieldsTest(string request)
        {
            string output = "\n{\n  \"verb\": " + "\"GET\",\n  " +
                "\"noun\": " + "\"/korisnici/1\",\n  \"fields\": " + "\"id;name;surname\"\n}";

            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(output, wc.ConvertToJsonFields(request));
        }

        [Test]
        [TestCase("GET /korisnici/1 name='pera';surname='petrovic' id;name;surname")]
        public void ConvertToJsonFullTest(string request)
        {
            string output = "\n{\n  \"verb\": " + "\"GET\",\n  " +
                "\"noun\": \"/korisnici/1\",\n  \"query\": \"name='pera';surname='petrovic'\",\n  \"fields\": \"id;name;surname\"\n}";

            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(output, wc.ConvertToJsonFull(request));
        }

        //POST METOD
        [Test]
        [TestCase("POST /korisnici/1")]
        public void ConvertToJsonPOSTTest(string request)
        {
            string output = "\n{\n  \"verb\": \"POST\",\n  \"noun\": \"/korisnici/1\"\n}";
            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(output, wc.ConvertToJson(request));
        }
        
        [Test]
        [TestCase("POST /korisnici/1 name='pera';surname='peric'")]
        public void ConvertToJsonQueryPOSTTest(string request)
        {
            string output = "\n{\n  \"verb\": \"POST\",\n  \"noun\": \"/korisnici/1\",\n  \"query\": \"name='pera';surname='peric'\"\n}";

            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(output, wc.ConvertToJsonQuery(request));
        }

        //PATCH METOD
        [Test]
        [TestCase("PATCH /korisnici/1")]
        public void ConvertToJsonPATCHTest(string request)
        {
            string output = "\n{\n  \"verb\": \"PATCH\",\n  \"noun\": \"/korisnici/1\"\n}";
            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(output, wc.ConvertToJson(request));
        }
        
        [Test]
        [TestCase("PATCH /korisnici/1 name='pera';surname='peric'")]
        public void ConvertToJsonQueryPATCHTest(string request)
        {
            string output = "\n{\n  \"verb\": \"PATCH\",\n  \"noun\": \"/korisnici/1\",\n  \"query\": \"name='pera';surname='peric'\"\n}";

            var pom = new Mock<IWebClient>();
            WebClient wc = new WebClient(pom.Object);
            Assert.AreEqual(output, wc.ConvertToJsonQuery(request));
        }

        [Test]
        [TestCase("BAD_FORMAT 5000")]
        public void SendResponseTest(string response)
        {
            var pom = new Mock<IWebClient>();
            WebClient wb = new WebClient(pom.Object);
            Assert.AreEqual(response, wb.SendResponse(response));
        }

        [Test]
        [TestCase("GET /korisnici/1")]
        [TestCase("GET /korisnici")]
        public void DetermineMethodTest(string input)
        {
            string output = "standard";
            var pom = new Mock<IWebClient>();
            WebClient wb = new WebClient(pom.Object);
            Assert.AreEqual(output, wb.DetermineMethod(input));
        }

        [Test]
        [TestCase("GET /korisnici/1 name='pera';surname='peric'")]
        public void DetermineMethodTest1(string input)
        {
            string output = "query";
            var pom = new Mock<IWebClient>();
            WebClient wb = new WebClient(pom.Object);
            Assert.AreEqual(output, wb.DetermineMethod(input));
        }

        [Test]
        [TestCase("GET /korisnici/1 id;name")]
        public void DetermineMethodTest2(string input)
        {
            string output = "fields";
            var pom = new Mock<IWebClient>();
            WebClient wb = new WebClient(pom.Object);
            Assert.AreEqual(output, wb.DetermineMethod(input));
        }

        [Test]
        [TestCase("GET /korisnici/1 name='pera';surname='peric' id;name;surname")]
        public void DetermineMethodTest3(string input)
        {
            string output = "full";
            var pom = new Mock<IWebClient>();
            WebClient wb = new WebClient(pom.Object);
            Assert.AreEqual(output, wb.DetermineMethod(input));
        }
    }
}
