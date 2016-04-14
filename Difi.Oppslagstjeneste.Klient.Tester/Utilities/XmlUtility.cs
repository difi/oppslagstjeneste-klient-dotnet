using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Tests.Utilities
{
    internal class XmlUtility
    {
        internal static XmlDocument ToXmlDocument(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            return xmlDocument;
        }
    }
}