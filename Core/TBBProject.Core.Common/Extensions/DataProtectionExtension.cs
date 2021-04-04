using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace TBBProject.Core.Common.Extensions
{
    public class EncryptionService
    {
        private readonly KeyInfo _keyInfo;

        public EncryptionService(KeyInfo keyInfo = null)
        {
            _keyInfo = keyInfo;
        }

        public string Encrypt(string input)
        {
            var enc = EncryptStringToBytes_Aes(input, _keyInfo.Key, _keyInfo.Iv);
            return Convert.ToBase64String(enc);
        }

        public string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            return DecryptStringFromBytes_Aes(cipherBytes, _keyInfo.Key, _keyInfo.Iv);
        }
        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            string plaintext;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }
        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));
            byte[] encrypted;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }
        // EncryptStringToBytes_Aes and DecryptStringFromBytes_Aes above ...
    }
    public class KeyInfo
    {
        public byte[] Key { get; }
        public byte[] Iv { get; }

        public string KeyString => Convert.ToBase64String(Key);
        public string IVString => Convert.ToBase64String(Iv);

        public KeyInfo()
        {
            using (var myAes = Aes.Create())
            {
                Key = myAes.Key;
                Iv = myAes.IV;
            }
        }

        public KeyInfo(string key, string iv)
        {
            Key = Convert.FromBase64String(key);
            Iv = Convert.FromBase64String(iv);
        }

        public KeyInfo(byte[] key, byte[] iv)
        {
            Key = key;
            Iv = iv;
        }
    }

}
