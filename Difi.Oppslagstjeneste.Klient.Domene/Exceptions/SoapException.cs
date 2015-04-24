using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class SoapException : OppslagstjenesteException
    {

        internal SoapException(string outerXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(outerXml);

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("env", Navnerom.env);
            namespaceManager.AddNamespace("eb", Navnerom.eb);
            
             try
            {
                var rot = xmlDocument.DocumentElement;
                var xPath = "//ns6:MessageId"; //Endre til passende
                var targetNode = rot.SelectSingleNode(xPath, namespaceManager);

        }
    }
}
