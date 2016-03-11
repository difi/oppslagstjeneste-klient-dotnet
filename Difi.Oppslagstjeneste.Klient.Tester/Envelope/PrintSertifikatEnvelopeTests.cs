using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Envelope
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
                OppslagstjenesteId = "OppslagstjenesteId",
                TimestampId = "TimestampId"
            };

            const string sendPåVegneAv = "sendPåVegneAv";

            //Act
            var envelope = new PrintSertifikatEnvelope(innstillinger, sendPåVegneAv);

            //Assert
            Assert.AreEqual(envelope.Settings.BinarySecurityId, innstillinger.BinarySecurityId);
            Assert.AreEqual(envelope.Settings.BodyId, innstillinger.BodyId);
            Assert.AreEqual(envelope.Settings.OppslagstjenesteId, innstillinger.OppslagstjenesteId);
            Assert.AreEqual(envelope.Settings.TimestampId, innstillinger.TimestampId);
            Assert.AreEqual(envelope.SendPåVegneAv, sendPåVegneAv);
            Assert.AreEqual(envelope.Instillinger, innstillinger);

            envelope.ToXml();
        }
    }
}