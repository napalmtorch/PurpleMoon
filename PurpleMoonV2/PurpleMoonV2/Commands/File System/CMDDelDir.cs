using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDDelDir : Command
    {
        public CMDDelDir()
        {
            this.Name = "DELDIR";
            this.Help = "Deletes a directory with a specified name";
        }

        public override void Execute(string line, string[] args)
        {
            CLI.Write("[WARNING] ", Color.DarkYellow);
            CLI.WriteLine("This command is a WIP and may corrupt data, continue? Y = YES, N = NO", Color.White);
            string input = CLI.ReadLine();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                bool success = false;
                string realPath;
                if (args.Length > 1)
                {
                    string path = line.Substring(7, line.Length - 7);
                    if (path.EndsWith('\\')) { path = path.Remove(path.Length - 1, 1); }
                    //path += "\\";

                    realPath = TryParseFolder(path, true);

                    if (realPath != "*ERROR") { success = true; }
                    else { success = false; }

                    if (success)
                    {
                        if (PMFAT.DeleteFolder(realPath))
                        {
                            CLI.WriteLine("Successfully deleted directory \"" + realPath + "\"", Color.Green);
                            if (realPath == PMFAT.CurrentDirectory) { PMFAT.CurrentDirectory = @"0:\"; }
                        }
                        else
                        {
                            CLI.WriteLine("Could not delete directory \"" + realPath + "\"", Color.Red);
                        }
                    }
                    CLI.WriteLine("Error attempting to delete directory!", Color.Red);
                }
                else { CLI.WriteLine("Argument expected!", Color.Red); }
            }
            else { CLI.WriteLine("Operation aborted."); }
        }

        private static string TryParseFolder(string path, bool exists)
        {
            string realPath = path;
            if (path.StartsWith(PMFAT.CurrentDirectory)) { realPath = path; }
            else if (path.StartsWith(@"0:\")) { realPath = path; }
            else if (!path.StartsWith(PMFAT.CurrentDirectory) && !path.StartsWith(@"0:\")) { realPath = PMFAT.CurrentDirectory + path; }
            if (exists)
            {
                if (PMFAT.FolderExists(realPath)) { return realPath; }
                else { return "*ERROR"; }
            }
            else { return realPath; }
        }
    }
}
