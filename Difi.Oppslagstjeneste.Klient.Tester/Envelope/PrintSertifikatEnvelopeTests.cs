using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    
    public class ConstructorMethod : PersonerEnvelopeTests
    {
        [Fact]
        public void Initializes_fields()
        {
            //Arrange
            var senderUnitTestCertificate = DomainUtility.GetSenderSelfSignedCertificate();
            const string sendOnBehalfOf = "sendPåVegneAv";

            //Act
            var envelope = new PrintCertificateEnvelope(senderUnitTestCertificate, sendOnBehalfOf);

            //Assert
            Assert.NotNull(envelope.Settings.BinarySecurityId);
            Assert.NotNull(envelope.Settings.BodyId);
            Assert.NotNull(envelope.Settings.TimestampId);
            Assert.Equal(envelope.SendOnBehalfOf, sendOnBehalfOf);
            Assert.Equal(envelope.SenderCertificate, senderUnitTestCertificate);

            Assert.NotNull(envelope.XmlDocument);
        }
    }
}