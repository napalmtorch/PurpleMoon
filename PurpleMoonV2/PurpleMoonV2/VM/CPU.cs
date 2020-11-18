using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.VM
{
    public static class CPU
    {
        public const int MaxRegisters = 16;
        public const int StackSize = 256;

        // registers
        public static byte[] Registers { get; private set; }
        public static ushort[] Stack { get; private set; }
        public static ushort ProgCtr { get; private set; }
        public static ushort StackPtr { get; private set; }
        public static ushort I { get; private set; }
        public static byte W { get; private set; }

        // op code
        public static byte OpCode { get; private set; }

        // flags
        public static bool IsWaiting { get; private set; }
        public static bool IsHalted { get; private set; }
        public static bool DebugVisible { get; set; }

        // updates
        public static int Delay = 100;

        // initialize
        public static void Initialize()
        {
            // init registers
            Registers = new byte[MaxRegisters];
            Stack = new ushort[StackSize];
            ProgCtr = 0; StackPtr = 0; I = 0; W = 0;

            // clear ram
            Memory.Clear();

            // flags
            DebugVisible = true;
            IsWaiting = false;
            IsHalted = true;
        }

        // reset
        public static void Reset()
        {
            ClearRegisters();
            ClearStack();
            ProgCtr = 0; StackPtr = 0; I = 0; W = 0;

            // flags
            IsWaiting = false;
            IsHalted = true;
        }

        // clear registers
        public static void ClearRegisters() { for (int i = 0; i < MaxRegisters; i++) { Registers[i] = 0x00; } }

        // clear stack
        public static void ClearStack() { for (int i = 0; i < StackSize; i++) { Stack[i] = 0x0000; } }

        // push stack
        public static bool PushStack(ushort data)
        {
            if (StackPtr >= StackSize) { return false; }
            else { Stack[++StackPtr] = data; return true; }
        }

        // pop stack
        public static ushort PopStack()
        {
            if (StackPtr < 0) { return 0x0000; }
            else { ushort val = Stack[StackPtr--]; return val; }
        }

        // peek stack
        public static ushort PeekStack()
        {
            if (StackPtr < 0 || StackPtr >= StackSize) { return 0x0000; }
            else { ushort val = Stack[StackPtr]; return val; }
        }

        // halt state
        public static void Continue() { IsHalted = false; }
        public static void Halt() { IsHalted = true; }

        // wait state
        public static void Wait() { IsWaiting = true; }
        public static void StopWaiting() { IsWaiting = false; }

        // fetch opcode
        private static void FetchOpCode() { OpCode = Memory.Read(ProgCtr); }

        // program counter
        public static void StepProgramCounter(int steps) { ProgCtr += (ushort)steps; }
        public static void MoveProgramCounter(int val) { ProgCtr = (ushort)val; }

        // set values
        public static void SetRegister(int r, byte b) { Registers[r] = b; }
        public static void SetI(ushort s) { I = s; }
        public static void SetW(byte b) { W = b; }

        // clock
        public static void Clock()
        {
            if (!IsHalted)
            {
                // reset program counter at max
                if (ProgCtr >= Memory.Size)
                {
                    Runner.Stop();
                    Shell.DrawFresh();
                    ProgCtr = 0;
                    CLI.WriteLine("Program counter left memory space", Color.DarkYellow);
                    Shell.GetInput();
                }

                // fetch op code
                FetchOpCode();

                // execute opcode
                bool executed = Execute(OpCode);

                if (DebugVisible)
                {
                    // drawing constantly at max speed results in the OS crashing
                    // so we limit the speed here to 1ms, which is slow enough to be stable
                    Kernel.Delay(Delay);
                }
            }

            // draw debug text
            if (DebugVisible) { DrawDebug(); }
        }

        // excute op code
        public static bool Execute(byte op)
        {
            bool success = false;

            // math
            if (OpCode == Instructions.I_ADD.OpCode) { success = Instructions.F_ADD(); ProgCtr++; }
            else if (OpCode == Instructions.I_ADDR.OpCode) { success = Instructions.F_ADDR(); ProgCtr++; }
            else if (OpCode == Instructions.I_ADDI.OpCode) { success = Instructions.F_ADDI(); ProgCtr++; }
            else if (OpCode == Instructions.I_SUB.OpCode) { success = Instructions.F_SUB(); ProgCtr++; }
            else if (OpCode == Instructions.I_SUBR.OpCode) { success = Instructions.F_SUBR(); ProgCtr++; }
            else if (OpCode == Instructions.I_SUBI.OpCode) { success = Instructions.F_SUBI(); ProgCtr++; }
            else if (OpCode == Instructions.I_AND.OpCode) { success = Instructions.F_AND(); ProgCtr++; }
            else if (OpCode == Instructions.I_OR.OpCode) { success = Instructions.F_OR(); ProgCtr++; }
            else if (OpCode == Instructions.I_XOR.OpCode) { success = Instructions.F_XOR(); ProgCtr++; }
            else if (OpCode == Instructions.I_SHL.OpCode) { success = Instructions.F_SHL(); ProgCtr++; }
            else if (OpCode == Instructions.I_SHR.OpCode) { success = Instructions.F_SHR(); ProgCtr++; }
            else if (OpCode == Instructions.I_SE.OpCode) { success = Instructions.F_SE(); ProgCtr++; }
            else if (OpCode == Instructions.I_SNE.OpCode) { success = Instructions.F_SNE(); ProgCtr++; }
            else if (OpCode == Instructions.I_SER.OpCode) { success = Instructions.F_SER(); ProgCtr++; }
            else if (OpCode == Instructions.I_SNER.OpCode) { success = Instructions.F_SNER(); ProgCtr++; }
            else if (OpCode == Instructions.I_RAND.OpCode) { success = Instructions.F_RAND(); ProgCtr++; }
            // flow
            else if (OpCode == Instructions.I_JUMP.OpCode) { success = Instructions.F_JUMP(); }
            else if (OpCode == Instructions.I_JUMPI.OpCode) { success = Instructions.F_JUMPI(); }
            else if (OpCode == Instructions.I_CALL.OpCode) { success = Instructions.F_CALL(); }
            else if (OpCode == Instructions.I_CALLI.OpCode) { success = Instructions.F_CALLI(); }
            else if (OpCode == Instructions.I_RET.OpCode) { success = Instructions.F_RET(); ProgCtr++; }
            // data
            else if (OpCode == Instructions.I_LOAD.OpCode) { success = Instructions.F_LOAD(); ProgCtr++; }
            else if (OpCode == Instructions.I_LOADI.OpCode) { success = Instructions.F_LOADI(); ProgCtr++; }
            else if (OpCode == Instructions.I_LOADW.OpCode) { success = Instructions.F_LOADW(); ProgCtr++; }
            else if (OpCode == Instructions.I_STOR.OpCode) { success = Instructions.F_STOR(); ProgCtr++; }
            else if (OpCode == Instructions.I_STORA.OpCode) { success = Instructions.F_STORA(); ProgCtr++; }
            else if (OpCode == Instructions.I_STORW.OpCode) { success = Instructions.F_STORW(); ProgCtr++; }
            else if (OpCode == Instructions.I_STORI.OpCode) { success = Instructions.F_STORI(); ProgCtr++; }
            // system
            else if (OpCode == Instructions.I_HALT.OpCode) { success = Instructions.F_HALT(); }
            else if (OpCode == Instructions.I_EXIT.OpCode) { success = Instructions.F_EXIT(); }
            else if (OpCode == Instructions.I_WAIT.OpCode) { success = Instructions.F_WAIT(); }
            // graphics
            else if (OpCode == Instructions.I_CLS.OpCode) { success = Instructions.F_CLS(); ProgCtr++; }
            else if (OpCode == Instructions.I_PRINT.OpCode) { success = Instructions.F_PRINT(); ProgCtr++; }
            else if (OpCode == Instructions.I_PRINTL.OpCode) { success = Instructions.F_PRINTL(); ProgCtr++; }
            else if (OpCode == Instructions.I_PRINTI.OpCode) { success = Instructions.F_PRINTI(); ProgCtr++; }
            else if (OpCode == Instructions.I_PRINTIL.OpCode) { success = Instructions.F_PRINTIL(); ProgCtr++; }
            else if (OpCode == Instructions.I_PIX.OpCode) { success = Instructions.F_PIX(); ProgCtr++; }
            else if (OpCode == Instructions.I_PIXR.OpCode) { success = Instructions.F_PIXR(); ProgCtr++; }
            else if (OpCode == Instructions.I_PIXA.OpCode) { success = Instructions.F_PIXA(); ProgCtr++; }
            else if (OpCode == Instructions.I_PIXAR.OpCode) { success = Instructions.F_PIXAR(); ProgCtr++; }
            else if (OpCode == Instructions.I_FCOL.OpCode) { success = Instructions.F_FCOL(); ProgCtr++; }
            else if (OpCode == Instructions.I_BCOL.OpCode) { success = Instructions.F_BCOL(); ProgCtr++; }
            else if (OpCode == Instructions.I_CURSX.OpCode) { success = Instructions.F_CURSX(); ProgCtr++; }
            else if (OpCode == Instructions.I_CURSY.OpCode) { success = Instructions.F_CURSY(); ProgCtr++; }
            else if (OpCode == Instructions.I_CURGX.OpCode) { success = Instructions.F_CURGX(); ProgCtr++; }
            else if (OpCode == Instructions.I_CURGY.OpCode) { success = Instructions.F_CURGY(); ProgCtr++; }
            // skip invalid
            else { success = false; ProgCtr++; }
            return success;
        }

        // draw debug information
        public static void DrawDebug()
        {
            // draw registers
            string regs = "";
            for (int i = 0; i < MaxRegisters; i++) { regs += DataUtils.IntToHex(Registers[i], "X2") + " "; }
            if (regs.Length > 0) { TextGraphics.DrawString(0, CLI.Height - 1, regs, Color.Cyan, CLI.BackColor); }

            // draw program counter
            TextGraphics.DrawString(48, CLI.Height - 1, "PC=" + DataUtils.IntToHex(ProgCtr), Color.Yellow, CLI.BackColor);

            // draw i register
            TextGraphics.DrawString(58, CLI.Height - 1, "I=" + DataUtils.IntToHex(I), Color.Green, CLI.BackColor);

            // draw w register
            TextGraphics.DrawString(67, CLI.Height - 1, "W=" + DataUtils.IntToHex(W), Color.Magenta, CLI.BackColor);

            // draw waiting
            TextGraphics.DrawString(72, CLI.Height - 1, "WAIT=" + (IsWaiting ? "1" : "0"), Color.DarkRed, CLI.BackColor);
        }
    }
}
