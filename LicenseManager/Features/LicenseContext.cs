//-----------------------------------------------------------------------
// <copyright file="LicenseContext.cs" company="Lifeprojects.de">
//     Class: LicenseContext
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>21.06.2026</date>
//
// <summary>
// Beinhaltet alle Daten zur Lizenzinformationen
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{

    public sealed class LicenseContext
    {
        public bool IsLicensed { get; init; }
        public bool SubscriptionValid { get; init; }
        public string SubscriptionId { get; set; }
        public LicenseState State { get; init; }
        public int RemainingTrialDays { get; init; }
        public string Customer { get; init; }
        public string Product { get; init; }
        public DateTime ExpiryDate { get; init; }
        public string Path { get; init; }
        public LicenseFeature Features { get; init; }
        public bool HasFeature(LicenseFeature feature)
        {
            return (Features & feature) == feature;
        }
    }
}
