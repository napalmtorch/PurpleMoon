using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDDelFile : Command
    {
        public CMDDelFile()
        {
            this.Name = "DEL";
            this.Help = "Deletes a file with a specified name";
        }

        public override void Execute(string line, string[] args)
        {
            bool success = false;
            string realFile = "";
            if (line.Length > 4)
            {
                string path = line.Substring(4, line.Length - 4);

                realFile = TryParseFile(path);
                if (realFile != "*ERROR") { PMFAT.DeleteFile(realFile); success = true; }
                else { CLI.WriteLine("Could not locate file \"" + realFile + "\"", Color.Red); success = false; }

            }
            else { CLI.WriteLine("Invalid argument! File expected.", Color.Red); }

            if (success) { CLI.WriteLine("Successfully deleted file \"" + realFile + "\"", Color.Green); }
        }

        private static string TryParseFile(string file)
        {
            string realFile = file;
            if (file.StartsWith(PMFAT.CurrentDirectory)) { realFile = file; }
            else if (file.StartsWith(@"0:\")) { realFile = file; }
            else if (!file.StartsWith(PMFAT.CurrentDirectory) && !file.StartsWith(@"0:\")) { realFile = PMFAT.CurrentDirectory + file; }
            if (PMFAT.FileExists(realFile)) { return realFile; }
            else { return "*ERROR"; }
        }
    }
}
