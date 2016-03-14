using System.Linq;
using System.Text;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Examples;
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

            var xmlDocument = TestResourceUtility.Response.PersonResponse.AsXmlDocument();
            var responseDokumenter = new ResponseContainer(xmlDocument);
            _personerSvar = DtoConverter.ToDomainObject(SerializeUtil.Deserialize<HentPersonerRespons>(responseDokumenter.BodyElement.InnerXml));
        }

        [TestMethod]
        public void HentEnPersonSuksess()
        {
            Assert.AreEqual(1, _personerSvar.Personer.Count());
        }
    }
}