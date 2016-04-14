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
        public class ConstructorMethod : EndringerEnvelopeTests
        {
            [TestMethod]
            public void EnkelKonstruktør()
            {
                //Arrange
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();

                const string sendOnBehalfOf = "sendPåVegneAv";
                const int fromChangeNumber = 0;
                var informationNeed = new[] {Informasjonsbehov.Person, Informasjonsbehov.Kontaktinfo};

                //Act
                var envelope = new EndringerEnvelope(senderUnitTestCertificate, sendOnBehalfOf, fromChangeNumber, informationNeed);

                //Assert
                Assert.IsNotNull(envelope.Settings.BinarySecurityId);
                Assert.IsNotNull(envelope.Settings.BodyId);
                Assert.IsNotNull(envelope.Settings.TimestampId);
                Assert.AreEqual(envelope.FraEndringsNummer, fromChangeNumber);
                Assert.AreEqual(envelope.Informasjonsbehov, informationNeed);
                Assert.AreEqual(envelope.SendOnBehalfOf, sendOnBehalfOf);
                Assert.AreEqual(envelope.SenderCertificate, senderUnitTestCertificate);

                Assert.IsNotNull(envelope.XmlDocument);
            }
        }
    }
}