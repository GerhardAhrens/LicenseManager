//-----------------------------------------------------------------------
// <copyright file="TrialInfo.cs" company="Lifeprojects.de">
//     Class: TrialInfo
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>21.06.2026</date>
//
// <summary>
// Beinhaltet die Trail informationen zur Demo Version)
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{
    public sealed class TrialInfo
    {
        public DateTime FirstStartDate { get; set; }
        public int TrialDays { get; set; }
        public DateTime ExpiryDate => FirstStartDate.AddDays(TrialDays);
        public bool IsExpired => DateTime.UtcNow > FirstStartDate.AddDays(TrialDays);
        public LicenseFeature Features { get; set; }

        public int RemainingDays
        {
            get
            {
                var days = (int)(FirstStartDate.AddDays(TrialDays) - DateTime.UtcNow).TotalDays;

                return Math.Max(0, days);
            }
        }
    }
}
