using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;
using PurpleMoonV2.VM;

namespace PurpleMoonV2.Commands
{
    public class CMDRun : Command
    {
        public CMDRun()
        {
            this.Name = "RUN";
            this.Help = "Execute a specified binary file";
        }

        public override void Execute(string line, string[] args)
        {
            if (line.Length > 4)
            {
                string file = "", realFile = "*ERROR";
                if (args[1] == "-d")
                {
                    if (line.Length > 7)
                    {
                        CPU.DebugVisible = true;
                        file = line.Substring(7, line.Length - 7);
                        realFile = TryParseFile(file, true);
                    }
                    else { CLI.WriteLine("Invalid arguments!", Color.Red); }
                }
                else
                {
                    CPU.DebugVisible = false;
                    file = line.Substring(4, line.Length - 4);
                    realFile = TryParseFile(file, true);
                }

                if (realFile != "*ERROR")
                {
                    if (realFile.ToUpper().EndsWith(".BIN") || realFile.ToUpper().EndsWith(".PRG"))
                    {
                        try
                        {
                            byte[] data = PMFAT.ReadBytes(realFile);
                            Runner.Reset(true);
                            Memory.WriteArray(0, data, data.Length);
                            Runner.Start();
                        }
                        catch (Exception ex)
                        {
                            CLI.WriteLine("Error occurred attempting to execute \"" + realFile + "\"", Color.Red);
                            CLI.Write("[INTERNAL] ", Color.Red); CLI.WriteLine(ex.Message, Color.White);
                        }
                    }
                    else { CLI.WriteLine("File is not marked as executable", Color.Red); CLI.WriteLine("Expected file extension .BIN or .PRG", Color.White); }
                }
                else { CLI.WriteLine("Error occurred attempting to execute \"" + file + "\"", Color.Red); }
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
