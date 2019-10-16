using System;
using System.Text;
using System.Security.Cryptography;

namespace CoreKit.Cryptography.PBKDF2
{

    internal class PBKDF2KitOriginal
    {
        public PBKDF2KitOriginal()
        {
            HashIterations = 100000;
            SaltSize = 34;
        }
        private int HashIterations { get; set; }
        private int SaltSize { get; set; }
        private string PlainText { get; set; }
        private string HashedText { get; set; }
        private string Salt { get; set; }
        public string Compute()
        {
            if (string.IsNullOrEmpty(PlainText)) throw new InvalidOperationException("PlainText cannot be empty");
            if (string.IsNullOrEmpty(Salt))
            {
                GenerateSalt();
            }
            HashedText = calculateHash(HashIterations);
            return HashedText;
        }
        public string Compute(string textToHash)
        {
            PlainText = textToHash;
            Compute();
            return HashedText;
        }
        public string Compute(string textToHash, int saltSize, int hashIterations)
        {
            PlainText = textToHash;
            GenerateSalt(hashIterations, saltSize);
            Compute();
            return HashedText;
        }
        public string Compute(string textToHash, string salt)
        {
            PlainText = textToHash;
            Salt = salt;
            expandSalt();
            Compute();
            return HashedText;
        }
        public string GenerateSalt()
        {
            if (SaltSize < 1)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Cannot generate a salt of size {0}, use a value greater than 1, recommended: 16",
                        SaltSize
                    )
                );
            }
            var rand = RandomNumberGenerator.Create();
            var ret = new byte[SaltSize];
            rand.GetBytes(ret);
            Salt = string.Format("{0}.{1}", HashIterations, Convert.ToBase64String(ret));
            return Salt;
        }
        public string GenerateSalt(int hashIterations, int saltSize)
        {
            HashIterations = hashIterations;
            SaltSize = saltSize;
            return GenerateSalt();
        }
        private string calculateHash(int iteration)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(Salt);
            var pbkdf2 = new Rfc2898DeriveBytes(PlainText, saltBytes, iteration);
            var key = pbkdf2.GetBytes(64);
            return Convert.ToBase64String(key);
        }
        private void expandSalt()
        {
            try
            {
                var i = Salt.IndexOf('.');
                HashIterations = int.Parse(Salt.Substring(0, i), System.Globalization.NumberStyles.Number);
            }
            catch (Exception)
            {
                throw new FormatException("The salt was not in an expected format of {int}.{string}");
            }
        }
        public bool Compare(string passwordHash1, string passwordHash2)
        {
            if (passwordHash1 == null || passwordHash2 == null)
            {
                return false;
            }
            int min_length = Math.Min(passwordHash1.Length, passwordHash2.Length);
            int result = 0;
            for (int i = 0; i < min_length; i++)
            {
                result |= passwordHash1[i] ^ passwordHash2[i];
            }
            return 0 == result;
        }

    }

}
