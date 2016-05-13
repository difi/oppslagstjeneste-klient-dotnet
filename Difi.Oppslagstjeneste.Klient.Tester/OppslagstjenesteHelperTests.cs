using System;
using System.Linq;
using System.Net;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
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
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                var response = XmlResource.Response.GetSoapFault();

                //Act
                try
                {
                    var oppslagstjenesteClientMock = MockUtility.OppslagstjenesteKlientMock(response.OuterXml, HttpStatusCode.Forbidden, senderUnitTestCertificate);
                    var client = oppslagstjenesteClientMock.Object;
                    client.HentPersoner(new[] {"31108412345"}, Informasjonsbehov.Person);
                }
                catch (AggregateException e)
                {
                    var exception = e.InnerExceptions.ElementAt(0);

                    //Assert
                    Assert.IsTrue(exception.GetType() == typeof (UventetFeilException));
                    var soapException = exception as UventetFeilException;
                    Assert.AreEqual("env:Sender", soapException.Skyldig);
                    Assert.AreEqual("Invalid service usage: Service owner 988015814 does not have access to ENDRINGSTJENESTEN", soapException.Beskrivelse);
                }
            }
        }
    }
}