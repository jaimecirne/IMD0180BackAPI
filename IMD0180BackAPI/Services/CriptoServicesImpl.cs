namespace IMD0180BackAPI.Services
{
    using System.Security.Cryptography;
    using System.Text;

    public class CriptoServicesImpl : ICriptoServices
    {
        public string Hash(string original, string salt)
        {
            return HashASCIISHA256(salt + original);
        }

        private string HashASCIISHA256(string original)
        {
            var bytes = new System.Text.ASCIIEncoding().GetBytes(original);
            var sha = new SHA256Managed();
            var hash = sha.ComputeHash(bytes);
            return ToHexString(hash);
        }

        private string ToHexString(byte[] bytes)
        {
            var hex = new StringBuilder();
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }
    }
}