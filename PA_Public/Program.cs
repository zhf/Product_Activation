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
            if (args.Length == 1)
            {
                // Got Signature (License)? Then Verify it
                byte[] signature = Convert.FromBase64String(args[0]);
                // Public key must be hard-coded!
                const string publicKeyString = "RUNTMSAAAACOUGMbADUgo3dDq42gwv+uCPsI8jjQm61r0CUPVioibLfzskpsGmUAJD29rt3FzS5qb28gS5Ed85jDPwQoesBJ";
                byte[] publicKey = Convert.FromBase64String(publicKeyString);
                CngKey cngKey = CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob);
                ECDsaCng dsa = new ECDsaCng(cngKey);
                dsa.HashAlgorithm = CngAlgorithm.MD5;
                if (dsa.VerifyData(MachineID.ComputeEasyMachineID(), signature))
                {
                    Console.WriteLine("License is valid. Thank you!");
                }
                else
                {
                    Console.WriteLine("License is invalid!");
                }
                dsa.Clear();
            }
            else
            {
                // No Signature (License). Display InstallationID
                Console.WriteLine("No license found. Contact your software vendor and provide the following code:");
                foreach (byte b in MachineID.ComputeEasyMachineID())
                {
                    Console.Write(b);
                }
            }
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
    }
}
