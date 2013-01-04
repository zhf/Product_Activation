using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Product_Activation
{
    class Program
    {
        static void Main(string[] args)
        {
            string keyName = "PA ECDSA Key";
            byte[] publicKeyBytes, privateKeyBytes;

            CngKey cngKey;

            if (CngKey.Exists(keyName))
            {
                cngKey = CngKey.Open(keyName);
                cngKey.Delete();
                Console.WriteLine("Existing key deleted.");
            }

            CngKeyCreationParameters creationParameters = new CngKeyCreationParameters();
            // Allow exporting private key as plaintext
            creationParameters.ExportPolicy = CngExportPolicies.AllowPlaintextExport;
            cngKey = CngKey.Create(CngAlgorithm.ECDsaP256, keyName, creationParameters);

            publicKeyBytes = cngKey.Export(CngKeyBlobFormat.EccPublicBlob);
            privateKeyBytes = cngKey.Export(CngKeyBlobFormat.EccPrivateBlob);

            Console.WriteLine("\n\nPrivate: " + Debugger.BytesToString(privateKeyBytes));
            Console.WriteLine("\n\nPublic: " + Debugger.BytesToString(publicKeyBytes));
            Console.WriteLine("\n\nPublic Base64: " + Convert.ToBase64String(publicKeyBytes));
            Console.ReadKey();
        }
    }
}
