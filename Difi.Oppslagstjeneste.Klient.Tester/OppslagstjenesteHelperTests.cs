using System;
using System.Linq;
using System.Net;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;
using Assert = Xunit.Assert;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    
    public class OppslagstjenesteHelperTests
    {
        
        public class SendAsyncMethod : OppslagstjenesteHelperTests
        {
            
            [Fact(Skip = "ignored")]
            public void Handles_generic_soap_fault_correctly()
            {
                //Arrange
                var senderUnitTestCertificate = DomainUtility.GetAvsenderTestCertificate();
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
                    Assert.True(exception.GetType() == typeof (UventetFeilException));
                    var soapException = exception as UventetFeilException;
                    Assert.Equal("env:Sender", soapException.Skyldig);
                    Assert.Equal("Invalid service usage: Service owner 988015814 does not have access to ENDRINGSTJENESTEN", soapException.Beskrivelse);
                }
            }
        }
    }
}
