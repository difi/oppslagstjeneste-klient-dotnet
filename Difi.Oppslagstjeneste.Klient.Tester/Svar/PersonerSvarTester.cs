using System.Linq;
using System.Text;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.DTO;
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
            var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Eksempler.Respons");
            var mottattPersonRespons = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "HentPersonerRespons.xml"));
            var xmlDocument = XmlUtility.TilXmlDokument(mottattPersonRespons);
            var responseDokumenter = new ResponseDokument(xmlDocument);
            _personerSvar = DtoKonverterer.TilDomeneObjekt(responseDokumenter.ToDtoObject<HentPersonerRespons>());
        }

        [TestMethod]
        public void HentEnPersonSuksess()
        {
            Assert.AreEqual(1, _personerSvar.Personer.Count());
        }
    }
}