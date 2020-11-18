using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;
using PurpleMoonV2.VM;

namespace PurpleMoonV2.Core
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            // initialize fat driver
            PMFAT.Initialize();

            // init shell
            Shell.Initialize();

            // init vm
            Runner.Initialize();
        }

        protected override void Run()
        {
            // get input
            Shell.GetInput();
        }

        public static void Delay(int millis) { Cosmos.HAL.Global.PIT.Wait((uint)millis); }
    }
}
