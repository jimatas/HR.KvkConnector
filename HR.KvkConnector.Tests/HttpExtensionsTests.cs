using HR.KvkConnector.Infrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net;

namespace HR.KvkConnector.Tests
{
    [TestClass]
    public class HttpExtensionsTests
    {
        [TestMethod]
        public void AddTrustedRootCertificates_ByDefault_SetsServerCertificateValidationCallback()
        {
            // Arrange
            var httpRequest = WebRequest.CreateHttp("https://ssltest.kvk.nl");

            // Act
            var callbackBefore = httpRequest.ServerCertificateValidationCallback;
            httpRequest.AddTrustedRootCertificates();
            var callbackAfter = httpRequest.ServerCertificateValidationCallback;

            // Assert
            Assert.IsNull(callbackBefore);
            Assert.IsNotNull(callbackAfter);
        }

        #region Integration tests
        [TestMethod]
        public void ExecutingWebRequest_WithoutCallingAddTrustedRootCertificates_ThrowsException()
        {
            // Arrange
            var httpRequest = WebRequest.CreateHttp("https://ssltest.kvk.nl");

            // Act
            void action() => httpRequest.GetResponse();

            // Assert
            Assert.ThrowsException<WebException>(action);
        }

        [TestMethod]
        public void ExecutingWebRequest_AfterCallingAddTrustedRootCertificates_ReturnsResponse()
        {
            // Arrange
            var httpRequest = WebRequest.CreateHttp("https://ssltest.kvk.nl");
            httpRequest.AddTrustedRootCertificates();

            // Act
            using var httpResponse = httpRequest.GetResponse();

            // Assert
            Assert.IsNotNull(httpResponse);
        }
        #endregion
    }
}
