using System.Linq;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Scripts.XsdToCode.Code;
using Difi.Oppslagstjeneste.Klient.Svar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests.Svar
{
    [TestClass]
    public class EndringerSvarTests
    {
        private static EndringerSvar _endringerSvar;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var xmlDokument = XmlResource.Response.GetEndringer();
            var responseDokument = new ResponseContainer(xmlDokument);
            _endringerSvar = DtoConverter.ToDomainObject(SerializeUtil.Deserialize<HentEndringerRespons>(responseDokument.BodyElement.InnerXml));
        }

        [TestMethod]
        public void HentTrePersonerEndringerSuksess()
        {
            Assert.AreEqual(3, _endringerSvar.Personer.Count());
        }

        [TestMethod]
        public void HentFraEndringsnummerSuksess()
        {
            Assert.AreEqual(600, _endringerSvar.FraEndringsNummer);
        }

        [TestMethod]
        public void HentTilEndringsnummerSuksess()
        {
            Assert.AreEqual(5791, _endringerSvar.TilEndringsNummer);
        }

        [TestMethod]
        public void HentSenesteEndringsnummerSuksess()
        {
            Assert.AreEqual(2925086, _endringerSvar.SenesteEndringsNummer);
        }
    }
}