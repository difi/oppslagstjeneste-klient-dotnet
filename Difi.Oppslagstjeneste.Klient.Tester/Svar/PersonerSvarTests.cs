using System.Linq;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Scripts.XsdToCode.Code;
using Difi.Oppslagstjeneste.Klient.Svar;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Svar
{
    
    public class PersonerSvarTests
    {
        private static PersonerSvar _personerSvar;

        public PersonerSvarTests()
        {
            var xmlDocument = XmlResource.Response.GetPerson();
            var responseDokumenter = new ResponseContainer(xmlDocument);
            _personerSvar = DtoConverter.ToDomainObject(SerializeUtil.Deserialize<HentPersonerRespons>(responseDokumenter.BodyElement.InnerXml));
        }

        [Fact]
        public void HentEnPersonSuksess()
        {
            Assert.Equal(1, _personerSvar.Personer.Count());
        }
    }
}