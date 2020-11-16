using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using PurpleMoonV2.Core;

namespace PurpleMoonV2.Hardware
{
    public static class PMFAT
    {
        // driver
        public static CosmosVFS Driver;

        // directories
        public static string CurrentDirectory = @"0:\";
        public static string ConfigFile = @"0:\SYSTEM\USERCFG.PMC";

        // init
        public static void Initialize()
        {
            try
            {
                // init Driver
                Driver = new CosmosVFS();
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(Driver);

                // check for system path
                if (!FolderExists(@"0:\SYSTEM")) { CreateFolder(@"0:\SYSTEM"); }

                // if no config file exists at default location, create one
                if (!FileExists(ConfigFile)) { WriteAllText(ConfigFile, SystemInfo.DefaultConfig); }
            }
            // unknown exception
            catch (Exception ex)
            {
                Console.WriteLine("Error intializing FAT file system!");
                Console.WriteLine("[INTERNAL] " + ex.Message);
            }
        }

        // get files
        public static string[] GetFiles(string path)
        {
            string[] files = Directory.GetFiles(path);
            return files;
        }

        // get folders
        public static string[] GetFolders(string path)
        {
            string[] folders = Directory.GetDirectories(path);
            return folders;
        }

        // get volumes
        public static List<Sys.FileSystem.Listing.DirectoryEntry> GetVolumes()
        {
            return Driver.GetVolumes();
        }

        // exists
        public static bool FileExists(string file) { return File.Exists(file); }
        public static bool FolderExists(string path) { return Directory.Exists(path); }

        // reads
        public static string[] ReadLines(string path)
        {
            string[] data;
            data = File.ReadAllLines(path);
            return data;
        }
        public static string ReadText(string path)
        {
            string data;
            data = File.ReadAllText(path);
            return data;
        }
        public static byte[] ReadBytes(string path)
        {
            byte[] data;
            data = File.ReadAllBytes(path);
            return data;
        }

        // writes
        public static void WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text);
        }
        public static void WriteAllLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }
        public static void WriteAllBytes(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }
        public static void WriteAllBytes(string path, List<byte> data)
        {
            byte[] input = new byte[data.Count];
            for (int i = 0; i < input.Length; i++) { input[i] = data[i]; }
            WriteAllBytes(path, input);
        }

        // creates
        public static bool CreateFolder(string name)
        {
            bool value = false;
            if (FolderExists(name)) { value = false; }
            else { Directory.CreateDirectory(name); value = true; }
            return value;
        }

        // rename directory
        public static bool RenameFolder(string input, string newName)
        {
            bool value = false;
            if (Directory.Exists(input))
            { Directory.Move(input, newName); value = true; }
            else { value = false; }
            return value;
        }

        // rename file
        public static bool RenameFile(string input, string newName)
        {
            bool value = false;
            if (FileExists(input))
            { File.Move(input, newName); value = true; }
            else { value = false; }
            return value;
        }

        // delete directory
        public static bool DeleteFolder(string path)
        {
            if (FolderExists(path)) { return Driver.DeleteDirectory(Driver.GetDirectory(path)); }
            else { return false; }
        }

        // delete file
        public static bool DeleteFile(string file)
        {
            if (FileExists(file)) { File.Delete(file); return true; }
            else { return false; }
        }

        // get file info
        public static Cosmos.System.FileSystem.Listing.DirectoryEntry GetFileInfo(string file)
        {
            if (FileExists(file))
            {
                try
                {
                    Cosmos.System.FileSystem.Listing.DirectoryEntry attr = Driver.GetFile(file);
                    return attr;
                }
                catch (Exception ex)
                {
                    CLI.WriteLine("Error occured when trying to retrieve file info", Graphics.Color.Red);
                    CLI.Write("[INTERNAL] ", Graphics.Color.Red); CLI.WriteLine(ex.Message, Graphics.Color.White);
                    return null;
                }
            }
            else
            {
                CLI.WriteLine("Could not locate file \"" + file + "\"", Graphics.Color.Red);
                return null;
            }
        }

        public static bool CopyFile(string src, string dest)
        {
            try
            {
                byte[] sourceData = ReadBytes(src);
                WriteAllBytes(dest, sourceData);
                return true;
            }
            catch (Exception ex)
            {
                CLI.WriteLine("Error occured when trying to copy file", Graphics.Color.Red);
                CLI.Write("[INTERNAL] ", Graphics.Color.Red); CLI.WriteLine(ex.Message, Graphics.Color.White);
                return false;
            }
        }

        // convert bytes to megabytes
        public static double BytesToMB(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        // convert bytes to kilobytes
        public static double BytesToKB(long bytes)
        {
            return bytes / 1024;
        }
    }
}
