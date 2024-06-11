using System;
using System.ComponentModel.Design;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;

namespace Unix
{
    public class BepinexInstaller
    {
        public static string GetPathToInstall()
        {
            string result = "";
            if (Environment.SystemDirectory.Contains(@"C:\"))
            {
                if (Directory.Exists(@"C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins"))
                    result = @"C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins";
                else
                    result = @"C:\Program Files\Oculus\Software\Software\another-axiom-gorilla-tag\BepInEx\plugins";
            }
            else
            {
                if (Directory.Exists(@"D:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins"))
                    result = @"D:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins";
                else
                    result = @"D:\Program Files\Oculus\Software\Software\another-axiom-gorilla-tag\BepInEx\plugins";
            }
            if (Environment.SystemDirectory.Contains(@"E:\"))
            {
                if (Directory.Exists(@"E:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins"))
                    result = @"E:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins";
                else
                    result = @"E:\Program Files\Oculus\Software\Software\another-axiom-gorilla-tag\BepInEx\plugins";
            }
            return result;
        }

        public static string GetBasePathToInstall()
        {
            string result = "";
            if (Environment.SystemDirectory.Contains(@"C:\"))
            {
                if (Directory.Exists(@"C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag"))
                    result = @"C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag";
                else
                    result = @"C:\Program Files\Oculus\Software\Software\another-axiom-gorilla-tag";
            }
            else
            {
                if (Directory.Exists(@"D:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag"))
                    result = @"D:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag";
                else
                    result = @"D:\Program Files\Oculus\Software\Software\another-axiom-gorilla-tag";
            }
            return result;
        }

        static bool alreadyInstalledBepInEx = false;
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] Install BepInEx");
            Console.WriteLine("[2] Exit");
            Console.WriteLine("[3] Download My Menu (Incharilla Menu)");
            Console.Write("> ");
            var opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    #region Backing up BepInEx Files
                    if (Directory.Exists(GetBasePathToInstall() + @"\BepInEx"))
                    {
                        string src = $@"{GetPathToInstall()}";
                        string backup = AppDomain.CurrentDomain.BaseDirectory + @"\Backup";
                        Directory.CreateDirectory(backup);
                        string[] f = Directory.GetFiles(src);
                        foreach (string str in f)
                        {
                            string fn = Path.GetFileName(str);
                            string tg = Path.Combine(backup, fn);
                            File.Copy(str, tg, true);
                        }
                        Directory.Delete(GetBasePathToInstall() + @"\BepInEx", true);
                        File.Delete(GetBasePathToInstall() + @"\.doorstop_version");
                        File.Delete(GetBasePathToInstall() + @"\changelog.txt");
                        File.Delete(GetBasePathToInstall() + @"\doorstop_config.ini");
                        File.Delete(GetBasePathToInstall() + @"\winhttp.dll");
                        alreadyInstalledBepInEx = true;
                    }
                    #endregion
                    #region Downloading and Extracting BepInEx Files
                    string bepinex = "https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.1/BepInEx_win_x64_5.4.23.1.zip";
                    WebClient client = new WebClient();
                    client.DownloadFile(bepinex, "BepInEx_win_x64_5.4.23.1.zip");
                    ZipFile.ExtractToDirectory("BepInEx_win_x64_5.4.23.1.zip", GetBasePathToInstall());
                    File.Delete($@"{AppDomain.CurrentDomain.BaseDirectory}\BepInEx_win_x64_5.4.23.1.zip");
                    Directory.CreateDirectory($@"{GetPathToInstall()}");
                    #endregion
                    #region Restoring Files
                    if (alreadyInstalledBepInEx)
                    {
                        string src = $@"{GetPathToInstall()}";
                        string backup = AppDomain.CurrentDomain.BaseDirectory + @"\Backup";
                        string[] f = Directory.GetFiles(backup);
                        foreach (string str in f)
                        {
                            string fn = Path.GetFileName(str);
                            string tg = Path.Combine(src, fn);
                            File.Copy(str, tg, true);
                        }
                    }
                    #endregion
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
                case "3":
                    #region Downloading Incharilla Menu
                    string menuUrl = "https://cdn.discordapp.com/attachments/1249514439229571143/1250184270195068939/IncharillaMenu.dll?ex=666a0455&is=6668b2d5&hm=85d2c5ed08696c3b71ec31f20a5fb4d70da687dc5fc4ba00321d8b0b43bf93ae&";
                    string menuPath = Path.Combine(GetPathToInstall(), "IncharillaMenu.dll");
                    WebClient menuClient = new WebClient();
                    menuClient.DownloadFile(menuUrl, menuPath);
                    Console.WriteLine("Incharilla Menu downloaded successfully.");
                    #endregion
                    break;
            }
        }
    }
}
