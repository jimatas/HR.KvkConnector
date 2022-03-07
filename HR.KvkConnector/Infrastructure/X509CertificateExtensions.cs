using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace HR.KvkConnector.Infrastructure
{
    internal static class X509CertificateExtensions
    {
        /// <summary>
        /// Loads all the certificates that are included as embedded resources in the current assembly and adds them to the <see cref="X509CertificateCollection"/>.
        /// </summary>
        /// <param name="certificates">The certificates collection to load the certificates into.</param>
        public static void LoadFromEmbeddedResourceFiles(this X509CertificateCollection certificates)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            foreach (var certificateFileName in executingAssembly.GetManifestResourceNames()
                .Where(resourceName => resourceName.EndsWith(".crt") || resourceName.EndsWith(".cer")))
            {
                using (var fileStream = executingAssembly.GetManifestResourceStream(certificateFileName))
                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    certificates.Add(new X509Certificate2(memoryStream.ToArray()));
                }
            }
        }

        /// <summary>
        /// Using the hash values of the certificates to determine equality, returns a boolean value indicating whether the <see cref="X509CertificateCollection"/> contains the specified <see cref="X509Certificate"/>.
        /// </summary>
        /// <param name="certificates">The certificate collection to locate the certificate in.</param>
        /// <param name="certificate">The certificate to locate.</param>
        /// <returns></returns>
        public static bool ContainsByCertHash(this X509CertificateCollection certificates, X509Certificate certificate)
        {
            var certHash = certificate.GetCertHash();
            for (var i = 0; i < certificates.Count; i++)
            {
                if (certificates[i].GetCertHash().SequenceEqual(certHash))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
