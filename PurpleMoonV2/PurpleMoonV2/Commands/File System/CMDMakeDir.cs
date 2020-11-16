using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDMakeDir : Command
    {
        public CMDMakeDir()
        {
            this.Name = "MKDIR";
            this.Help = "Creates a directory with a specified name";
        }

        public override void Execute(string line, string[] args)
        {
            bool success = false;
            string path = "";
            if (line.Length > 6)
            {
                path = line.Substring(6, line.Length - 6);
                if (path.EndsWith('\\')) { path = path.Remove(path.Length - 1, 1); }
                path += "\\";

                    if (path.StartsWith(PMFAT.CurrentDirectory)) { PMFAT.CreateFolder(path); success = true; }
                    else if (path.StartsWith(@"0:\")) { PMFAT.CreateFolder(path); success = true; }
                    else if (!path.StartsWith(PMFAT.CurrentDirectory) && !path.StartsWith(@"0:\"))
                    {
                        PMFAT.CreateFolder(PMFAT.CurrentDirectory + path);
                        success = true;
                    }
                else { CLI.WriteLine("Could not locate directory!", Color.Red); }
            }
            else { CLI.WriteLine("Invalid argument! Path expected.", Color.Red); }

            if (success) { CLI.WriteLine("Successfully created directory \"" + path + "\"", Color.Green); }
        }
    }
}
