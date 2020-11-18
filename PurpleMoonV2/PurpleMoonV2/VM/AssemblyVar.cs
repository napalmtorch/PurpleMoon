using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoonV2.VM
{
    public class AssemblyVar
    {
        public string Name;
        public byte[] Data;
        public int Length;

        // string var
        public AssemblyVar(string name, string data)
        {
            // properties
            this.Name = name;
            this.Length = data.Length;

            // load data
            char[] chars = data.ToCharArray();
            this.Data = new byte[chars.Length];
            for (int i = 0; i < chars.Length; i++) { Data[i] = (byte)chars[i]; }
        }

        // byte var
        public AssemblyVar(string name, byte data)
        {
            // properties
            this.Name = name;
            this.Length = 1;

            // load data
            this.Data = new byte[1] { data };
        }

        // short var
        public AssemblyVar(string name, ushort data)
        {
            // properties
            this.Name = name;
            this.Length = 2;

            // load data
            this.Data = new byte[2]
            {
                 (byte)((data & 0xFF00) >> 8), // left
                 (byte)(data & 0x00FF)         // right
            };
        }
    }
}
