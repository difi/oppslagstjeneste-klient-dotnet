using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    [TestClass]
    public class PersonerEnvelopeTests
    {
        [TestClass]
        public class KonstruktørMethod : PersonerEnvelopeTests
        {
            [TestMethod]
            public void InitializesFields()
            {
                //Arrange
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                const string sendOnBehalfOf = "sendPåVegneAv";
                var persons = new[] {"12312312312", "32132132132"};
                var informationNeed = new[] {Informasjonsbehov.Person, Informasjonsbehov.Kontaktinfo};

                //Act
                var envelope = new PersonerEnvelope(senderUnitTestCertificate, sendOnBehalfOf, persons, informationNeed);

                //Assert
                Assert.IsNotNull(envelope.Settings.BinarySecurityId);
                Assert.IsNotNull(envelope.Settings.BodyId);
                Assert.IsNotNull(envelope.Settings.TimestampId);
                Assert.AreEqual(envelope.Personidentifikator, persons);
                Assert.AreEqual(envelope.Informasjonsbehov, informationNeed);
                Assert.AreEqual(envelope.SendPåVegneAv, sendOnBehalfOf);
                Assert.AreEqual(envelope.AvsenderSertifikat, senderUnitTestCertificate);

                Assert.IsNotNull(envelope.XmlDocument);
            }
        }
    }
}