using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    public abstract class Svar
    {
        protected readonly XmlDocument XmlDocument;
        protected readonly XmlNamespaceManager XmlNamespaceManager;

        protected Svar(XmlDocument xmlDocument)
        {
            XmlDocument = xmlDocument;
            XmlNamespaceManager = InitalizeNamespaceManager(XmlDocument);
        }

        private XmlNamespaceManager InitalizeNamespaceManager(XmlDocument xmlDocument)
        {
            var namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("env", Navnerom.env11);
            namespaceManager.AddNamespace("ns", Navnerom.krr);
            namespaceManager.AddNamespace("difi", Navnerom.difi);
            return namespaceManager;
        }
    }
}
