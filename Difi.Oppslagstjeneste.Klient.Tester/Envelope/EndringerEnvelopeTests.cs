using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    [TestClass]
    public class EndringerEnvelopeTests
    {
        [TestClass]
        public class KonstruktørMethod : EndringerEnvelopeTests
        {
            [TestMethod]
            public void EnkelKonstruktør()
            {
                //Arrange
                var avsenderSertifikat = DomainUtility.GetSenderUnitTestCertificate();

                const string sendPåVegneAv = "sendPåVegneAv";
                const int fraEndringer = 0;
                var informasjonsBehov = new[] {Informasjonsbehov.Person, Informasjonsbehov.Kontaktinfo};

                //Act
                var envelope = new EndringerEnvelope(avsenderSertifikat, sendPåVegneAv, fraEndringer, informasjonsBehov);

                //Assert
                Assert.IsNotNull(envelope.Settings.BinarySecurityId);
                Assert.IsNotNull(envelope.Settings.BodyId);
                Assert.IsNotNull(envelope.Settings.TimestampId);
                Assert.AreEqual(envelope.FraEndringsNummer, fraEndringer);
                Assert.AreEqual(envelope.Informasjonsbehov, informasjonsBehov);
                Assert.AreEqual(envelope.SendPåVegneAv, sendPåVegneAv);
                Assert.AreEqual(envelope.AvsenderSertifikat, avsenderSertifikat);

                Assert.IsNotNull(envelope.XmlDocument);
            }
        }
    }
}