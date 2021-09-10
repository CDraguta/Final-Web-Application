using System;
using System.Text;

namespace WebApplication1
{
    public static class Crypto
    {
        public static string  Hash(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }
    }
}