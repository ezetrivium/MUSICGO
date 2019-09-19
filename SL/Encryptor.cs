﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SL
{
    public class Encryptor
    {
        public string Encrypt(string stringToHash)
        {
            using (var md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public bool CheckEncryption(string stringOrigin, string stringToCompare)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(stringOrigin, stringToCompare))
                return true;

            return false;
        }
    }
}
