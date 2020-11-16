using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDSetDir : Command
    {
        public CMDSetDir()
        {
            this.Name = "CD";
            this.Help = "Set the active directory";
        }

        public override void Execute(string line, string[] args)
        {
            if (args.Length == 1) { PMFAT.CurrentDirectory = @"0:\"; }
            else
            {
                if (line.Length > 3)
                {
                    string path = line.Substring(3, line.Length - 3).ToLower();
                    if (path.EndsWith('\\')) { path = path.Remove(path.Length - 1, 1); }
                    path += "\\";

                    if (PMFAT.FolderExists(path))
                    {
                        if (path.StartsWith(PMFAT.CurrentDirectory)) { PMFAT.CurrentDirectory = path; }
                        else if (path.StartsWith(@"0:\")) { PMFAT.CurrentDirectory = path; }
                        else if (!path.StartsWith(PMFAT.CurrentDirectory) && !path.StartsWith(@"0:\"))
                        {
                            if (PMFAT.FolderExists(PMFAT.CurrentDirectory + path)) { PMFAT.CurrentDirectory = PMFAT.CurrentDirectory + path; }
                            else { CLI.WriteLine("Could not locate directory \"" + path + "\"", Color.Red); }
                        }
                    }
                    else { CLI.WriteLine("Could not locate directory!", Color.Red); }
                }
                else { CLI.WriteLine("Invalid argument! Path expected.", Color.Red); }
            }
        }
    }
}
