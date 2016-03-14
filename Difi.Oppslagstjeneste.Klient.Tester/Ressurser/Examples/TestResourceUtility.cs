using System.Text;
using System.Xml;
using ApiClientShared;

namespace Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Examples
{
    internal class TestResourceUtility
    {
        private static readonly ResourceUtility ResourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Examples");

        public class Request
        {
            public class PersonRequest
            {
                public static string AsText()
                {
                    var request = Encoding.UTF8.GetString(ResourceUtility.ReadAllBytes(true, "Request", "HentPersoner.xml"));
                    return request;
                }

                public static XmlDocument AsXmlDocument()
                {
                    return XmlUtility.TilXmlDokument(AsText());
                }
            }
        }

        public class Response
        {
            public class PersonResponse
            {
                public static string AsText()
                {
                    var response = Encoding.UTF8.GetString(ResourceUtility.ReadAllBytes(true, "Response", "HentPersoner.xml"));
                    return response;
                }

                public static XmlDocument AsXmlDocument()
                {
                    return XmlUtility.TilXmlDokument(AsText());
                }
            }

            public class PersonResponseEncrypted
            {
                public static string AsText()
                {
                    var response = Encoding.UTF8.GetString(ResourceUtility.ReadAllBytes(true, "Response", "HentPersonerEncrypted.xml"));
                    return response;
                }

                public static XmlDocument AsXmlDocument()
                {
                    return XmlUtility.TilXmlDokument(AsText());
                }
            }

            public class SoapFaultResponse
            {
                public static string AsText()
                {
                    var response = Encoding.UTF8.GetString(ResourceUtility.ReadAllBytes(true, "Response", "SoapFault.xml"));
                    return response;
                }

                public static XmlDocument AsXmlDocument()
                {
                    return XmlUtility.TilXmlDokument(AsText());
                }
            }

            public class PrintSertificatResponse
            {
                public static string AsText()
                {
                    var respons = Encoding.UTF8.GetString(ResourceUtility.ReadAllBytes(true, "Response", "HentPrintSertifikat.xml"));
                    return respons;
                }

                public static XmlDocument AsXmlDocument()
                {
                    return XmlUtility.TilXmlDokument(AsText());
                }
            }

            public static class EndringerResponse
            {
                public static string AsText()
                {
                    var respons = Encoding.UTF8.GetString(ResourceUtility.ReadAllBytes(true, "Response", "HentEndringer.xml"));
                    return respons;
                }

                public static XmlDocument AsXmlDocument()
                {
                    return XmlUtility.TilXmlDokument(AsText());
                }
            }
        }
    }
}