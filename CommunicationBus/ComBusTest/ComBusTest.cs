using CommunicationBus;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBusTest
{
    [TestFixture]
    public class ComBusTest
    {
        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\"\n}", "standard")]
        public void SendTestOk(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT * FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"id\"\n}", "fields")]
        public void SendTestOk1(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"name\"\n}", "fields")]
        public void SendTestOk2(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"surname\"\n}", "fields")]
        public void SendTestOk3(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"id,name\"\n}", "fields")]
        public void SendTestOk4(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,name FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"id,surname\"\n}", "fields")]
        public void SendTestOk5(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,surname FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"name,surname\"\n}", "fields")]
        public void SendTestOk6(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,surname FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"name,id\"\n}", "fields")]
        public void SendTestOk7(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,id FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"surname,id\"\n}", "fields")]
        public void SendTestOk8(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,id FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"surname,name\"\n}", "fields")]
        public void SendTestOk9(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,name FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"id,name,surname\"\n}", "fields")]
        public void SendTestOk10(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,name,surname FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"id,surname,name\"\n}", "fields")]
        public void SendTestOk11(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,surname,name FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"name,id,surname\"\n}", "fields")]
        public void SendTestOk12(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,id,surname FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"name,surname,id\"\n}", "fields")]
        public void SendTestOk13(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,surname,id FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"surname,id,name\"\n}", "fields")]
        public void SendTestOk14(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,id,name FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"fields\": \"surname,name,id\"\n}", "fields")]
        public void SendTestOk15(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,name,id FROM resource WHERE id=1";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\"\n}", "query")]
        public void SendTestQuery0(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT * FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\"\n}", "query")]
        public void SendTestQuery1(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT * FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera';surname='mitic'\"\n}", "query")]
        public void SendTestQuery2(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT * FROM resource WHERE id=1 AND name='pera' AND surname='mitic'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='petrovic';name='pera'\"\n}", "query")]
        public void SendTestQuery3(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT * FROM resource WHERE id=1 AND surname='petrovic' AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"id\"}", "full")]
        public void SendTestQueryFields0(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"name\"}", "full")]
        public void SendTestQueryFields1(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"surname\"}", "full")]
        public void SendTestQueryFields2(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"id,name\"}", "full")]
        public void SendTestQueryFields3(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,name FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"id,surname\"}", "full")]
        public void SendTestQueryFields4(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,surname FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"name,surname\"}", "full")]
        public void SendTestQueryFields5(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,surname FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"name,id\"}", "full")]
        public void SendTestQueryFields6(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,id FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"surname,id\"}", "full")]
        public void SendTestQueryFields7(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,id FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"surname,name\"}", "full")]
        public void SendTestQueryFields8(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,name FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='pera'\",\n  \"fields\": \"id,name,surname\"}", "full")]
        public void SendTestQueryFields9(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,name,surname FROM resource WHERE id=1 AND name='pera'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        //sa surname
        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"id\"}", "full")]
        public void SendTestQueryFields00(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"name\"}", "full")]
        public void SendTestQueryFields10(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"surname\"}", "full")]
        public void SendTestQueryFields11(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"id,name\"}", "full")]
        public void SendTestQueryFields12(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,name FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"id,surname\"}", "full")]
        public void SendTestQueryFields13(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,surname FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"name,surname\"}", "full")]
        public void SendTestQueryFields14(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,surname FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"name,id\"}", "full")]
        public void SendTestQueryFields15(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,id FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"surname,id\"}", "full")]
        public void SendTestQueryFields16(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,id FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"surname,name\"}", "full")]
        public void SendTestQueryFields17(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,name FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"surname='peric'\",\n  \"fields\": \"id,name,surname\"}", "full")]
        public void SendTestQueryFields18(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,name,surname FROM resource WHERE id=1 AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        //sa name i surname
        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"id\"}", "full")]
        public void SendTestQueryFields19(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"name\"}", "full")]
        public void SendTestQueryFields20(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"surname\"}", "full")]
        public void SendTestQueryFields21(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"id,name\"}", "full")]
        public void SendTestQueryFields22(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,name FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"id,surname\"}", "full")]
        public void SendTestQueryFields23(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,surname FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"name,surname\"}", "full")]
        public void SendTestQueryFields24(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,surname FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"name,id\"}", "full")]
        public void SendTestQueryFields25(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT name,id FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"surname,id\"}", "full")]
        public void SendTestQueryFields26(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,id FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"surname,name\"}", "full")]
        public void SendTestQueryFields27(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT surname,name FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }

        [Test]
        [TestCase("\n{\n  \"verb\": \"GET\",\n  \"noun\": \"/resource/1\",\n  \"query\": \"name='petar';surname='peric'\",\n  \"fields\": \"id,name,surname\"}", "full")]
        public void SendTestQueryFields28(string json, string attr)
        {
            var pom = new Mock<IComBus>();
            ComBus cb = new ComBus(pom.Object);
            string expected = "SELECT id,name,surname FROM resource WHERE id=1 AND name='petar' AND surname='peric'";
            Assert.AreEqual(expected, cb.DetermineMethod(json, attr));
        }
        
    }
}
