using System.Linq;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Svar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests.Svar
{
    [TestClass]
    public class PersonerSvarTests
    {
        private static PersonerSvar _personerSvar;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var xmlDocument = XmlResource.Response.GetPerson();
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