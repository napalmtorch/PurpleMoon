using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Core
{
    public static class SystemInfo
    {
        // operating system
        public static string OSName = "PurpleMoon OS";
        public static double OSVersion = 2.5;
        public static string KernelVersion = "20200708";

        // ram
        public static uint TotalRAM { get { return Cosmos.Core.CPU.GetAmountOfRAM(); } }

        // config
        public static string DefaultConfig =
        "back_col,0" + "\n" +
        "text_col,15" + "\n" +
        "titlebar_col,5" + "\n" +
        "title_col,13" + "\n" +
        "time_col,15" + "\n" +
        "titlebar_show,1" + "\n";

        // show system information
        public static void ShowInfo()
        {
            CLI.WriteLine(OSName, Color.Magenta);
            CLI.Write("version=" + OSVersion.ToString(), Color.White);
            CLI.WriteLine("    |    user kit " + KernelVersion, Color.Gray);
            ShowRAM();
            CLI.Write("\n");
        }

        // show total ram
        public static void ShowRAM() { CLI.WriteLine(TotalRAM.ToString() + "MB RAM"); }

        // load system configuration file
        public static void LoadConfig(string file, bool clear)
        {
            if (!PMFAT.FileExists(file)) { CLI.WriteLine("Could not locate configuration file!", Color.Red); }
            else
            {

                try
                {
                    string[] lines = PMFAT.ReadLines(file);

                    foreach (string line in lines)
                    {
                        string[] args = line.Split(',');

                        // load config attributes
                        if (args[0] == "back_col") { CLI.BackColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "text_col") { CLI.ForeColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "titlebar_col") { Shell.TitleBarColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "title_col") { Shell.TitleColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "time_col") { Shell.DateTimeColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "titlebar_show") { Shell.TitleBarVisible = DataUtils.IntToBool(int.Parse(args[1])); }
                    }

                    // reset screen
                    if (clear) { Shell.DrawFresh(); }
                    else { Shell.DrawTitleBar(); }
                }
                // unexpected error!
                catch (Exception ex)
                { 
                    CLI.WriteLine("Error loading system configuration file!", Color.Red);
                    CLI.Write("[INTERNAL] ", Color.Red); CLI.WriteLine(ex.Message);
                }
            }
        }

        public static void SaveConfig(string file)
        {
            string[] lines = new string[6];

            // write attributes
            lines[0] = "back_col," + ((int)(CLI.BackColor)).ToString();
            lines[1] = "text_col," + ((int)(CLI.ForeColor)).ToString();
            lines[2] = "titlebar_col," + ((int)(Shell.TitleBarColor)).ToString();
            lines[3] = "title_col," + ((int)(Shell.TitleColor)).ToString();
            lines[4] = "time_col," + ((int)(Shell.DateTimeColor)).ToString();
            lines[5] = "titlebar_show," + (Shell.TitleBarVisible ? "1" : "0");

            // attempt save
            try { PMFAT.WriteAllLines(file, lines); }
            // unexpected error
            catch (Exception ex)
            {
                CLI.WriteLine("Error occured while trying to save system configuration!", Color.Red);
                CLI.Write("[INTERNAL] ", Color.Red); CLI.WriteLine(ex.Message);
            }
        }
    }
}
