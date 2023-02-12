using System.Security.Cryptography;
using System.Text;

namespace SelfCare.Application.Helpers
{
    public static class Cypher
    {
        const string SALT = "Hello there, I'm a Cypher Salt!";

        public static string GenerateSaltedHash(string input)
        {
            var salt = Encoding.ASCII.GetBytes(SALT);
            var plainText = Encoding.ASCII.GetBytes(input);

            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + SALT.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < SALT.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return Encoding.ASCII.GetString(algorithm.ComputeHash(plainTextWithSaltBytes));
        }

        public static bool ByteComparison(string input1, string input2)
        {
            var array1 = Encoding.ASCII.GetBytes(input1);
            var array2 = Encoding.ASCII.GetBytes(input2);

            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
