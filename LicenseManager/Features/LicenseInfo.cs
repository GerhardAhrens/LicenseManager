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
// Beinhaltet die Lizenzinformationen
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{
    public sealed class LicenseInfo
    {
        public string Customer { get; set; } = "";
        public string Product { get; set; } = "";
        public MachineId MachineId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SubscriptionId { get; set; } = "";
        public LicenseFeature Features { get; set; }
        public string Signature { get; set; } = "";

        public string GetSignatureData()
        {
            return $"{this.Customer}|{this.Product}|{this.MachineId.value}|{this.ExpiryDate:O}|{(int)this.Features}|{this.SubscriptionId}";
        }
    }
}
