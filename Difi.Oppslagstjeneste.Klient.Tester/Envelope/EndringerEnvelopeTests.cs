using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Envelope
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
                var innstillinger = new OppslagstjenesteInstillinger
                {
                    Avsendersertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat(),
                    BinarySecurityId = "BinarySecurityId",
                    BodyId = "BodyId",
                    OppslagstjenesteId = "OppslagstjenesteId",
                    TimestampId = "TimestampId"
                };

                const string sendPåVegneAv = "sendPåVegneAv";
                const int fraEndringer = 0;
                const Informasjonsbehov informasjonsBehov = Informasjonsbehov.Person | Informasjonsbehov.Kontaktinfo;

                //Act
                var envelope = new EndringerEnvelope(innstillinger, sendPåVegneAv, fraEndringer, informasjonsBehov);

                //Assert
                Assert.AreEqual(envelope.Settings.BinarySecurityId, innstillinger.BinarySecurityId);
                Assert.AreEqual(envelope.Settings.BodyId, innstillinger.BodyId);
                Assert.AreEqual(envelope.Settings.OppslagstjenesteId, innstillinger.OppslagstjenesteId);
                Assert.AreEqual(envelope.Settings.TimestampId, innstillinger.TimestampId);
                Assert.AreEqual(envelope.FraEndringsNummer, fraEndringer);
                Assert.AreEqual(envelope.Informasjonsbehov, informasjonsBehov);
                Assert.AreEqual(envelope.SendPåVegneAv, sendPåVegneAv);
                Assert.AreEqual(envelope.Instillinger, innstillinger);

                envelope.ToXml();
            }
        }
    }
}