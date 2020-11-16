using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDReboot : Command
    {
        public CMDReboot() 
        {
            this.Name = "REBOOT";
            this.Help = "Reboots the computer";
        }

        public override void Execute(string line, string[] args)
        {
            Cosmos.System.Power.Reboot();
        }
    }
}
