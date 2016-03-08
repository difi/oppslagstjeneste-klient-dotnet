using System.IO;
using System.Text;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tester.Utilities.CompareObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Svar
{
    [TestClass()]
    public class ResponseDokumentTester
    {
        [TestClass]
        public class ResponseDokumentMethod : ResponseDokumentTester
        {
            [TestMethod]
            public void ResponseDokumentMedDekryptertResponse()
            {
                //Arrange
                var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Eksempler.Respons");
                var respons = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "HentPersonerRespons.xml"));
                var xmlResponse = XmlUtility.TilXmlDokument(respons);
                var bodyElement = xmlResponse.SelectSingleNode("/env:Envelope/env:Body", NamespaceManager.InitalizeNamespaceManager(xmlResponse));
                var expectedDeserializedResponse = SerializeUtil.Deserialize<HentPersonerRespons>(bodyElement.InnerXml);


                //Act
                var candidate = new ResponseDokument(GenerateStreamFromString(xmlResponse.OuterXml));

                //Assert
                var cmp = new Comparator();
                cmp.AreEqual(expectedDeserializedResponse, candidate.TilDtoObjekt<HentPersonerRespons>());
                Assert.AreEqual(XmlUtility.TilXmlDokument(respons).OuterXml,candidate.Envelope.OuterXml);
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
                var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Eksempler.Respons");
                var respons = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "HentPersonerResponsKryptert.xml"));
                var xmlResponse = XmlUtility.TilXmlDokument(respons);
                

                //Act
                var candidate = new ResponseDokument(GenerateStreamFromString(xmlResponse.OuterXml));

                //Assert
                Assert.AreEqual(XmlUtility.TilXmlDokument(respons).OuterXml, candidate.Envelope.OuterXml);
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