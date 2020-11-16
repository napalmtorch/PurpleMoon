using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoonV2.Core
{
    public abstract class Command
    {
        // properties
        public string Name;
        public string Help;

        // execution
        public abstract void Execute(string line, string[] args);
    }
}
