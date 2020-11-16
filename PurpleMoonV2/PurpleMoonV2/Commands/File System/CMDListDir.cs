using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDListDir : Command
    {
        public CMDListDir()
        {
            this.Name = "DIR";
            this.Help = "List contents of specified directory";
        }

        public override void Execute(string line, string[] args)
        {
            // show contents of active directory
            if (args.Length == 1)
            {
                ListContents(PMFAT.CurrentDirectory);
            }
            // show contents of specified directory
            else if (line.Length > 4)
            {
                // parse path
                string path = line.Substring(4, line.Length - 4).ToLower();
                if (path.EndsWith('\\')) { path = path.Remove(path.Length - 1, 1); }
                path += "\\";

                if (PMFAT.FolderExists(path))
                {
                    if (path.StartsWith(PMFAT.CurrentDirectory)) { ListContents(path); }
                    else if (path.StartsWith(@"0:\")) { ListContents(path); }
                    else if (!path.StartsWith(PMFAT.CurrentDirectory) && !path.StartsWith(@"0:\"))
                    {
                        if (PMFAT.FolderExists(PMFAT.CurrentDirectory + path)) ListContents(PMFAT.CurrentDirectory + path);
                        else { CLI.WriteLine("Could not locate directory \"" + path + "\"", Color.Red); }
                    }
                    else { CLI.WriteLine("Could not locate directory \"" + path + "\"", Color.Red); }
                }
                else { CLI.WriteLine("Could not locate directory!", Color.Red); }
            }
            else { CLI.WriteLine("Invalid argument! Path expected.", Color.Red); }
        }

        private void ListContents(string path)
        {
            try
            {
                string[] folders = PMFAT.GetFolders(path);
                string[] files = PMFAT.GetFiles(path);

                CLI.WriteLine("Showing contents of directory \"" + path + "\"");

                // draw folders
                for (int i = 0; i < folders.Length; i++)
                {
                    CLI.WriteLine(folders[i], Color.Yellow);
                }

                // draw files
                for (int i = 0; i < files.Length; i++)
                {
                    Cosmos.System.FileSystem.Listing.DirectoryEntry attr = PMFAT.GetFileInfo(path + files[i]);
                    if (attr != null)
                    {
                        CLI.Write(files[i], Color.White);
                        CLI.SetCursorPos(30, CLI.CursorY);
                        CLI.WriteLine(attr.mSize.ToString() + " BYTES", Color.Gray);
                    }
                    else { CLI.WriteLine("Error retrieiving file info", Color.Red); }
                }

                CLI.WriteLine("");
                CLI.Write("Total folders: " + folders.Length.ToString());
                CLI.WriteLine("        Total files: " + files.Length.ToString());
            }
            catch (Exception ex) { }
        }
    }
}
