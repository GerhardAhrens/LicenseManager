//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Lifeprojects.de">
//     Class: Program
//     Copyright © Lifeprojects.de 2026
// </copyright>
// <Template>
// 	Version 3.0.2026.2, 15.04.2026
// </Template>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>21.06.2026 08:59:46</date>
//
// <summary>
// Konsolen Applikation mit Menü
// </summary>
//-----------------------------------------------------------------------

namespace LicenseManager
{
    /* Imports from NET Framework */
    using System;
    using System.ComponentModel;

    using LicenseManager.Features;

    public class Program
    {
        public Program()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
        }

        private static void Main(string[] args)
        {
            CMenu mainMenu = new CMenu("Lizenzverwaltung");
            mainMenu.AddItem("Erstelle LizenzKey", MenuPoint1);
            mainMenu.AddItem("Erstelle und prüfen Lizenzdatei BASIC", MenuPoint2);
            mainMenu.AddItem("Erstelle und prüfen Lizenzdatei ENTERPRISE", MenuPoint3);
            mainMenu.AddItem("Beenden", () => ApplicationExit());
            mainMenu.Show();
        }

        private static void ApplicationExit()
        {
            Environment.Exit(0);
        }

        private static void MenuPoint1()
        {
            Console.Clear();
            KeyGenerator.Create();

            Console.Wait();
        }

        private static void MenuPoint2()
        {
            Console.Clear();

            MachineId machineId = MachineIdProvider.GetMachineId();
            LicenseGenerator.Generate("Muster GmbH", "ERP", machineId, expiryDate: DateTime.Now.AddYears(1), LicenseProfiles.Basic, subscriptionId: "SUB-2026-0001", privateKeyFile: "private.key", outputFile: "license.lic");

            Features.LicenseManager.Initialize();
            Features.LicenseContext license = Features.LicenseManager.GetContext();

            var resultDump = Dump.Get(license);

            foreach (var (name, type, value) in resultDump)
            {
                Console.WriteText($"{name} [{type.Name}] = {value}");
            }

            Console.Wait();
        }

        private static void MenuPoint3()
        {
            Console.Clear();
            MachineId machineId = MachineIdProvider.GetMachineId();
            LicenseGenerator.Generate("Muster GmbH", "ERP", machineId, expiryDate: DateTime.Now.AddYears(1), LicenseProfiles.Enterprise, subscriptionId: "SUB-2026-0001", privateKeyFile: "private.key", outputFile: "license.lic");

            Features.LicenseManager.Initialize();
            Features.LicenseContext license = Features.LicenseManager.GetContext();

            var resultDump = Dump.Get(license);

            foreach (var (name, type, value) in resultDump)
            {
                Console.WriteText($"{name} [{type.Name}] = {value}");
            }

            Console.Wait();
        }
    }
}
