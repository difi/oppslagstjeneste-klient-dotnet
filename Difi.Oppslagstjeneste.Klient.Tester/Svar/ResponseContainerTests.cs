using System.IO;
using System.Text;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Examples;
using Difi.Oppslagstjeneste.Klient.Tester.Utilities.CompareObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Svar
{
    [TestClass()]
    public class ResponseContainerTests
    {
        [TestClass]
        public class ResponseContainerMethod : ResponseContainerTests
        {
            [TestMethod]
            public void ResponseDokumentMedDekryptertResponse()
            {
                //Arrange
                var xmlResponse = TestResourceUtility.Response.PersonResponse.AsXmlDocument();
                var bodyElement = xmlResponse.SelectSingleNode("/env:Envelope/env:Body", NamespaceManager.InitalizeNamespaceManager(xmlResponse));
                var expectedDeserializedResponse = SerializeUtil.Deserialize<HentPersonerRespons>(bodyElement.InnerXml);
                

                //Act
                var candidate = new ResponseContainer(GenerateStreamFromString(xmlResponse.OuterXml));

                //Assert
                var cmp = new Comparator();
                cmp.AreEqual(expectedDeserializedResponse, SerializeUtil.Deserialize<HentPersonerRespons>(candidate.BodyElement.InnerXml));
                Assert.AreEqual(xmlResponse.OuterXml,candidate.Envelope.OuterXml);
                Assert.IsNotNull(candidate.HeaderBinarySecurityToken);
                Assert.IsNotNull(candidate.BodyElement);
                Assert.IsNotNull(candidate.Cipher);
                Assert.IsNotNull(candidate.HeaderSecurityElement);
                Assert.IsNotNull(candidate.HeaderSignature);
                Assert.IsNotNull(candidate.HeaderSignatureElement);
                Assert.IsNotNull(candidate.TimestampElement);

                Assert.IsNull(candidate.EncryptedBody);
            }

            [TestMethod]
            public void ResponseDokumentMedKryptertResponse()
            {
                //Arrange
                var xmlResponse = TestResourceUtility.Response.PersonResponseEncrypted.AsXmlDocument();
                
                
                //Act
                var candidate = new ResponseContainer(GenerateStreamFromString(xmlResponse.OuterXml));

                //Assert
                Assert.AreEqual(xmlResponse.OuterXml, candidate.Envelope.OuterXml);
                Assert.IsNotNull(candidate.HeaderBinarySecurityToken);
                Assert.IsNotNull(candidate.BodyElement);
                Assert.IsNotNull(candidate.Cipher);
                Assert.IsNotNull(candidate.EncryptedBody);
                Assert.IsNotNull(candidate.HeaderSecurityElement);
                Assert.IsNotNull(candidate.HeaderSignature);
                Assert.IsNotNull(candidate.HeaderSignatureElement);
                Assert.IsNotNull(candidate.TimestampElement);

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