using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_Activation
{
    class CodeConverter
    {
        public static byte[] FromBase64(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        public static byte[] FromInt64Array(ulong[] codeList)
        {
            byte[] a = new byte[codeList.Length * 8];
            for (int i = 0; i < codeList.Length; i++)
            {
                BitConverter.GetBytes(codeList[i]).CopyTo(a, i * 8);
            }
            return a;
        }

        public static byte[] FromFormattedCode(string formatedBase32String)
        {
            string base32String = formatedBase32String.Replace("-", String.Empty);
            return Base32Encoding.ToBytes(base32String);
        }

        public static string ToBase64(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static ulong[] ToInt64Array(byte[] data)
        {
            ulong[] a = new ulong[data.Length / 8];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = BitConverter.ToUInt64(data, i * 8);
            }
            return a;
        }

        public static string ToFormatedBase32Code(byte[] data)
        {
            string s = Base32Encoding.ToString(data);
            string formatted = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (i != 0 && i % 5 == 0)
                {
                    formatted += '-';
                }
                formatted += s[i];
            }
            return formatted;
        }
    }

    class Debugger
    {
        public static string BytesToString(byte[] Bytes)
        {
            string s = "";
            for (int i = 0; i < Bytes.Length; i++)
            {
                s += Bytes[i].ToString("x2");
            }
            return s;
        }
    }
}
