using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    
    public class EndringerEnvelopeTests
    {
        
        public class ConstructorMethod : EndringerEnvelopeTests
        {
            [Fact]
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
                Assert.NotNull(envelope.Settings.BinarySecurityId);
                Assert.NotNull(envelope.Settings.BodyId);
                Assert.NotNull(envelope.Settings.TimestampId);
                Assert.Equal(envelope.FraEndringsNummer, fromChangeNumber);
                Assert.Equal(envelope.Informasjonsbehov, informationNeed);
                Assert.Equal(envelope.SendOnBehalfOf, sendOnBehalfOf);
                Assert.Equal(envelope.SenderCertificate, senderUnitTestCertificate);

                Assert.NotNull(envelope.XmlDocument);
            }
        }
    }
}