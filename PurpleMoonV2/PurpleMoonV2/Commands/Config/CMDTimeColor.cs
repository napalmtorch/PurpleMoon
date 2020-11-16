﻿using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDTimeColor : Command
    {
        public CMDTimeColor()
        {
            this.Name = "CLKFG";
            this.Help = "Changes the clock color";
        }

        public override void Execute(string line, string[] args)
        {
            int newColor = 40;
            string colString = "";
            if (args.Length == 2)
            {
                colString = args[1];
                newColor = (int)CLI.StringToColor(args[1]);
                if (newColor != 40) { Shell.DateTimeColor = (Color)newColor; CLI.WriteLine("Changed foreground color to " + colString, Color.Green); }
                else { CLI.WriteLine(colString + " is not a valid color!", Color.Red); }
            }
            else { CLI.WriteLine("Invalid arguments!", Color.Red); }
            SystemInfo.SaveConfig(PMFAT.ConfigFile);
            if (Shell.TitleBarVisible) { Shell.DrawTitleBar(); }
        }
    }
}
