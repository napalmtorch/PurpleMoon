using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDHelp : Command
    {
        public CMDHelp()
        {
            this.Name = "HELP";
            this.Help = "Shows the help menu";
        }

        public override void Execute(string line, string[] args)
        {
            // show menu
            if (args.Length == 1)
            {
                // draw
                SetupScreen();
                DrawMessage();

                // exit message
                TextGraphics.DrawString(2, CLI.Height - 2, "Press any key to exit...", Color.White, Color.Blue);

                // exit
                CLI.SetCursorPos(CLI.Width - 13, 0);
                CLI.ReadKey(true);
                Shell.DrawFresh();
            }
            // show command usage
            else
            {
                string cmd = args[1].ToUpper();
                bool invalid = false;
                for (int i = 0; i < Shell.Commands.Count; i++)
                {
                    if (Shell.Commands[i].Name == cmd)
                    {
                        CLI.WriteLine(Shell.Commands[i].Help);
                        if (Shell.Commands[i].Usage.Length > 0) { CLI.WriteLine(Shell.Commands[i].Usage, Color.Gray); }
                        invalid = false;
                        break;
                    }
                    else { invalid = true; }
                }

                if (invalid) { CLI.WriteLine("Invalid argument!", Color.Red); CLI.WriteLine("Type help without an argument to see available commands.", Color.White); }
            }
        }

        private void SetupScreen()
        {
            CLI.ForceClear(Color.Black);
            Shell.DrawTitleBar();
            TextGraphics.FillRect(2, 2, CLI.Width - 4, CLI.Height - 3, ' ', Color.White, Color.Blue);
            TextGraphics.DrawLineH(2, 2, CLI.Width - 4, ' ', Color.White, Color.Cyan);
            TextGraphics.DrawString(2, 2, " CMD                   DESCRIPTION", Color.Black, Color.Cyan);
            TextGraphics.DrawLineH(0, 3, CLI.Width, ' ', Color.White, Color.Black);
            TextGraphics.DrawString(Shell.TitleBarTime.Length, 0, " | Run \"help [cmd]\" for usage", Shell.TitleColor, Shell.TitleBarColor);
            CLI.SetCursorPos(2, 2);
        }

        private void DrawMessage()
        {
            int dx = 3, dy = 4;
            for (int i = 0; i < Shell.Commands.Count; i++)
            {
                TextGraphics.DrawString(dx, dy, "---------------------", Color.DarkGray, Color.Blue);
                TextGraphics.DrawString(dx, dy, Shell.Commands[i].Name + " ", Color.White, Color.Blue);
                TextGraphics.DrawString(dx + 22, dy, Shell.Commands[i].Help, Color.Gray, Color.Blue);
                dy += 1;
                if (dy >= 23)
                {
                    // next message
                    TextGraphics.DrawString(2, CLI.Height - 2, "Press any key to goto next page", Color.White, Color.Blue);
                    CLI.SetCursorPos(CLI.Width - 13, 0);

                    // goto next page
                    CLI.ReadKey(true);
                    SetupScreen();
                    dy = 4; 
                }
            }
        }
    }
}
