//-----------------------------------------------------------------------
// <copyright file="LicenseFeature.cs" company="Lifeprojects.de">
//     Class: LicenseFeature
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>21.06.2026</date>
//
// <summary>
// Beinhaltet die Features Informationen zu einer Lizenz
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{
    [Flags]
    public enum LicenseFeature
    {
        None = 0,

        CustomerManagement = 1,

        InvoiceManagement = 2,

        Export = 4,

        ApiAccess = 8,

        All =
            CustomerManagement |
            InvoiceManagement |
            Export |
            ApiAccess
    }
}
