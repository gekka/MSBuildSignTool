namespace CSConsoleCore
{
    using System;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;


    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] bs = new CertificateRequest("CN=TestUser,O=Test.org,C=JP", RSA.Create(2048), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)
                       .CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(100))
                       .Export(X509ContentType.Pfx, "123456");

            System.IO.File.WriteAllBytes("Test.pfx", bs);            
        }
    }
}
