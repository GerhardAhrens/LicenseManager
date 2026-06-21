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
// Gibt eine PC/Notebook Maschinen ID zurück. Eine Kombination aus MachineName und UserDomainName.
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{
    using System.Security.Cryptography;
    using System.Text;

    public record MachineId(string value);

    public static class MachineIdProvider
    {
        public static MachineId GetMachineId()
        {
            string source = $"{Environment.MachineName}{Environment.UserDomainName}";

            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(source));

            return new MachineId(Convert.ToHexString(hash));
        }
    }
}
