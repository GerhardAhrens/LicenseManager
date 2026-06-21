//-----------------------------------------------------------------------
// <copyright file="LicenseManager.cs" company="Lifeprojects.de">
//     Class: LicenseManager
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>21.06.2026</date>
//
// <summary>
// Lesen und verifizieren einer Lizenzdatei license.lic
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;

    public static class LicenseManager
    {
        private const string LicenseFile = "license.lic";

        private const string PublicKeyFile = "public.key";

        private static LicenseInfo _license;

        private static LicenseContext _context;

        public static void Initialize()
        {
            _license = LoadLicense();

            _context = BuildContext();
        }

        public static LicenseInfo GetLicense()
        {
            if (_license == null)
            {
                throw new InvalidOperationException("Keine Lizenz geladen.");
            }

            return _license;
        }

        public static LicenseContext GetContext()
        {
            if (_context == null)
            {
                throw new InvalidOperationException("LicenseManager.Initialize() wurde nicht aufgerufen.");
            }

            return _context;
        }

        public static bool HasFeature(LicenseFeature feature)
        {
            return GetContext().HasFeature(feature);
        }

        private static LicenseInfo LoadLicense()
        {
            if (!File.Exists(LicenseFile))
                return null;

            try
            {
                string json = File.ReadAllText(LicenseFile);

                return JsonSerializer.Deserialize<LicenseInfo>(json);
            }
            catch
            {
                return null;
            }
        }

        private static LicenseContext BuildContext()
        {
            if (_license == null)
            {
                TrialInfo trial = TrialManager.GetTrial();

                return new LicenseContext
                {
                    State = trial.IsExpired ? LicenseState.TrialExpired : LicenseState.Trial,
                    RemainingTrialDays = trial.RemainingDays,
                    IsLicensed = false,
                    ExpiryDate = trial.ExpiryDate,
                    Features = trial.Features,
                    SubscriptionValid = false
                };
            }

            if (!VerifySignature(_license))
            {
                return new LicenseContext
                {
                    State = LicenseState.InvalidLicense,
                    IsLicensed = false,
                    SubscriptionValid = false
                };
            }

            if (!VerifyMachine(_license))
            {
                return new LicenseContext
                {
                    State = LicenseState.InvalidLicense,
                    IsLicensed = false,
                    SubscriptionValid = false
                };
            }

            bool subscriptionValid = _license.ExpiryDate >= DateTime.UtcNow;

            return new LicenseContext
            {
                Customer = _license.Customer,
                Product = _license.Product,
                ExpiryDate = _license.ExpiryDate,
                Features = _license.Features,
                IsLicensed = true,
                SubscriptionValid = subscriptionValid,
                SubscriptionId = _license.SubscriptionId,
                State = subscriptionValid
                    ? LicenseState.Full
                    : LicenseState.SubscriptionExpired
            };
        }

        private static bool VerifyMachine(
            LicenseInfo license)
        {
            return license.MachineId.value == MachineIdProvider.GetMachineId().value;
        }

        private static bool VerifySignature(LicenseInfo license)
        {
            if (!File.Exists(PublicKeyFile))
            {
                return false;
            }

            string publicKey = File.ReadAllText(PublicKeyFile);

            using RSA rsa = RSA.Create();

            rsa.ImportFromPem(publicKey);

            string data = license.GetSignatureData();

            return rsa.VerifyData(Encoding.UTF8.GetBytes(data), Convert.FromBase64String(license.Signature), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}
