//-----------------------------------------------------------------------
// <copyright file="LicenseGenerator.cs" company="Lifeprojects.de">
//     Class: LicenseGenerator
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>21.06.2026</date>
//
// <summary>
// Erstellt die notwendigen die Lizenzinformationen und speichert diese in license.lic ab.
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;

    public static class LicenseGenerator
    {
        public static void Generate(
            string customer,
            string product,
            MachineId machineId,
            DateTime expiryDate,
            LicenseFeature features,
            string subscriptionId,
            string privateKeyFile,
            string outputFile)
        {
            LicenseInfo license = new()
            {
                Customer = customer,
                Product = product,
                MachineId = machineId,
                ExpiryDate = expiryDate,
                Features = features,
                SubscriptionId = subscriptionId
            };

            string dataToSign = license.GetSignatureData();

            using RSA rsa = RSA.Create();

            rsa.ImportFromPem(File.ReadAllText(privateKeyFile));

            byte[] signature = rsa.SignData(Encoding.UTF8.GetBytes(dataToSign), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            license.Signature =  Convert.ToBase64String(signature);


            JsonSerializerOptions jsonSerializerOptions = new()
            {
                WriteIndented = true
            };

            JsonSerializerOptions options = jsonSerializerOptions;

            string json = JsonSerializer.Serialize(license, options: options);

            File.WriteAllText(outputFile, json);
        }
    }
}
