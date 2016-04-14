using System;
using System.Linq;
using System.Net;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Tester.Utilities;
using Difi.Oppslagstjeneste.Klient.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester
{
    [TestClass]
    public class OppslagstjenesteHelperTests
    {
        [TestClass]
        public class SendAsyncMethod : OppslagstjenesteHelperTests
        {
            [TestMethod]
            public void OppslagstjenesteKlientHandlesGenericSoapFaultCorrectly()
            {
                //Arrange
                var avsenderEnhetstesterSertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat();
                var response = XmlResource.Response.GetSoapFault();
                //Act
                try
                {
                    var oppslagstjenesteKlientMock = MockUtility.OppslagstjenesteKlientMock(response.OuterXml, HttpStatusCode.Forbidden, avsenderEnhetstesterSertifikat);
                    var client = oppslagstjenesteKlientMock.Object;
                    client.HentPersoner(new[] {"31108412345"}, Informasjonsbehov.Person);
                }
                catch (AggregateException e)
                {
                    var ex = e.InnerExceptions.ElementAt(0);
                    //Assert
                    Assert.IsTrue(ex.GetType() == typeof (SoapException));
                    var soapException = ex as SoapException;
                    Assert.AreEqual("env:Sender", soapException.Skyldig);
                    Assert.AreEqual("Invalid service usage: Service owner 988015814 does not have access to ENDRINGSTJENESTEN", soapException.Beskrivelse);
                }
            }
        }
    }
}