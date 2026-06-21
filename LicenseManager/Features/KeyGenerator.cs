//-----------------------------------------------------------------------
// <copyright file="LicenseInfo.cs" company="Lifeprojects.de">
//     Class: LicenseInfo
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>21.06.2026</date>
//
// <summary>
// Erstellt die beiden Schlüssel für Private und Public
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{
    using System.Security.Cryptography;

    public class KeyGenerator
    {
        public static void Create()
        {
            using RSA rsa = RSA.Create(2048);

            File.WriteAllText("private.key", rsa.ExportRSAPrivateKeyPem());

            File.WriteAllText("public.key", rsa.ExportRSAPublicKeyPem());
        }
    }
}
