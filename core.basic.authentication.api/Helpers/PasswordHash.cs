

using System.Security.Cryptography;

namespace core.basic.authentication.api.Infrastructures
{
    public sealed class PasswordHash
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 1000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
            return $"{Convert.ToHexString(salt)}-{Convert.ToHexString(hash)}";
        }

        public static bool Verify(string password, string passwordHash)
        {
            var parts = passwordHash.Split('-');
            var salt = Convert.FromHexString(parts[0]);
            var hash = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
            //return hash.SequenceEqual(inputHash);
            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
