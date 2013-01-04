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
            if (args.Length != 1 && args[0].Length != 8)
            {
                Console.WriteLine("Use a valid MachineID as the argument.");
                return;
            }
            else
            {
                Console.WriteLine("MachineID: " + args[0]);
            }

            // The data to sign.
            byte[] data = new byte[8];

            // Convert MachineID to byte array
            for (int i = 0; i < 8; i++)
            {
                data[i] = Byte.Parse(args[0][i].ToString());
            }
            
            // Load the key from system store
            // This key was created by key gen
            CngKey cngKey = CngKey.Open("PA ECDSA Key");
            using (ECDsaCng dsa = new ECDsaCng(cngKey))
            {
                dsa.HashAlgorithm = CngAlgorithm.MD5;
                byte[] signature = dsa.SignData(data);
                dsa.Clear();

                // Print signature
                Console.WriteLine("\nSignature:\n" + Debugger.BytesToString(signature));
                // Base64
                Console.WriteLine("\nSignature in Base64:\n" + CodeConverter.ToBase64(signature));

                // Big numbers
                Console.WriteLine("\nSignature in Codes:");
                ulong[] a = CodeConverter.ToInt64Array(signature);
                foreach (ulong i in a) Console.WriteLine(i.ToString());

                // Formated Base32
                Console.WriteLine("\nSignature in Formated Base32:\n" + CodeConverter.ToFormatedBase32Code(signature));

                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
        }
    }
}
