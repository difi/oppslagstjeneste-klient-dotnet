using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Envelope
{
    [TestClass]
    public class PersonerEnvelopeTests
    {
        [TestClass]
        public class KonstruktørMethod : PersonerEnvelopeTests
        {
            [TestMethod]
            public void EnkelKonstruktør()
            {
                //Arrange
                var avsendersertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat();
                const string sendPåVegneAv = "sendPåVegneAv";
                var personer = new[] {"12312312312", "32132132132"};
                var informasjonsBehov = new[] { Informasjonsbehov.Person, Informasjonsbehov.Kontaktinfo }; 

                //Act
                var envelope = new PersonerEnvelope(avsendersertifikat, sendPåVegneAv, personer, informasjonsBehov);

                //Assert
                Assert.IsNotNull(envelope.Settings.BinarySecurityId);
                Assert.IsNotNull(envelope.Settings.BodyId);
                Assert.IsNotNull(envelope.Settings.TimestampId);
                Assert.AreEqual(envelope.Personidentifikator, personer);
                Assert.AreEqual(envelope.Informasjonsbehov, informasjonsBehov);
                Assert.AreEqual(envelope.SendPåVegneAv, sendPåVegneAv);
                Assert.AreEqual(envelope.AvsenderSertifikat, avsendersertifikat);

                Assert.IsNotNull(envelope.XmlDocument);
            }
        }
    }
}