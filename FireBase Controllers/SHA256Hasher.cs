using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
public class SHA256Hasher
    {
        public static string ComputeHash(string input)
        {
            byte[] hashBytes = SHA256.Create("SHA256").ComputeHash(Encoding.UTF8.GetBytes(input));

            string hashResult = "";

            foreach (var x in hashBytes)
            {
                hashResult += string.Format("{0:x2}", x);
            }

            return hashResult;
        }
    }

