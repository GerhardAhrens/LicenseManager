namespace LicenseManager.Features
{
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;

    public static class ProtectedStorage
    {
        private static readonly byte[] Entropy;

        static ProtectedStorage()
        {
            string rootNamespace = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            Entropy = Encoding.UTF8.GetBytes($"{rootNamespace}-License-v1");
        }

        public static void Save(string fileName, string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] encrypted = ProtectedData.Protect(plainBytes, Entropy, DataProtectionScope.CurrentUser);

            File.WriteAllBytes(fileName, encrypted);
        }

        public static string Load(string fileName)
        {
            byte[] encrypted = File.ReadAllBytes(fileName);

            byte[] plainBytes = ProtectedData.Unprotect(encrypted, Entropy, DataProtectionScope.CurrentUser);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
