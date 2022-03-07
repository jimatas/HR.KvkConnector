using System;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HR.KvkConnector.Infrastructure
{
    internal static class HttpExtensions
    {
        /// <summary>
        /// Asynchronously returns a <see cref="HttpWebResponse"/> object that contains the response from the internet resource.
        /// </summary>
        /// <param name="httpRequest">The request to the internet resource.</param>
        /// <param name="ensureSuccessCode">If <c>true</c>, a status code that signals an HTTP error will result in an exception being thrown, which is also the default behavior for <see cref="HttpWebRequest.GetResponse"/>.
        /// If <c>false</c>, no exception will be thrown in case of an error status code, and the response will be returned.</param>
        /// <returns>The response from the internet resource.</returns>
        public static async Task<HttpWebResponse> GetHttpResponseAsync(this HttpWebRequest httpRequest, bool ensureSuccessCode = true)
            => await Task<HttpWebResponse>.Factory.FromAsync(
                beginMethod: httpRequest.BeginGetResponse,
                endMethod: asyncResult =>
                {
                    try
                    {
                        return (HttpWebResponse)httpRequest.EndGetResponse(asyncResult);
                    }
                    catch (WebException exception) when (!ensureSuccessCode && exception.Status == WebExceptionStatus.ProtocolError)
                    {
                        return (HttpWebResponse)exception.Response;
                    }
                },
                state: null).WithoutCapturingContext();

        /// <summary>
        /// Determines whether the status code of the response indicates an HTTP error (i.e., 400+, 500+).
        /// </summary>
        /// <param name="httpResponse">The response from the internet resource.</param>
        /// <returns><c>true</c> if the status code of the response indicates an error, <c>false</c> otherwise.</returns>
        public static bool IsErrorStatusCode(this HttpWebResponse httpResponse)
            => (int)httpResponse.StatusCode >= 400 && (int)httpResponse.StatusCode <= 599;

        /// <summary>
        /// Sets up a <see cref="RemoteCertificateValidationCallback"/> function that will attempt to handle an untrusted root certificate validation failure
        /// by matching the server provided root certificate to any of the trusted root certificates included as embedded resources in the current assembly.
        /// </summary>
        /// <param name="httpRequest"></param>
        public static void AddTrustedRootCertificates(this HttpWebRequest httpRequest)
        {
            httpRequest.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                {
                    return true;
                }

                var trustedRootCertificates = new Lazy<X509Certificate2Collection>(() =>
                {
                    var certificates = new X509Certificate2Collection();
                    certificates.LoadFromEmbeddedResourceFiles();
                    return certificates;
                });

                foreach (var element in chain.ChainElements)
                {
                    foreach (var status in element.ChainElementStatus)
                    {
                        if (status.Status == X509ChainStatusFlags.UntrustedRoot) // Note: not using HasFlag() as we only wish to handle the UntrustedRoot case.
                        {
                            if (trustedRootCertificates.Value.ContainsByCertHash(element.Certificate))
                            {
                                continue;
                            }
                        }
                        return false;
                    }
                }
                return true;
            };
        }
    }
}
