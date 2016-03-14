using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    public class NamespaceManager
    {
        public static XmlNamespaceManager InitalizeNamespaceManager(XmlDocument xmlDocument)
        {
            var namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("env", Navnerom.SoapEnvelope12);
            namespaceManager.AddNamespace("ns", Navnerom.OppslagstjenesteDefinisjon);
            namespaceManager.AddNamespace("difi", Navnerom.OppslagstjenesteMetadata);
            namespaceManager.AddNamespace("wsse", Navnerom.WssecuritySecext10);
            namespaceManager.AddNamespace("ds", Navnerom.XmlDsig);
            namespaceManager.AddNamespace("xenc", Navnerom.Xenc);
            namespaceManager.AddNamespace("wsse11", Navnerom.WssecuritySecext11);
            namespaceManager.AddNamespace("wsu", Navnerom.WssecurityUtility10);
            return namespaceManager;
        }
    }
}