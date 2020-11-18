using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoonV2.VM
{
    public class AssemblyLabel
    {
        public string Name;
        public int PC;

        public AssemblyLabel(string name, int pc) { this.Name = name; this.PC = pc; }
    }
}
