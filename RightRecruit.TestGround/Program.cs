using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RightRecruit.Mvc.Infrastructure.Utility;

namespace RightRecruit.TestGround
{
    class Program
    {
        static void Main(string[] args)
        {
            var pwd = "complex@123";
            string salt;
            string hash;
            new SaltedHash().GetHashAndSaltString(pwd, out hash, out salt);
            var saltBytes = salt.ToByteArray();
            var hashBytes = hash.ToByteArray();
            Console.WriteLine(pwd.ToByteArray() + " " + saltBytes + " " + hashBytes);
            Console.WriteLine(saltBytes.ToPlainString() + " " +  hashBytes.ToPlainString());

            var repwd = "complex@123";
            Console.WriteLine(new SaltedHash().VerifyHashString(repwd, hashBytes.ToPlainString(), saltBytes.ToPlainString()));
            Console.ReadLine();
        }
    }
}
