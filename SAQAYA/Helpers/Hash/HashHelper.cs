using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace SAQAYA.Helpers.Hash
{
    public static class HashHelper
    {
        /// <summary>
        /// Create a data hash
        /// </summary>
        /// <param name="data">The data for calculating the hash</param>
        /// <param name="hashAlgorithm">Hash algorithm</param>
        /// <param name="trimByteCount">The number of bytes, which will be used in the hash algorithm; leave 0 to use all array</param>
        /// <returns>Data hash</returns>
        public static string CreateHash(string password, string saltkey, string hashAlgorithm)
        {
            var data = Encoding.UTF8.GetBytes(string.Concat(password, saltkey));
            if (string.IsNullOrEmpty(hashAlgorithm))
                throw new ArgumentNullException(nameof(hashAlgorithm));

            var algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(hashAlgorithm);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            return BitConverter.ToString(algorithm.ComputeHash(data)).Replace("-", string.Empty);
        }
    }
}
