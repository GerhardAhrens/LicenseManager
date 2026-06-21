//-----------------------------------------------------------------------
// <copyright file="TrialManager.cs" company="Lifeprojects.de">
//     Class: TrialManager
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>21.06.2026</date>
//
// <summary>
// Beinhaltet die Verwaltung einer Trail Version
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager.Features
{
    using System.Globalization;
    using System.Reflection;
    using System.Text.Json;

    public static class TrialManager
    {
        private static readonly string TrialFile;

        private const int TrialDays = 30;

        static TrialManager()
        {
            var rootNamespace = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);

            TrialFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), rootNamespace, "trial.dat");
        }

        public static TrialInfo GetTrial()
        {
            if (File.Exists(TrialFile) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(TrialFile)!);

                TrialInfo info = new()
                {
                    FirstStartDate = DateTime.UtcNow,
                    TrialDays = TrialDays,
                    Features = LicenseFeature.All
                };

                Save(info);

                return info;
            }

            string trailFileContent = ProtectedStorage.Load(TrialFile);
            return JsonSerializer.Deserialize<TrialInfo>(trailFileContent) ?? throw new InvalidOperationException();
        }

        private static void Save(TrialInfo info)
        {
            JsonSerializerOptions jsonSerializerOptions = new()
            {
                WriteIndented = true
            };

            JsonSerializerOptions options = jsonSerializerOptions;

            string json = JsonSerializer.Serialize(info, options);
            ProtectedStorage.Save(TrialFile, json);
        }
    }
}
