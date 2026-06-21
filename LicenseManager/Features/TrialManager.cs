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
    using System.Globalization;

    public static class TrialManager
    {
        private static readonly string TrialFile =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "MyApplication.trial");

        public static bool IsTrialValid()
        {
            DateTime startDate;

            if (!File.Exists(TrialFile))
            {
                startDate = DateTime.Now;

                File.WriteAllText(
                    TrialFile,
                    startDate.ToString("O"));
            }
            else
            {
                startDate =
                    DateTime.Parse(
                        File.ReadAllText(TrialFile));
            }

            return DateTime.Now <
                   startDate.AddDays(30);
        }

        public static int RemainingDays()
        {
            if (!File.Exists(TrialFile))
                return 30;

            DateTime startDate =
                DateTime.Parse(
                    File.ReadAllText(TrialFile),CultureInfo.CurrentCulture);

            return Math.Max(
                0,
                (int)(startDate.AddDays(30) - DateTime.Now).TotalDays);
        }
    }
}
