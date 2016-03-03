using System;
using System.Linq;
using System.Text;
using System.Xml;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Svar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Svar
{
    [TestClass]
    public class EndringerSvarTester
    {
        private static EndringerSvar _endringerSvar;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Eksempler.Respons");
            var mottattEndringerRespons = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "HentEndringerRespons.xml"));
            var xmlDokument = XmlUtility.TilXmlDokument(mottattEndringerRespons);
            _endringerSvar = new EndringerSvar(xmlDokument);
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