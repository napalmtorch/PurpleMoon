using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDTitleBar : Command
    {
        public CMDTitleBar()
        {
            this.Name = "TBAR";
            this.Help = "Enable/disable the title bar";
        }

        public override void Execute(string line, string[] args)
        {
            bool result = true;
            bool error = false;
            if (args[1] == "0" || args[1].ToLower() == "false" || args[1].ToLower() == "no" || args[1].ToLower() == "off") { result = false; }
            else if (args[1] == "1" || args[1].ToLower() == "true" || args[1].ToLower() == "yes" || args[1].ToLower() == "on") { result = true; }
            else { CLI.Write("Invalid argument!", Color.Red); CLI.WriteLine("Usage: tbar (1,0) (true,false) (yes,no) (on,off) "); error = true; }

            if (!error)
            {
                Shell.TitleBarVisible = result;
                int oldX = CLI.CursorX, oldY = CLI.CursorY;
                SystemInfo.SaveConfig(PMFAT.ConfigFile);
                if (result)
                {
                    Shell.DrawTitleBar();
                    CLI.WriteLine("Title bar is now visible.");
                }
                else
                {
                    TextGraphics.DrawLineH(0, 0, CLI.Width, ' ', Color.Black, CLI.BackColor);
                    CLI.SetCursorPos(oldX, oldY);
                    CLI.WriteLine("Title bar hidden.");
                }
            }
        }
    }
}
