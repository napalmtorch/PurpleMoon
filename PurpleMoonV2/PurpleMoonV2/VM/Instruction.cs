using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoonV2.VM
{
    public class Instruction
    {
        public string Name;
        public string Format;
        public byte OpCode;
        public int Arguments;

        public Instruction(string name = "NOP", byte op = 0x00, int args = 0, string format = "")
        {
            this.Name = name;
            this.OpCode = op;
            this.Arguments = args;
            this.Format = format;
        }
    }
}
