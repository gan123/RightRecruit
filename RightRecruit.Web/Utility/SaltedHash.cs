using System;
using System.Security.Cryptography;

namespace RightRecruit.Web.Utility
{
    public sealed class SaltedHash
    {
        public string Salt { get { return _salt; } }
        public string Hash { get { return _hash; } }

        public static SaltedHash Create(string password)
        {
            var salt = CreateSalt();
            var hash = CalculateHash(salt, password);
            return new SaltedHash(salt, hash);
        }

        public static SaltedHash Create(string salt, string hash)
        {
            return new SaltedHash(salt, hash);
        }

        public bool Verify(string password)
        {
            var h = CalculateHash(_salt, password);
            return _hash.Equals(h);
        }

        private SaltedHash(string s, string h)
        {
            _salt = s;
            _hash = h;
        }

        private static string CreateSalt()
        {
            var r = CreateRandomBytes(SaltLength);
            return Convert.ToBase64String(r);
        }

        private static byte[] CreateRandomBytes(int len)
        {
            var r = new byte[len];
            new RNGCryptoServiceProvider().GetBytes(r);
            return r;
        }

        private static string CalculateHash(string salt, string password)
        {
            var data = ToByteArray(salt + password);
            var hash = CalculateHash(data);
            return Convert.ToBase64String(hash);
        }

        private static byte[] CalculateHash(byte[] data)
        {
            return new SHA1CryptoServiceProvider().ComputeHash(data);
        }

        private static byte[] ToByteArray(string s)
        {
            return System.Text.Encoding.UTF8.GetBytes(s);
        }

        private readonly string _salt;
        private readonly string _hash;
        private const int SaltLength = 6;
    }
}