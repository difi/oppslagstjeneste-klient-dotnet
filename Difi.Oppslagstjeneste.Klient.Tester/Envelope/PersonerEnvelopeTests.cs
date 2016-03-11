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
                var innstillinger = new OppslagstjenesteInstillinger
                {
                    Avsendersertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat(),
                    BinarySecurityId = "BinarySecurityId",
                    BodyId = "BodyId",
                    TimestampId = "TimestampId"
                };

                const string sendPåVegneAv = "sendPåVegneAv";
                var personer = new[] {"12312312312", "32132132132"};
                const Informasjonsbehov informasjonsBehov = Informasjonsbehov.Person | Informasjonsbehov.Kontaktinfo;

                //Act
                var envelope = new PersonerEnvelope(innstillinger, sendPåVegneAv, personer, informasjonsBehov);

                //Assert
                Assert.AreEqual(envelope.Settings.BinarySecurityId, innstillinger.BinarySecurityId);
                Assert.AreEqual(envelope.Settings.BodyId, innstillinger.BodyId);
                Assert.AreEqual(envelope.Settings.TimestampId, innstillinger.TimestampId);
                Assert.AreEqual(envelope.Personidentifikator, personer);
                Assert.AreEqual(envelope.Informasjonsbehov, informasjonsBehov);
                Assert.AreEqual(envelope.SendPåVegneAv, sendPåVegneAv);
                Assert.AreEqual(envelope.Instillinger, innstillinger);

                envelope.ToXml();
            }
        }
    }
}