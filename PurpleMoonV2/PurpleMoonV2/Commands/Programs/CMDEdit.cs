using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDEdit : Command
    {
        public CMDEdit()
        {
            this.Name = "EDIT";
            this.Help = "A fairly simple text editor";
            this.Usage = "Usage: edit | edit [filename]";
        }

        public override void Execute(string line, string[] args)
        {
            if (args.Length == 1)
            {
                Programs.DocEdit.Start();
                //Programs.TextEditor.Run();
            }
            else
            {
                if (args.Length > 1)
                {
                    string file = line.Substring(5, line.Length - 5);
                    string realFile = TryParseFile(file, true);

                    if (realFile != "*ERROR")
                    {
                        try
                        {
                            Programs.DocEdit.Start(realFile);
                            //if (!Programs.TextEditor.Load(realFile)) { CLI.WriteLine("Error loading file!"); }
                        }
                        catch (Exception ex) 
                        {
                            CLI.WriteLine("Unknown error while attemping to open file \"" + file + "\"", Color.Red);
                            CLI.Write("[INTERNAL] ", Color.Red); CLI.WriteLine(ex.Message, Color.White);
                        }
                    }
                    else { CLI.WriteLine("Could not locate file \"" + file + "\"", Color.Red); }
                }
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
