using System.Linq;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Scripts.XsdToCode.Code;
using Difi.Oppslagstjeneste.Klient.Svar;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Svar
{
    public class EndringerSvarFixture
    {
        public EndringerSvar EndringerSvar { get; }

        public EndringerSvarFixture()
        {
            var xmlDokument = XmlResource.Response.GetEndringer();
            var responseDokument = new ResponseContainer(xmlDokument);
            EndringerSvar = DtoConverter.ToDomainObject(SerializeUtil.Deserialize<HentEndringerRespons>(responseDokument.BodyElement.InnerXml));
        }
    }


    public class EndringerSvarTests : IClassFixture<EndringerSvarFixture>
    {
        private readonly EndringerSvarFixture _fixture;

        public EndringerSvarTests(EndringerSvarFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Hent_tre_personer_endringer_suksess()
        {
            Assert.Equal(3, _fixture.EndringerSvar.Personer.Count());
        }

        [Fact]
        public void Hent_fra_endringsnummer_suksess()
        {
            Assert.Equal(600, _fixture.EndringerSvar.FraEndringsNummer);
        }

        [Fact]
        public void Hent_til_endringsnummer_suksess()
        {
            Assert.Equal(5791, _fixture.EndringerSvar.TilEndringsNummer);
        }

        [Fact]
        public void Hent_seneste_endringsnummer_suksess()
        {
            Assert.Equal(2925086, _fixture.EndringerSvar.SenesteEndringsNummer);
        }
    }
}