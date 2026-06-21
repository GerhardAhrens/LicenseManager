//-----------------------------------------------------------------------
// <copyright file="LicenseProfiles.cs" company="Lifeprojects.de">
//     Class: LicenseProfiles
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>21.06.2026</date>
//
// <summary>
// Zusammenstellung von Lizenzprofilen
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{
    public sealed class LicenseProfiles
    {
        public const LicenseFeature Trial =
                LicenseFeature.CustomerManagement;

        public const LicenseFeature Basic =
            LicenseFeature.CustomerManagement |
            LicenseFeature.InvoiceManagement;

        public const LicenseFeature Professional =
            LicenseFeature.CustomerManagement |
            LicenseFeature.InvoiceManagement |
            LicenseFeature.Export;

        public const LicenseFeature Enterprise =
            LicenseFeature.All;
    }
}
