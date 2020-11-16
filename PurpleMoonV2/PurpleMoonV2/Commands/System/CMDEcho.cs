using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDEcho : Command
    {
        public CMDEcho() 
        {
            this.Name = "ECHO";
            this.Help = "Prints a line of input";
        }

        public override void Execute(string line, string[] args)
        {
            CLI.WriteLine(line.Substring(5, line.Length - 5));
        }
    }
}
