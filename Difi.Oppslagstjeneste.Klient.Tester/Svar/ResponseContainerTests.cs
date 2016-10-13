using System.IO;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Scripts.XsdToCode.Code;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities.CompareObjects;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Svar
{
    
    public class ResponseContainerTests
    {
        
        public class ResponseContainerConstructor : ResponseContainerTests
        {
            [Fact]
            public void ResponseContainerWithDecryptedResponse()
            {
                //Arrange
                var xmlResponse = XmlResource.Response.GetPerson();
                var bodyElement = xmlResponse.SelectSingleNode("/env:Envelope/env:Body", NamespaceManager.InitalizeNamespaceManager(xmlResponse));
                var expectedDeserializedResponse = SerializeUtil.Deserialize<HentPersonerRespons>(bodyElement.InnerXml);
                //Act
                var candidate = new ResponseContainer(GenerateStreamFromString(xmlResponse.OuterXml));

                //Assert
                var cmp = new Comparator();
                cmp.AreEqual(expectedDeserializedResponse, SerializeUtil.Deserialize<HentPersonerRespons>(candidate.BodyElement.InnerXml));
                Assert.NotNull(candidate.HeaderBinarySecurityToken);
                Assert.NotNull(candidate.BodyElement);
                Assert.NotNull(candidate.Cipher);
                Assert.NotNull(candidate.HeaderSecurityElement);
                Assert.NotNull(candidate.HeaderSignature);
                Assert.NotNull(candidate.HeaderSignatureElement);
                Assert.NotNull(candidate.TimestampElement);

                Assert.Null(candidate.EncryptedBody);
            }

            [Fact]
            public void ResponseContainerWithEncryptedResponse()
            {
                //Arrange
                var xmlResponse = XmlResource.Response.GetPersonResponseEncrypted();

                //Act
                var candidate = new ResponseContainer(GenerateStreamFromString(xmlResponse.OuterXml));

                //Assert
                Assert.Equal(xmlResponse.OuterXml, candidate.Envelope.OuterXml);
                Assert.NotNull(candidate.HeaderBinarySecurityToken);
                Assert.NotNull(candidate.BodyElement);
                Assert.NotNull(candidate.Cipher);
                Assert.NotNull(candidate.EncryptedBody);
                Assert.NotNull(candidate.HeaderSecurityElement);
                Assert.NotNull(candidate.HeaderSignature);
                Assert.NotNull(candidate.HeaderSignatureElement);
                Assert.NotNull(candidate.TimestampElement);
            }

            public Stream GenerateStreamFromString(string s)
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(s);
                writer.Flush();
                stream.Position = 0;
                return stream;
            }
        }
    }
}