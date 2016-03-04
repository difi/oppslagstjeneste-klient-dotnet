using System;
using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Domene
{
    [TestClass]
    public class PersonTester
    {
        private static Person _person;

        /// <summary>
        ///     Tester oppretting av personobjekt fra Xml. Når dette objektet testes blir også følgende klasser
        ///     implisitt testet: Epostadresse, Kontaktinformasjon, Mobiltelefonnummer og Sikkerdigitalpostadresse.
        /// </summary>
        [ClassInitialize]
        public static void OpprettPersonFraXmlSuksess(TestContext context)
        {
            var xmlDocument = XmlUtility.TilXmlDokument(PersonXml());
            var deserialisertPerson = SerializeUtil.Deserialize<DTO.Person>(xmlDocument.InnerXml);
            _person = DtoKonverterer.TilDomeneObjekt(deserialisertPerson);
        }

        [TestMethod]
        public void HentPersonidentifikatorSuksess()
        {
            Assert.AreEqual("08077000292", _person.Personidentifikator);
        }

        [TestMethod]
        public void HentReservasjonOgStatusSuksess()
        {
            Assert.IsFalse(_person.Reservasjon);
            Assert.AreEqual(Status.AKTIV, _person.Status);
        }

        [TestMethod]
        public void HentMobiltelefonnummerOgMetadataSuksess()
        {
            Assert.AreEqual("+4740485641", _person.Kontaktinformasjon.Mobiltelefonnummer.Nummer);
            Assert.AreEqual(new DateTime(2015, 04, 14, 14, 35, 19),
                _person.Kontaktinformasjon.Mobiltelefonnummer.SistOppdatert);
            Assert.AreEqual(new DateTime(2015, 04, 14, 14, 35, 19),
                _person.Kontaktinformasjon.Mobiltelefonnummer.SistVerifisert);
        }

        [TestMethod]
        public void HentEpostOgMetadataSuksess()
        {
            Assert.AreEqual("extfjo@vegvesen.no", _person.Kontaktinformasjon.Epostadresse.Epost);
            Assert.AreEqual(new DateTime(2015, 04, 14, 14, 35, 30),
                _person.Kontaktinformasjon.Epostadresse.SistOppdatert);
            Assert.AreEqual(new DateTime(2015, 04, 14, 14, 35, 30),
                _person.Kontaktinformasjon.Epostadresse.SistVerifisert);
        }

        [TestMethod]
        public void HentSikkerDigitalPostAdresseSuksess()
        {
            Assert.AreEqual("id.porten.testuser#2346", _person.SikkerDigitalPostAdresse.Postkasseadresse);
            Assert.AreEqual("984661185", _person.SikkerDigitalPostAdresse.PostkasseleverandørAdresse);
        }

        [TestMethod]
        public void HentSertifikatSuksess()
        {
            var expected =
                new X509Certificate2(
                    Convert.FromBase64String(
                        @"MIIE7jCCA9agAwIBAgIKGBZrmEgzTHzeJjANBgkqhkiG9w0BAQsFADBRMQswCQYDVQQGEwJOTzEdMBsGA1UECgwUQnV5cGFzcyBBUy05ODMxNjMzMjcxIzAhBgNVBAMMGkJ1eXBhc3MgQ2xhc3MgMyBUZXN0NCBDQSAzMB4XDTE0MDQyNDEyMzA1MVoXDTE3MDQyNDIxNTkwMFowVTELMAkGA1UEBhMCTk8xGDAWBgNVBAoMD1BPU1RFTiBOT1JHRSBBUzEYMBYGA1UEAwwPUE9TVEVOIE5PUkdFIEFTMRIwEAYDVQQFEwk5ODQ2NjExODUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCLCxU4oBhtGmJxXZWbdWdzO2uA3eRNW/kPdddL1HYl1iXLV/g+H2Q0ELadWLggkS+1kOd8/jKxEN++biMmmDqqCWbzNdmEd1j4lctSlH6M7tt0ywmXIYdZMz5kxcLAMNXsaqnPdikI9uPJZQEL3Kc8hXhXISvpzP7gYOvKHg41uCxu1xCZQOM6pTlNbxemBYqvES4fRh2xvB9aMjwkB4Nz8jrIsyoPI89i05OmGMkI5BPZt8NTa40Yf3yU+SQECW0GWalB5cxaTMeB01tqslUzBJPV3cQx+AhtQG4hkOhQnAMDJramSPVtwbEnqOjQ+lyNmg5GQ4FJO02ApKJTZDTHAgMBAAGjggHCMIIBvjAJBgNVHRMEAjAAMB8GA1UdIwQYMBaAFD+u9XgLkqNwIDVfWvr3JKBSAfBBMB0GA1UdDgQWBBQ1gsJfVC7KYGiWVLP7ZwzppyVYTTAOBgNVHQ8BAf8EBAMCBLAwFgYDVR0gBA8wDTALBglghEIBGgEAAwIwgbsGA1UdHwSBszCBsDA3oDWgM4YxaHR0cDovL2NybC50ZXN0NC5idXlwYXNzLm5vL2NybC9CUENsYXNzM1Q0Q0EzLmNybDB1oHOgcYZvbGRhcDovL2xkYXAudGVzdDQuYnV5cGFzcy5uby9kYz1CdXlwYXNzLGRjPU5PLENOPUJ1eXBhc3MlMjBDbGFzcyUyMDMlMjBUZXN0NCUyMENBJTIwMz9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0MIGKBggrBgEFBQcBAQR+MHwwOwYIKwYBBQUHMAGGL2h0dHA6Ly9vY3NwLnRlc3Q0LmJ1eXBhc3Mubm8vb2NzcC9CUENsYXNzM1Q0Q0EzMD0GCCsGAQUFBzAChjFodHRwOi8vY3J0LnRlc3Q0LmJ1eXBhc3Mubm8vY3J0L0JQQ2xhc3MzVDRDQTMuY2VyMA0GCSqGSIb3DQEBCwUAA4IBAQCe67UOZ/VSwcH2ov1cOSaWslL7JNfqhyNZWGpfgX1c0Gh+KkO3eVkMSozpgX6M4eeWBWJGELMiVN1LhNaGxBU9TBMdeQ3SqK219W6DXRJ2ycBtaVwQ26V5tWKRN4UlRovYYiY+nMLx9VrLOD4uoP6fm9GE5Fj0vSMMPvOEXi0NsN+8MUm3HWoBeUCLyFpe7/EPsS/Wud5bb0as/E2zIztRodxfNsoiXNvWaP2ZiPWFunIjK1H/8EcktEW1paiPd8AZek/QQoG0MKPfPIJuqH+WJU3a8J8epMDyVfaek+4+l9XOeKwVXNSOP/JSwgpOJNzTdaDOM+uVuk75n2191Fd7"));
            Assert.AreEqual(expected.Thumbprint, _person.X509Sertifikat.Thumbprint);
        }

        private static string PersonXml()
        {
            return
                "<ns2:Person xmlns:ns2=\"http://begrep.difi.no\">" +
                "<ns2:personidentifikator>08077000292</ns2:personidentifikator>" +
                "<ns2:reservasjon>NEI</ns2:reservasjon>" +
                "<ns2:status>AKTIV</ns2:status>" +
                "<ns2:Kontaktinformasjon>" +
                "<ns2:Mobiltelefonnummer sistOppdatert=\"2015-04-14T14:35:19.000+02:00\" sistVerifisert=\"2015-04-14T14:35:19.000+02:00\">+4740485641</ns2:Mobiltelefonnummer>" +
                " <ns2:Epostadresse sistOppdatert=\"2015-04-14T14:35:30.000+02:00\" sistVerifisert=\"2015-04-14T14:35:30.000+02:00\">extfjo@vegvesen.no</ns2:Epostadresse>" +
                "</ns2:Kontaktinformasjon>" +
                "<ns2:SikkerDigitalPostAdresse>" +
                "<ns2:postkasseadresse>id.porten.testuser#2346</ns2:postkasseadresse>" +
                " <ns2:postkasseleverandoerAdresse>984661185</ns2:postkasseleverandoerAdresse>" +
                "</ns2:SikkerDigitalPostAdresse>" +
                " <ns2:X509Sertifikat>MIIE7jCCA9agAwIBAgIKGBZrmEgzTHzeJjANBgkqhkiG9w0BAQsFADBRMQswCQYDVQQGEwJOTzEdMBsGA1UECgwUQnV5cGFzcyBBUy05ODMxNjMzMjcxIzAhBgNVBAMMGkJ1eXBhc3MgQ2xhc3MgMyBUZXN0NCBDQSAzMB4XDTE0MDQyNDEyMzA1MVoXDTE3MDQyNDIxNTkwMFowVTELMAkGA1UEBhMCTk8xGDAWBgNVBAoMD1BPU1RFTiBOT1JHRSBBUzEYMBYGA1UEAwwPUE9TVEVOIE5PUkdFIEFTMRIwEAYDVQQFEwk5ODQ2NjExODUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCLCxU4oBhtGmJxXZWbdWdzO2uA3eRNW/kPdddL1HYl1iXLV/g+H2Q0ELadWLggkS+1kOd8/jKxEN++biMmmDqqCWbzNdmEd1j4lctSlH6M7tt0ywmXIYdZMz5kxcLAMNXsaqnPdikI9uPJZQEL3Kc8hXhXISvpzP7gYOvKHg41uCxu1xCZQOM6pTlNbxemBYqvES4fRh2xvB9aMjwkB4Nz8jrIsyoPI89i05OmGMkI5BPZt8NTa40Yf3yU+SQECW0GWalB5cxaTMeB01tqslUzBJPV3cQx+AhtQG4hkOhQnAMDJramSPVtwbEnqOjQ+lyNmg5GQ4FJO02ApKJTZDTHAgMBAAGjggHCMIIBvjAJBgNVHRMEAjAAMB8GA1UdIwQYMBaAFD+u9XgLkqNwIDVfWvr3JKBSAfBBMB0GA1UdDgQWBBQ1gsJfVC7KYGiWVLP7ZwzppyVYTTAOBgNVHQ8BAf8EBAMCBLAwFgYDVR0gBA8wDTALBglghEIBGgEAAwIwgbsGA1UdHwSBszCBsDA3oDWgM4YxaHR0cDovL2NybC50ZXN0NC5idXlwYXNzLm5vL2NybC9CUENsYXNzM1Q0Q0EzLmNybDB1oHOgcYZvbGRhcDovL2xkYXAudGVzdDQuYnV5cGFzcy5uby9kYz1CdXlwYXNzLGRjPU5PLENOPUJ1eXBhc3MlMjBDbGFzcyUyMDMlMjBUZXN0NCUyMENBJTIwMz9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0MIGKBggrBgEFBQcBAQR+MHwwOwYIKwYBBQUHMAGGL2h0dHA6Ly9vY3NwLnRlc3Q0LmJ1eXBhc3Mubm8vb2NzcC9CUENsYXNzM1Q0Q0EzMD0GCCsGAQUFBzAChjFodHRwOi8vY3J0LnRlc3Q0LmJ1eXBhc3Mubm8vY3J0L0JQQ2xhc3MzVDRDQTMuY2VyMA0GCSqGSIb3DQEBCwUAA4IBAQCe67UOZ/VSwcH2ov1cOSaWslL7JNfqhyNZWGpfgX1c0Gh+KkO3eVkMSozpgX6M4eeWBWJGELMiVN1LhNaGxBU9TBMdeQ3SqK219W6DXRJ2ycBtaVwQ26V5tWKRN4UlRovYYiY+nMLx9VrLOD4uoP6fm9GE5Fj0vSMMPvOEXi0NsN+8MUm3HWoBeUCLyFpe7/EPsS/Wud5bb0as/E2zIztRodxfNsoiXNvWaP2ZiPWFunIjK1H/8EcktEW1paiPd8AZek/QQoG0MKPfPIJuqH+WJU3a8J8epMDyVfaek+4+l9XOeKwVXNSOP/JSwgpOJNzTdaDOM+uVuk75n2191Fd7</ns2:X509Sertifikat>" +
                "</ns2:Person>";
        }
    }
}