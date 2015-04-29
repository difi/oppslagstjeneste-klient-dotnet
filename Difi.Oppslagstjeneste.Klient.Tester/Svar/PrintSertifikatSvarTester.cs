using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Svar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Svar
{
    [TestClass]
    public class PrintSertifikatSvarTester
    {
        private static PrintSertifikatSvar _printSertifikatSvar;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(Envelope());

                _printSertifikatSvar = new PrintSertifikatSvar(xmlDocument);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HentSertifikatSuksess()
        {
            string expectedThumb = new X509Certificate2(Convert.FromBase64String(@"MIIE7jCCA9agAwIBAgIKGBZrmEgzTHzeJjANBgkqhkiG9w0BAQsFADBRMQswCQYDVQQGEwJOTzEdMBsGA1UECgwUQnV5cGFzcyBBUy05ODMxNjMzMjcxIzAhBgNVBAMMGkJ1eXBhc3MgQ2xhc3MgMyBUZXN0NCBDQSAzMB4XDTE0MDQyNDEyMzA1MVoXDTE3MDQyNDIxNTkwMFowVTELMAkGA1UEBhMCTk8xGDAWBgNVBAoMD1BPU1RFTiBOT1JHRSBBUzEYMBYGA1UEAwwPUE9TVEVOIE5PUkdFIEFTMRIwEAYDVQQFEwk5ODQ2NjExODUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCLCxU4oBhtGmJxXZWbdWdzO2uA3eRNW/kPdddL1HYl1iXLV/g+H2Q0ELadWLggkS+1kOd8/jKxEN++biMmmDqqCWbzNdmEd1j4lctSlH6M7tt0ywmXIYdZMz5kxcLAMNXsaqnPdikI9uPJZQEL3Kc8hXhXISvpzP7gYOvKHg41uCxu1xCZQOM6pTlNbxemBYqvES4fRh2xvB9aMjwkB4Nz8jrIsyoPI89i05OmGMkI5BPZt8NTa40Yf3yU+SQECW0GWalB5cxaTMeB01tqslUzBJPV3cQx+AhtQG4hkOhQnAMDJramSPVtwbEnqOjQ+lyNmg5GQ4FJO02ApKJTZDTHAgMBAAGjggHCMIIBvjAJBgNVHRMEAjAAMB8GA1UdIwQYMBaAFD+u9XgLkqNwIDVfWvr3JKBSAfBBMB0GA1UdDgQWBBQ1gsJfVC7KYGiWVLP7ZwzppyVYTTAOBgNVHQ8BAf8EBAMCBLAwFgYDVR0gBA8wDTALBglghEIBGgEAAwIwgbsGA1UdHwSBszCBsDA3oDWgM4YxaHR0cDovL2NybC50ZXN0NC5idXlwYXNzLm5vL2NybC9CUENsYXNzM1Q0Q0EzLmNybDB1oHOgcYZvbGRhcDovL2xkYXAudGVzdDQuYnV5cGFzcy5uby9kYz1CdXlwYXNzLGRjPU5PLENOPUJ1eXBhc3MlMjBDbGFzcyUyMDMlMjBUZXN0NCUyMENBJTIwMz9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0MIGKBggrBgEFBQcBAQR+MHwwOwYIKwYBBQUHMAGGL2h0dHA6Ly9vY3NwLnRlc3Q0LmJ1eXBhc3Mubm8vb2NzcC9CUENsYXNzM1Q0Q0EzMD0GCCsGAQUFBzAChjFodHRwOi8vY3J0LnRlc3Q0LmJ1eXBhc3Mubm8vY3J0L0JQQ2xhc3MzVDRDQTMuY2VyMA0GCSqGSIb3DQEBCwUAA4IBAQCe67UOZ/VSwcH2ov1cOSaWslL7JNfqhyNZWGpfgX1c0Gh+KkO3eVkMSozpgX6M4eeWBWJGELMiVN1LhNaGxBU9TBMdeQ3SqK219W6DXRJ2ycBtaVwQ26V5tWKRN4UlRovYYiY+nMLx9VrLOD4uoP6fm9GE5Fj0vSMMPvOEXi0NsN+8MUm3HWoBeUCLyFpe7/EPsS/Wud5bb0as/E2zIztRodxfNsoiXNvWaP2ZiPWFunIjK1H/8EcktEW1paiPd8AZek/QQoG0MKPfPIJuqH+WJU3a8J8epMDyVfaek+4+l9XOeKwVXNSOP/JSwgpOJNzTdaDOM+uVuk75n2191Fd7")).Thumbprint;
            string actualThumb = _printSertifikatSvar.Printsertifikat.Thumbprint;
            Assert.AreEqual(expectedThumb, actualThumb);
        }

        [TestMethod]
        public void HentPostkasseleverandørAdresseSuksess()
        {
            Assert.AreEqual("984661185", _printSertifikatSvar.PostkasseleverandørAdresse);
        }

        private static string Envelope()
        {
            return "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header><wsse:Security xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\" SOAP-ENV:mustUnderstand=\"1\"><xenc:EncryptedKey xmlns:xenc=\"http://www.w3.org/2001/04/xmlenc#\" Id=\"EK-A8B662655B5F7C9FF614302966018041013390\"><xenc:EncryptionMethod Algorithm=\"http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p\" /><ds:KeyInfo xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\"><wsse:SecurityTokenReference><ds:X509Data><ds:X509IssuerSerial><ds:X509IssuerName>CN=Buypass Class 3 Test4 CA 3,O=Buypass AS-983163327,C=NO</ds:X509IssuerName><ds:X509SerialNumber>118253491097063369020649</ds:X509SerialNumber></ds:X509IssuerSerial></ds:X509Data></wsse:SecurityTokenReference></ds:KeyInfo><xenc:CipherData><xenc:CipherValue>sh27gc1Xk8STlNLKHhuwz+a4t3J5v5/2/2nq0uOCWtnsVzlfs8Hqo2REf1dvQxgC7SiiSgGETKsgi4mlG0hEMUd+HDAKZwLp0GZD55b2DUBt+nmp/epGV044nnsaTIG/6w1EKKCoqjUHKlidhx/1VpnLwefRBYBXHY7/L0X5zz0vxxDmdMxjj7HCTFEA5PDjVkzz3uF1AUwToFqLZ3jZ42jwjSCulxTLSeFqJDHU1tOXsiLU11t+3uEFyqfbvAU9OPt/kes4nuLLAeSMuzu58U3YOzHfygTY9EA/ESr0oK0whUeod5CHLyorVy1uvdAd2NER/lN3hvziCTiE4+/YlA==</xenc:CipherValue></xenc:CipherData><xenc:ReferenceList><xenc:DataReference URI=\"#ED-A8B662655B5F7C9FF614302966018041013391\" /></xenc:ReferenceList></xenc:EncryptedKey><wsu:Timestamp wsu:Id=\"TS-A8B662655B5F7C9FF614302966018031013389\"><wsu:Created>2015-04-29T08:36:41.803Z</wsu:Created><wsu:Expires>2015-04-29T08:37:41.803Z</wsu:Expires></wsu:Timestamp><ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" Id=\"SIG-A8B662655B5F7C9FF614302966017911013388\"><ds:SignedInfo><ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /><ds:SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" /><ds:Reference URI=\"#id-A8B662655B5F7C9FF614302966017851013387\"><ds:Transforms><ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /></ds:Transforms><ds:DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><ds:DigestValue>SaxAKNo2dD91k7RQMLyj/bj7Mc8=</ds:DigestValue></ds:Reference><ds:Reference URI=\"#SC-A8B662655B5F7C9FF614302966017841013383\"><ds:Transforms><ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /></ds:Transforms><ds:DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><ds:DigestValue>jH+JLyIIXvxxzTW9oTyMTvVp97U=</ds:DigestValue></ds:Reference></ds:SignedInfo><ds:SignatureValue>mJhsE5pdTBlZCXPjurel6su0swZB8BKcLG4+D8W7mhhNdL8BtuXkslRw4YJS5x7P3SxkJQ73WIXXIDjJjB0x/wLzBuwxxWwL8ttol28LXsEqmKyXYq1abVeHV3CbQdGt6SKOkP29GqLlU2wkbI15eGSEw734fepRIKPqwccmQpN/o3CUmOLgvekj0OT3wtbsBp97hbduWZr+w3gzG/BwmlyXG9x+/00jT51GuSrmtVc8AHzbSwqvYo5awV+3yhvXph/tcbvEa9P9WxAmD+NdRbySU5zewhCevjou6T5uebDAutHHbsrp+LUK0ySls5z8JXlRytRTT4m3qZyyT9aubQ==</ds:SignatureValue><ds:KeyInfo Id=\"KI-A8B662655B5F7C9FF614302966017851013385\"><wsse:SecurityTokenReference wsu:Id=\"STR-A8B662655B5F7C9FF614302966017851013386\"><ds:X509Data><ds:X509IssuerSerial><ds:X509IssuerName>CN=Buypass Class 3 CA 3,O=Buypass AS-983163327,C=NO</ds:X509IssuerName><ds:X509SerialNumber>234761966151431973311547</ds:X509SerialNumber></ds:X509IssuerSerial></ds:X509Data></wsse:SecurityTokenReference></ds:KeyInfo></ds:Signature><wsse11:SignatureConfirmation xmlns:wsse11=\"http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd\" Value=\"N1wfA5vfQAMtSm5mgQvfSJ89xHhXvJNjB8Yl8NJNxjYj2Fsu9rJFyk3w+jMSC/Tf3PwQ0bMOCxbTI/vebMLm/JTVNYc5Qwc40KfkbdR6jZkW6HC3zv5+p3jFX5NAMBImVwdDxoozpH6TetVkMTxhk4sxLApmXlu/aUUrlquaIHK0231nSxKwc+wqRSGDmAHXhH76jIWNr9EpCQFn9xlsj22zFubR2wvldHWXYQgZ5ZheekjorGxzzVTg8NOPgFxsfoF4UgOGePidexgy1ptViGa2qZRd8l/2GYDRaD53ubMtHlGN7nXfKSloFDho/W8+VwiLauskl/Nn9zhYVvJShQ==\" wsu:Id=\"SC-A8B662655B5F7C9FF614302966017841013383\" /></wsse:Security></SOAP-ENV:Header><SOAP-ENV:Body xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\" wsu:Id=\"id-A8B662655B5F7C9FF614302966017851013387\"><ns2:HentPrintSertifikatRespons xmlns:ns2=\"http://kontaktinfo.difi.no/xsd/oppslagstjeneste/14-05\">" +
                        "<ns2:postkasseleverandoerAdresse>984661185</ns2:postkasseleverandoerAdresse>" +
                        "<ns2:X509Sertifikat>" +
                            "MIIE7jCCA9agAwIBAgIKGBZrmEgzTHzeJjANBgkqhkiG9w0BAQsFADBRMQswCQYDVQQGEwJOTzEdMBsGA1UECgwUQnV5cGFzcyBBUy05ODMxNjMzMjcxIzAhBgNVBAMMGkJ1eXBhc3MgQ2xhc3MgMyBUZXN0NCBDQSAzMB4XDTE0MDQyNDEyMzA1MVoXDTE3MDQyNDIxNTkwMFowVTELMAkGA1UEBhMCTk8xGDAWBgNVBAoMD1BPU1RFTiBOT1JHRSBBUzEYMBYGA1UEAwwPUE9TVEVOIE5PUkdFIEFTMRIwEAYDVQQFEwk5ODQ2NjExODUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCLCxU4oBhtGmJxXZWbdWdzO2uA3eRNW/kPdddL1HYl1iXLV/g+H2Q0ELadWLggkS+1kOd8/jKxEN++biMmmDqqCWbzNdmEd1j4lctSlH6M7tt0ywmXIYdZMz5kxcLAMNXsaqnPdikI9uPJZQEL3Kc8hXhXISvpzP7gYOvKHg41uCxu1xCZQOM6pTlNbxemBYqvES4fRh2xvB9aMjwkB4Nz8jrIsyoPI89i05OmGMkI5BPZt8NTa40Yf3yU+SQECW0GWalB5cxaTMeB01tqslUzBJPV3cQx+AhtQG4hkOhQnAMDJramSPVtwbEnqOjQ+lyNmg5GQ4FJO02ApKJTZDTHAgMBAAGjggHCMIIBvjAJBgNVHRMEAjAAMB8GA1UdIwQYMBaAFD+u9XgLkqNwIDVfWvr3JKBSAfBBMB0GA1UdDgQWBBQ1gsJfVC7KYGiWVLP7ZwzppyVYTTAOBgNVHQ8BAf8EBAMCBLAwFgYDVR0gBA8wDTALBglghEIBGgEAAwIwgbsGA1UdHwSBszCBsDA3oDWgM4YxaHR0cDovL2NybC50ZXN0NC5idXlwYXNzLm5vL2NybC9CUENsYXNzM1Q0Q0EzLmNybDB1oHOgcYZvbGRhcDovL2xkYXAudGVzdDQuYnV5cGFzcy5uby9kYz1CdXlwYXNzLGRjPU5PLENOPUJ1eXBhc3MlMjBDbGFzcyUyMDMlMjBUZXN0NCUyMENBJTIwMz9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0MIGKBggrBgEFBQcBAQR+MHwwOwYIKwYBBQUHMAGGL2h0dHA6Ly9vY3NwLnRlc3Q0LmJ1eXBhc3Mubm8vb2NzcC9CUENsYXNzM1Q0Q0EzMD0GCCsGAQUFBzAChjFodHRwOi8vY3J0LnRlc3Q0LmJ1eXBhc3Mubm8vY3J0L0JQQ2xhc3MzVDRDQTMuY2VyMA0GCSqGSIb3DQEBCwUAA4IBAQCe67UOZ/VSwcH2ov1cOSaWslL7JNfqhyNZWGpfgX1c0Gh+KkO3eVkMSozpgX6M4eeWBWJGELMiVN1LhNaGxBU9TBMdeQ3SqK219W6DXRJ2ycBtaVwQ26V5tWKRN4UlRovYYiY+nMLx9VrLOD4uoP6fm9GE5Fj0vSMMPvOEXi0NsN+8MUm3HWoBeUCLyFpe7/EPsS/Wud5bb0as/E2zIztRodxfNsoiXNvWaP2ZiPWFunIjK1H/8EcktEW1paiPd8AZek/QQoG0MKPfPIJuqH+WJU3a8J8epMDyVfaek+4+l9XOeKwVXNSOP/JSwgpOJNzTdaDOM+uVuk75n2191Fd7" +
                        "</ns2:X509Sertifikat></ns2:HentPrintSertifikatRespons></SOAP-ENV:Body>" +
                   "</SOAP-ENV:Envelope>";
        }

    }
}
