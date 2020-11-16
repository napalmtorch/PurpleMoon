using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDCopyFile : Command
    {
        public CMDCopyFile()
        {
            this.Name = "COPY";
            this.Help = "Copies a file from one location to another";
        }

        public override void Execute(string line, string[] args)
        {
            bool success = false;
            string src = "", dest = "";
            if (args.Length == 3)
            {
                src = TryParseFile(args[1], true);
                dest = TryParseFile(args[2], false);

                if (src != "*ERROR" && dest != "*ERROR") { success = true; }
                else { success = false; }
            }
            else { CLI.WriteLine("Invalid argument! Path expected.", Color.Red); }

            if (success)
            {
                PMFAT.CopyFile(src, dest);
                CLI.WriteLine("Successfully copied file \"" + src + "\" to \"" + dest + "\"", Color.Green); 
            }
            else
            {
                CLI.WriteLine("Error copying file \"" + src + "\" to \"" + dest + "\"", Color.Red);
            }
        }

        private static string TryParseFile(string file, bool exists)
        {
            string realFile = file;
            if (file.StartsWith(PMFAT.CurrentDirectory)) { realFile = file; }
            else if (file.StartsWith(@"0:\")) { realFile = file; }
            else if (!file.StartsWith(PMFAT.CurrentDirectory) && !file.StartsWith(@"0:\")) { realFile = PMFAT.CurrentDirectory + file; }
            if (exists)
            {
                if (PMFAT.FileExists(realFile)) { return realFile; }
                else { return "*ERROR"; }
            }
            else { return realFile; }
        }
    }
}
