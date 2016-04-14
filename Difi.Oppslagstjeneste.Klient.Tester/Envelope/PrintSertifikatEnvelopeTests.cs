using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    [TestClass]
    public class KonstruktørMethod : PersonerEnvelopeTests
    {
        [TestMethod]
        public void EnkelKonstruktør()
        {
            //Arrange
            var avsendersertifikat = DomainUtility.GetSenderUnitTestCertificate();
            const string sendPåVegneAv = "sendPåVegneAv";

            //Act
            var envelope = new PrintSertifikatEnvelope(avsendersertifikat, sendPåVegneAv);

            //Assert
            Assert.IsNotNull(envelope.Settings.BinarySecurityId);
            Assert.IsNotNull(envelope.Settings.BodyId);
            Assert.IsNotNull(envelope.Settings.TimestampId);
            Assert.AreEqual(envelope.SendPåVegneAv, sendPåVegneAv);
            Assert.AreEqual(envelope.AvsenderSertifikat, avsendersertifikat);

            Assert.IsNotNull(envelope.XmlDocument);
        }
    }
}