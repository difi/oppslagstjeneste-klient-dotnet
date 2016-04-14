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
            var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
            const string sendOnBehalfOf = "sendPåVegneAv";

            //Act
            var envelope = new PrintSertifikatEnvelope(senderUnitTestCertificate, sendOnBehalfOf);

            //Assert
            Assert.IsNotNull(envelope.Settings.BinarySecurityId);
            Assert.IsNotNull(envelope.Settings.BodyId);
            Assert.IsNotNull(envelope.Settings.TimestampId);
            Assert.AreEqual(envelope.SendPåVegneAv, sendOnBehalfOf);
            Assert.AreEqual(envelope.AvsenderSertifikat, senderUnitTestCertificate);

            Assert.IsNotNull(envelope.XmlDocument);
        }
    }
}