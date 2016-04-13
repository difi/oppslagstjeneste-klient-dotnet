using System;
using System.Linq;
using System.Net;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Examples;
using Difi.Oppslagstjeneste.Klient.Tester.Utilities;
using Difi.Oppslagstjeneste.Klient.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester
{
    [TestClass]
    public class OppslagstjenesteKlientTests
    {
        [TestClass]
        public class KonstruktørMethod : OppslagstjenesteKlientTests
        {
            [TestMethod]
            public void KonstruktørMedSertifikater()
            {
                //Arrange
                var avsenderEnhetstesterSertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat();
                var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø, avsenderEnhetstesterSertifikat);

                //Act
                var oppslagstjenesteKlient = new OppslagstjenesteKlient(
                    oppslagstjenesteKonfigurasjon
                    );

                //Assert
                Assert.AreEqual(avsenderEnhetstesterSertifikat,
                    oppslagstjenesteKlient.OppslagstjenesteKonfigurasjon.Avsendersertifikat);
                Assert.AreEqual(oppslagstjenesteKonfigurasjon, oppslagstjenesteKlient.OppslagstjenesteKonfigurasjon);
            }
        }
    }
}