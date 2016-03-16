using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServer.Security
{
    //From: https://github.com/scottbrady91/IdentityServer3-Example/blob/master/src/ScottBrady91.IdentityServer3.Example/Configuration/Cert.cs
    internal static class Cert
    {
        public static X509Certificate2 Load()
        {
            var assembly = typeof(Cert).Assembly;
            using (var stream = assembly.GetManifestResourceStream("IdentityServer.Security.identity_server_demos.pfx"))
            {
                return new X509Certificate2(ReadStream(stream), "demo");
            }
        }

        private static byte[] ReadStream(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}