using System.Linq;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Svar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Svar
{
    [TestClass]
    public class PersonerSvarTester
    {
        private static PersonerSvar _personerSvar;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(Envelope());

                _personerSvar = new PersonerSvar(xmlDocument);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HentEnPersonSuksess()
        {
             Assert.AreEqual(1,_personerSvar.Personer.Count());
        }

        private static string Envelope()
        {
            return "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                        "<SOAP-ENV:Header>" +
                            "<wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\" SOAP-ENV:mustUnderstand=\"1\"><xenc:EncryptedKey xmlns:xenc=\"http://www.w3.org/2001/04/xmlenc#\" Id=\"EK-A8B662655B5F7C9FF614302883668321002320\"><xenc:EncryptionMethod Algorithm=\"http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p\" /><ds:KeyInfo xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\"><wsse:SecurityTokenReference><ds:X509Data><ds:X509IssuerSerial><ds:X509IssuerName>CN=Buypass Class 3 Test4 CA 3,O=Buypass AS-983163327,C=NO</ds:X509IssuerName><ds:X509SerialNumber>118253491097063369020649</ds:X509SerialNumber></ds:X509IssuerSerial></ds:X509Data></wsse:SecurityTokenReference></ds:KeyInfo><xenc:CipherData><xenc:CipherValue>XKRQ/2ya161MOr0DafisK1c6cKO+S/dzMCYRkwruZuYlWnOoytDTdJfLP9EcaeRSj7sMmWTR6OfbKqm7g3wriOUIALhPgGpRDzuh24Te4VOAawK1AnxXYcnwv5IZ4nSdKBQYHKsrjhiAlDVF1Xh1TkWufTg+WXzfp25jgMBdXfOwwbkF+RhSg7GAuZ9KnNtyv3MesjOBIYaPPdNNIW5KZt4zqGTuuaJ60a643I+XhN964rESsaTf/3+6AKi6jWz3b6takjG1Kh9wXJIlbRqK5p0781+mkA5hdjjgGIcC564+JmGSR5b5OrMeP9rgAZfNvyYUpt4+8RncVfX1WUhV6Q==</xenc:CipherValue></xenc:CipherData><xenc:ReferenceList><xenc:DataReference URI=\"#ED-A8B662655B5F7C9FF614302883668321002321\" /></xenc:ReferenceList></xenc:EncryptedKey><wsu:Timestamp wsu:Id=\"TS-A8B662655B5F7C9FF614302883668301002319\"><wsu:Created>2015-04-29T06:19:26.830Z</wsu:Created><wsu:Expires>2015-04-29T06:20:26.830Z</wsu:Expires></wsu:Timestamp><ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" Id=\"SIG-A8B662655B5F7C9FF614302883668181002318\"><ds:SignedInfo><ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /><ds:SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" /><ds:Reference URI=\"#id-A8B662655B5F7C9FF614302883668111002317\"><ds:Transforms><ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /></ds:Transforms><ds:DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><ds:DigestValue>Ei4FKW52pIjN9e9ZtrwY8EJBVvM=</ds:DigestValue></ds:Reference><ds:Reference URI=\"#SC-A8B662655B5F7C9FF614302883668101002313\"><ds:Transforms><ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /></ds:Transforms><ds:DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><ds:DigestValue>zmip5+6MW2xPFzD1j3vmygDR284=</ds:DigestValue></ds:Reference></ds:SignedInfo><ds:SignatureValue>o+1JaiOEVS8u410hFtpICUdlS6Sq6zb9cskKvT4EMW5Dr/t3OaS9O1CH80yoXx3anoZgje7hFtvktn29BcEgsZCqH2q4plHLLanLneHvHLUN5sjBphuCMACOG5hdv++XnN7+iQux6YFHMENd59PvUcbKHgNcGMsInsYFjW/Vr3JeQtjFDVfkFFj2b1vR5lxuvOk5A93wPcJ/qP78nQUNqM2BLxJlnJVv6XkNZFpgKa2ys27yjiUsk+YOPFn8oUE9oujnQ+uLfU29R+AyqQ5v8P7ZFaLRPrEJA3vZR4XQOAZTjd4xKTClIfwF2/JfZYbyg/ODHIIp9CumqnaBrTHWkg==</ds:SignatureValue><ds:KeyInfo Id=\"KI-A8B662655B5F7C9FF614302883668101002315\"><wsse:SecurityTokenReference wsu:Id=\"STR-A8B662655B5F7C9FF614302883668101002316\"><ds:X509Data><ds:X509IssuerSerial><ds:X509IssuerName>CN=Buypass Class 3 CA 3,O=Buypass AS-983163327,C=NO</ds:X509IssuerName><ds:X509SerialNumber>234761966151431973311547</ds:X509SerialNumber></ds:X509IssuerSerial></ds:X509Data></wsse:SecurityTokenReference></ds:KeyInfo></ds:Signature><wsse11:SignatureConfirmation xmlns:wsse11=\"http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd\" Value=\"AmfTg0n/82qS7ffkv3n850vvZvv3NCruR8g4lLrIIb+bc9Maz+kWlSZyu1j3SSIFiZgYqCAA/kH5lMRgAkCfRrgKwATeLtyQM0peMzKanGLzHPKfpZcrpBnABQWOGbl9H/cz82QnEjLD3j8yRim7sT3InUUMk0xCi57hX6pFC4KcS9Fn0yxtC1+VPT9Ha8AIOqACe6U53xulR1RVp9MQ8AQ+nyu03PWMWemF0TsaDve5azatkWm5iXzkaPgKo4WWLGwZpfqamrD3GoiwRxwNF/vX5RqNjwl6JjLwnJ7tv6+9jaPoq/pTo510B3cDdfvdY5/Pl6twcWXmCRKy4wC/cw==\" wsu:Id=\"SC-A8B662655B5F7C9FF614302883668101002313\" /></wsse:Security>" +
                        "</SOAP-ENV:Header>" +
                        "<SOAP-ENV:Body xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\" wsu:Id=\"id-A8B662655B5F7C9FF614302883668111002317\">" +
                            "<ns3:HentPersonerRespons xmlns:ns2=\"http://begrep.difi.no\" xmlns:ns3=\"http://kontaktinfo.difi.no/xsd/oppslagstjeneste/14-05\">" +
                                "<ns2:Person><ns2:personidentifikator>08077000292</ns2:personidentifikator>" +
                                    "<ns2:reservasjon>NEI</ns2:reservasjon>" +
                                    "<ns2:status>AKTIV</ns2:status>" +
                                    "<ns2:Kontaktinformasjon>" +
                                        "<ns2:Mobiltelefonnummer sistOppdatert=\"2015-04-14T14:35:19.000+02:00\" sistVerifisert=\"2015-04-14T14:35:19.000+02:00\">" +
                                            "+4740485641" +
                                        "</ns2:Mobiltelefonnummer>" +
                                        "<ns2:Epostadresse sistOppdatert=\"2015-04-14T14:35:30.000+02:00\" sistVerifisert=\"2015-04-14T14:35:30.000+02:00\">" +
                                            "extfjo@vegvesen.no" +
                                        "</ns2:Epostadresse>" +
                                    "</ns2:Kontaktinformasjon>" +
                                    "<ns2:SikkerDigitalPostAdresse>" +
                                        "<ns2:postkasseadresse>id.porten.testuser#2346</ns2:postkasseadresse>" +
                                        "<ns2:postkasseleverandoerAdresse>984661185</ns2:postkasseleverandoerAdresse>" +
                                    "</ns2:SikkerDigitalPostAdresse>" +
                                    "<ns2:X509Sertifikat>MIIE7jCCA9agAwIBAgIKGBZrmEgzTHzeJjANBgkqhkiG9w0BAQsFADBRMQswCQYDVQQGEwJOTzEdMBsGA1UECgwUQnV5cGFzcyBBUy05ODMxNjMzMjcxIzAhBgNVBAMMGkJ1eXBhc3MgQ2xhc3MgMyBUZXN0NCBDQSAzMB4XDTE0MDQyNDEyMzA1MVoXDTE3MDQyNDIxNTkwMFowVTELMAkGA1UEBhMCTk8xGDAWBgNVBAoMD1BPU1RFTiBOT1JHRSBBUzEYMBYGA1UEAwwPUE9TVEVOIE5PUkdFIEFTMRIwEAYDVQQFEwk5ODQ2NjExODUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCLCxU4oBhtGmJxXZWbdWdzO2uA3eRNW/kPdddL1HYl1iXLV/g+H2Q0ELadWLggkS+1kOd8/jKxEN++biMmmDqqCWbzNdmEd1j4lctSlH6M7tt0ywmXIYdZMz5kxcLAMNXsaqnPdikI9uPJZQEL3Kc8hXhXISvpzP7gYOvKHg41uCxu1xCZQOM6pTlNbxemBYqvES4fRh2xvB9aMjwkB4Nz8jrIsyoPI89i05OmGMkI5BPZt8NTa40Yf3yU+SQECW0GWalB5cxaTMeB01tqslUzBJPV3cQx+AhtQG4hkOhQnAMDJramSPVtwbEnqOjQ+lyNmg5GQ4FJO02ApKJTZDTHAgMBAAGjggHCMIIBvjAJBgNVHRMEAjAAMB8GA1UdIwQYMBaAFD+u9XgLkqNwIDVfWvr3JKBSAfBBMB0GA1UdDgQWBBQ1gsJfVC7KYGiWVLP7ZwzppyVYTTAOBgNVHQ8BAf8EBAMCBLAwFgYDVR0gBA8wDTALBglghEIBGgEAAwIwgbsGA1UdHwSBszCBsDA3oDWgM4YxaHR0cDovL2NybC50ZXN0NC5idXlwYXNzLm5vL2NybC9CUENsYXNzM1Q0Q0EzLmNybDB1oHOgcYZvbGRhcDovL2xkYXAudGVzdDQuYnV5cGFzcy5uby9kYz1CdXlwYXNzLGRjPU5PLENOPUJ1eXBhc3MlMjBDbGFzcyUyMDMlMjBUZXN0NCUyMENBJTIwMz9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0MIGKBggrBgEFBQcBAQR+MHwwOwYIKwYBBQUHMAGGL2h0dHA6Ly9vY3NwLnRlc3Q0LmJ1eXBhc3Mubm8vb2NzcC9CUENsYXNzM1Q0Q0EzMD0GCCsGAQUFBzAChjFodHRwOi8vY3J0LnRlc3Q0LmJ1eXBhc3Mubm8vY3J0L0JQQ2xhc3MzVDRDQTMuY2VyMA0GCSqGSIb3DQEBCwUAA4IBAQCe67UOZ/VSwcH2ov1cOSaWslL7JNfqhyNZWGpfgX1c0Gh+KkO3eVkMSozpgX6M4eeWBWJGELMiVN1LhNaGxBU9TBMdeQ3SqK219W6DXRJ2ycBtaVwQ26V5tWKRN4UlRovYYiY+nMLx9VrLOD4uoP6fm9GE5Fj0vSMMPvOEXi0NsN+8MUm3HWoBeUCLyFpe7/EPsS/Wud5bb0as/E2zIztRodxfNsoiXNvWaP2ZiPWFunIjK1H/8EcktEW1paiPd8AZek/QQoG0MKPfPIJuqH+WJU3a8J8epMDyVfaek+4+l9XOeKwVXNSOP/JSwgpOJNzTdaDOM+uVuk75n2191Fd7</ns2:X509Sertifikat>" +
                                "</ns2:Person>" +
                            "</ns3:HentPersonerRespons>" +
                        "</SOAP-ENV:Body>" +
                   "</SOAP-ENV:Envelope>";
        }
    }
}
