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
            ParseToClassMembers();
        }

        private XmlNamespaceManager InitalizeNamespaceManager(XmlDocument xmlDocument)
        {
            var namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("env", Navnerom.SoapEnvelope);
            namespaceManager.AddNamespace("ns", Navnerom.OppslagstjenesteDefinisjon);
            namespaceManager.AddNamespace("difi", Navnerom.OppslagstjenesteMetadata);
            return namespaceManager;
        }

        protected abstract void ParseToClassMembers();
    }
}
