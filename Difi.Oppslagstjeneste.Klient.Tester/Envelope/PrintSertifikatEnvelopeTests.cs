using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    
    public class KonstruktørMethod : PersonerEnvelopeTests
    {
        [Fact]
        public void EnkelKonstruktør()
        {
            //Arrange
            var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
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