using System;
using System.Security.Cryptography;
using System.Text;
using Recipes.Domain;

namespace Recipes.Persistence
{
    public class Sha256HashGenerator : IHashGenerator
    {
        public string GenerateHashFor(string input)
        {
            using (SHA256 mySha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var inputHash = mySha256.ComputeHash(inputBytes);
                return Convert.ToBase64String(inputHash);
            }
        }
    }
}