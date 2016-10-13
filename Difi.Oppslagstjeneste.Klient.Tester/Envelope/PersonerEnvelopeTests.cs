using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    
    public class PersonerEnvelopeTests
    {
        
        public class ConstructorMethod : PersonerEnvelopeTests
        {
            [Fact]
            public void Initializes_fields()
            {
                //Arrange
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                const string sendOnBehalfOf = "sendPåVegneAv";
                var persons = new[] {"12312312312", "32132132132"};
                var informationNeed = new[] {Informasjonsbehov.Person, Informasjonsbehov.Kontaktinfo};

                //Act
                var envelope = new PersonsEnvelope(senderUnitTestCertificate, sendOnBehalfOf, persons, informationNeed);

                //Assert
                Assert.NotNull(envelope.Settings.BinarySecurityId);
                Assert.NotNull(envelope.Settings.BodyId);
                Assert.NotNull(envelope.Settings.TimestampId);
                Assert.Equal(envelope.PersonId, persons);
                Assert.Equal(envelope.InformationNeeds, informationNeed);
                Assert.Equal(envelope.SendOnBehalfOf, sendOnBehalfOf);
                Assert.Equal(envelope.SenderCertificate, senderUnitTestCertificate);

                Assert.NotNull(envelope.XmlDocument);
            }
        }
    }
}