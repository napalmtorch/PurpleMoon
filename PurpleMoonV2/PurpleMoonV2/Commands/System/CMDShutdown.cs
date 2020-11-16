using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDShutdown : Command
    {
        public CMDShutdown()
        { 
            this.Name = "SHUTDOWN";
            this.Help = "Turn off the computer";
        }

        public override void Execute(string line, string[] args)
        {
            Cosmos.System.Power.Shutdown();
        }
    }
}
