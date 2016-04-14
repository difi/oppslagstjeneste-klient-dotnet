using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Utilities
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