using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDInfo : Command
    {
        public CMDInfo()
        {
            this.Name = "INFO";
            this.Help = "Shows operating system and hardware information";
        }

        public override void Execute(string line, string[] args)
        {
            CLI.WriteLine("");
            SystemInfo.ShowInfo();
        }
    }
}
