using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Core
{
    public static class Shell
    {
        // configuration
        public static Color TitleBarColor = Color.DarkMagenta;
        public static Color DateTimeColor = Color.White;
        public static Color TitleColor = Color.Magenta;
        public static bool TitleBarVisible = true;

        // command list
        public static List<Command> Commands = new List<Command>();

        // initialize
        public static void Initialize()
        {
            // add commands
            AddCommands();

            // load config
            SystemInfo.LoadConfig(PMFAT.ConfigFile);

            // clear 
            DrawFresh();

            // show info
            SystemInfo.ShowInfo();
        }

        // populate command list
        private static void AddCommands()
        {
            // system
            Commands.Add(new Commands.CMDClear());
            Commands.Add(new Commands.CMDEcho());
            Commands.Add(new Commands.CMDColors());
            Commands.Add(new Commands.CMDInfo());
            Commands.Add(new Commands.CMDHelp());
            Commands.Add(new Commands.CMDReboot());
            Commands.Add(new Commands.CMDShutdown());

            // config
            Commands.Add(new Commands.CMDForeColor());
            Commands.Add(new Commands.CMDBackColor());
            Commands.Add(new Commands.CMDTitleBarColor());
            Commands.Add(new Commands.CMDTitleColor());
            Commands.Add(new Commands.CMDTimeColor());
            Commands.Add(new Commands.CMDTitleBar());

            // file system
            Commands.Add(new Commands.CMDSetDir());
            Commands.Add(new Commands.CMDListDir());
            Commands.Add(new Commands.CMDMakeDir());
            Commands.Add(new Commands.CMDDelDir());
            Commands.Add(new Commands.CMDCutFile());
            Commands.Add(new Commands.CMDCopyFile());
            Commands.Add(new Commands.CMDDelFile());

            // vm
        }

        // clear screen, draw title bar, set cursor pos
        public static void DrawFresh()
        {
            // clear screen
            TextGraphics.Clear(CLI.BackColor);

            // draw title bar
            if (TitleBarVisible) { DrawTitleBar(); CLI.SetCursorPos(0, 2); }
            else { CLI.SetCursorPos(0, 0); }
        }

        // draw title bar
        public static void DrawTitleBar()
        {
            TextGraphics.DrawLineH(0, 0, CLI.Width, ' ', Color.Black, TitleBarColor); // draw background
            DrawTime(); // draw time
            TextGraphics.DrawString(CLI.Width - 13, 0, "PurpleMoon OS", TitleColor, TitleBarColor); // draw os name
        }

        // draw time
        public static void DrawTime() { TextGraphics.DrawString(0, 0, RTC.GetDateFormatted() + " " + RTC.GetTimeFormatted(), DateTimeColor, TitleBarColor); }

        // begin accepting commands
        public static void GetInput()
        {
            if (PMFAT.CurrentDirectory == @"0:\") { CLI.Write("root", Color.Magenta); CLI.Write(":- ", Color.White); }
            else
            {
                CLI.Write("root", Color.Magenta); CLI.Write("@", Color.White);
                CLI.Write(PMFAT.CurrentDirectory.Substring(3, PMFAT.CurrentDirectory.Length - 3), Color.Yellow);
                CLI.Write(":- ", Color.White);
            }
            string input = CLI.ReadLine();
            ParseCommand(input);
        }

        // parse command
        private static void ParseCommand(string line)
        {
            // if a command has actually been enter
            if (line.Length > 0)
            {
                string[] args = line.Split(' ');
                bool error = true;
                for (int i = 0; i < Commands.Count; i++)
                {
                    // validate command
                    if (args[0].ToUpper() == Commands[i].Name)
                    {
                        // execute and finish
                        Commands[i].Execute(line, args);
                        error = false;
                        break;
                    }
                }

                // invalid command has been entered
                if (error) { CLI.WriteLine("Invalid command or program!", Color.Red); }
            }

            // continue fetching commands
            DrawTitleBar();
            GetInput();
        }

        // get command from list based on name
        public static Command GetCommand(string cmd)
        {
            for (int i = 0; i < Commands.Count; i++)
            {
                if (Commands[i].Name == cmd.ToUpper()) { return Commands[i]; }
            }
            return null;
        }
    }
}
