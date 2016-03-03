using System.Linq;
using System.Text;
using System.Xml;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter;
using Difi.Oppslagstjeneste.Klient.Svar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Svar
{
    [TestClass]
    public class PersonerSvarTester
    {
        private static PersonerSvar _personerSvar;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            try
            {
                var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Testdata.Ressurser");
                var mottattPersonResponse =
                    Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "HentPersonResponseDecryptert.xml"));
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(mottattPersonResponse);

                _personerSvar = new PersonerSvar(xmlDocument);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HentEnPersonSuksess()
        {
            Assert.AreEqual(1, _personerSvar.Personer.Count());
        }

    }
}