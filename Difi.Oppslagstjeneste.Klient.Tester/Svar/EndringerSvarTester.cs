using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Svar
{
    [TestClass]
    public class EndringerSvarTester
    {
        private static EndringerSvar _endringerSvar;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(Envelope());

                _endringerSvar = new EndringerSvar(xmlDocument);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HentTrePersonerEndringerSuksess()
        {
            Assert.AreEqual(3, _endringerSvar.Personer.Count());
        }

        [TestMethod]
        public void HentFraEndringsnummerSuksess()
        {
            Assert.AreEqual(983831, _endringerSvar.FraEndringsNummer);
        }

        [TestMethod]
        public void HentTilEndringsnummerSuksess()
        {
            Assert.AreEqual(983867, _endringerSvar.TilEndringsNummer);
        }

        [TestMethod]
        public void HentSenesteEndringsnummerSuksess()
        {
            Assert.AreEqual(983867, _endringerSvar.SenesteEndringsNummer);
        }

        private static string Envelope()
        {
            return "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header><wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\" SOAP-ENV:mustUnderstand=\"1\"><xenc:EncryptedKey xmlns:xenc=\"http://www.w3.org/2001/04/xmlenc#\" Id=\"EK-A8B662655B5F7C9FF61430229281820848015\"><xenc:EncryptionMethod Algorithm=\"http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p\" /><ds:KeyInfo xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\"><wsse:SecurityTokenReference><ds:X509Data><ds:X509IssuerSerial><ds:X509IssuerName>CN=Buypass Class 3 Test4 CA 3,O=Buypass AS-983163327,C=NO</ds:X509IssuerName><ds:X509SerialNumber>118253491097063369020649</ds:X509SerialNumber></ds:X509IssuerSerial></ds:X509Data></wsse:SecurityTokenReference></ds:KeyInfo><xenc:CipherData><xenc:CipherValue>n0NrlvK4aMp13YPw6jf7Q/QZqgzacp86yhwLkVrwF6vVjTWtP/n5gYFBh2Qan4PNgmqgvwO1fr3ECLspU0SHnjlArXTR7oRpGV0FhwCpxazrkNnNZyI97YNG0n0LxBzpHibEzUAC7Kzxb4jRpuL5A7m2WZq1fptVkBy/NgHa5ju6TsCpkjODB/MZN1usyhHtvpki46t7c6GQr2IU6nExCqZWcQjhSHZLmOqpnBwPnY3PUrvG2s4fbd7bUVtDDmuqYclRV7QCk7AFP9Y1yZSw+YTKhTQIAUy6ZZY7TGGJ/Zrtj2E3KwdIKVkPhAYPxoUWBMiYpY48PbcsQPow5lIA3A==</xenc:CipherValue></xenc:CipherData><xenc:ReferenceList><xenc:DataReference URI=\"#ED-A8B662655B5F7C9FF61430229281820848016\" /></xenc:ReferenceList></xenc:EncryptedKey><wsu:Timestamp wsu:Id=\"TS-A8B662655B5F7C9FF61430229281819848014\"><wsu:Created>2015-04-28T13:54:41.819Z</wsu:Created><wsu:Expires>2015-04-28T13:55:41.819Z</wsu:Expires></wsu:Timestamp><ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" Id=\"SIG-A8B662655B5F7C9FF61430229281809848013\"><ds:SignedInfo><ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /><ds:SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" /><ds:Reference URI=\"#id-A8B662655B5F7C9FF61430229281805848012\"><ds:Transforms><ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /></ds:Transforms><ds:DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><ds:DigestValue>BSxvKRK3C4dWaxugMzArhIBsHac=</ds:DigestValue></ds:Reference><ds:Reference URI=\"#SC-A8B662655B5F7C9FF61430229281804848008\"><ds:Transforms><ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /></ds:Transforms><ds:DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><ds:DigestValue>AdQ48jmKR+ix2c+ZkBYmqdYl76E=</ds:DigestValue></ds:Reference></ds:SignedInfo><ds:SignatureValue>FZuIU2qIXRGbOpnVDwpjL8pUibX7oNkJQhqwbR7NCj1gCyL8AHz0rCRh36TPqA99FErfgt/BDtzaabGGP08SsSPa1g8axP5fDBtoN7EqwGVvjtah0EKGl/y5ESkVP17povFtvG7OnIvRFkTe0/MU8/1jd9hJ0OuGfOkByTYd6gk+31IOJIjKDm8jxjvbVaH0li/BilSOfs9z3fgNpXhfBZ9WjkLN5PS7CTFbVvdoOTgjg4GhnMzhQ0YQwR+rRLyjkKjwY08FhuPle09w07lAyF4UdC4RSdR424yJRoe10fQ54mOktWZ84s3fGIpiYjciiXDe7RhrSXtPswBfxZuMIg==</ds:SignatureValue><ds:KeyInfo Id=\"KI-A8B662655B5F7C9FF61430229281805848010\"><wsse:SecurityTokenReference wsu:Id=\"STR-A8B662655B5F7C9FF61430229281805848011\"><ds:X509Data><ds:X509IssuerSerial><ds:X509IssuerName>CN=Buypass Class 3 CA 3,O=Buypass AS-983163327,C=NO</ds:X509IssuerName><ds:X509SerialNumber>234761966151431973311547</ds:X509SerialNumber></ds:X509IssuerSerial></ds:X509Data></wsse:SecurityTokenReference></ds:KeyInfo></ds:Signature><wsse11:SignatureConfirmation xmlns:wsse11=\"http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd\" Value=\"Yd7OCuz47RKjspMbVHuyTpRga37Mjt6pTbvuTV987guWbcDxhQjQ+N/XYymqL/D6Y2td8yrFVxsyfAn89mrxrHF/vs9TdbojsGbhdOalOOPx9DQuFgYnvChu41AT3RLn9WQ2hSfWK/OnkO1my17U98P5bX2t1VCKhqSASx3DpK/E9rj/eH8OXNMYsf1Tx53RUcTpvjGqYbmpwWXe3I92Wf1RoQYO3LU0VgmCIu9sngu8VafB53Lh+JYwXXGXikjFxX+k6T/xhAtgv59cAN9rxTCzGjbK8ncjOD0KqjW5Pk9MaN6hPBL8+S/5IrWlU8EpHL0bPDngn2sTKMXjEB1CRg==\" wsu:Id=\"SC-A8B662655B5F7C9FF61430229281804848008\" /></wsse:Security></SOAP-ENV:Header><SOAP-ENV:Body xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\" wsu:Id=\"id-A8B662655B5F7C9FF61430229281805848012\"><ns3:HentEndringerRespons xmlns:ns2=\"http://begrep.difi.no\" xmlns:ns3=\"http://kontaktinfo.difi.no/xsd/oppslagstjeneste/14-05\" fraEndringsNummer=\"983831\" senesteEndringsNummer=\"983867\" tilEndringsNummer=\"983867\"><ns2:Person><ns2:personidentifikator>01015200193</ns2:personidentifikator><ns2:reservasjon>NEI</ns2:reservasjon><ns2:status>AKTIV</ns2:status><ns2:Kontaktinformasjon><ns2:Epostadresse sistVerifisert=\"2015-04-28T15:46:26.000+02:00\">01015200193-test@minid.norge.no</ns2:Epostadresse></ns2:Kontaktinformasjon></ns2:Person><ns2:Person><ns2:personidentifikator>04036126065</ns2:personidentifikator><ns2:reservasjon>NEI</ns2:reservasjon><ns2:status>AKTIV</ns2:status><ns2:Kontaktinformasjon><ns2:Epostadresse sistOppdatert=\"2015-04-28T15:52:00.000+02:00\" sistVerifisert=\"2015-04-28T15:52:00.000+02:00\">difi2@hostingtjenester.no</ns2:Epostadresse></ns2:Kontaktinformasjon></ns2:Person><ns2:Person><ns2:personidentifikator>01015700269</ns2:personidentifikator><ns2:reservasjon>NEI</ns2:reservasjon><ns2:status>AKTIV</ns2:status><ns2:Kontaktinformasjon><ns2:Epostadresse sistOppdatert=\"2015-04-28T15:54:29.000+02:00\" sistVerifisert=\"2015-04-28T15:54:29.000+02:00\">difi2@hostingtjenester.no</ns2:Epostadresse></ns2:Kontaktinformasjon></ns2:Person></ns3:HentEndringerRespons></SOAP-ENV:Body></SOAP-ENV:Envelope>";
        }
    }
}
