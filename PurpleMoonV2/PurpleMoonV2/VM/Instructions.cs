using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.VM
{
    public static class Instructions
    {
        // instruction list
        public static List<Instruction> List;

        // math instructions
        public static Instruction I_ADD = new Instruction("ADD", 0x01, 2, "bb");
        public static Instruction I_ADDR = new Instruction("ADDR", 0x02, 2, "bb");
        public static Instruction I_ADDI = new Instruction("ADDI", 0x03, 1, "b");
        public static Instruction I_SUB = new Instruction("SUB", 0x04, 2, "bb");
        public static Instruction I_SUBR = new Instruction("SUBR", 0x05, 2, "bb");
        public static Instruction I_SUBI = new Instruction("SUBI", 0x06, 1, "b");
        public static Instruction I_AND = new Instruction("AND", 0x07, 2, "bb");
        public static Instruction I_OR = new Instruction("OR", 0x08, 2, "bb");
        public static Instruction I_XOR = new Instruction("XOR", 0x09, 2, "bb");
        public static Instruction I_SHL = new Instruction("SHL", 0x0A, 2, "bb");
        public static Instruction I_SHR = new Instruction("SHR", 0x0B, 2, "bb");
        public static Instruction I_SE = new Instruction("SE", 0x0C, 2, "bb");
        public static Instruction I_SER = new Instruction("SER", 0x0D, 2, "bb");
        public static Instruction I_SNE = new Instruction("SNE", 0x0E, 2, "bb");
        public static Instruction I_SNER = new Instruction("SNER", 0x0F, 2, "bb");
        public static Instruction I_RAND = new Instruction("RAND", 0xFA, 2, "bb");

        // storage instructions
        public static Instruction I_LOAD = new Instruction("LOAD", 0x10, 3, "bs");
        public static Instruction I_LOADW = new Instruction("LOADW", 0x11, 1, "b");
        public static Instruction I_LOADI = new Instruction("LOADI", 0x12, 2, "s");
        public static Instruction I_STOR = new Instruction("STOR", 0x13, 3, "bs");
        public static Instruction I_STORA = new Instruction("STORA", 0x14, 3, "bs");
        public static Instruction I_STORW = new Instruction("STORW", 0x15, 2, "s");
        public static Instruction I_STORI = new Instruction("STORI", 0x16, 1, "b");

        // flow instructions
        public static Instruction I_JUMP = new Instruction("JUMP", 0x20, 2, "s");
        public static Instruction I_JUMPI = new Instruction("JUMPI", 0x21, 0);
        public static Instruction I_CALL = new Instruction("CALL", 0x22, 2, "s");
        public static Instruction I_CALLI = new Instruction("CALLI", 0x23, 0);
        public static Instruction I_RET = new Instruction("RET", 0x24, 0);

        // system instructions
        public static Instruction I_HALT = new Instruction("HALT", 0x30, 0);
        public static Instruction I_EXIT = new Instruction("EXIT", 0x31, 0);
        public static Instruction I_WAIT = new Instruction("WAIT", 0x32, 0);

        // graphics functions
        public static Instruction I_CLS = new Instruction("CLS", 0x40, 0);
        public static Instruction I_PRINT = new Instruction("PRINT", 0x41, 3, "bs");
        public static Instruction I_PRINTL = new Instruction("PRINTL", 0x42, 3, "bs");
        public static Instruction I_PRINTI = new Instruction("PRINTI", 0x43, 1, "b");
        public static Instruction I_PRINTIL = new Instruction("PRINTIL", 0x44, 1, "b");
        public static Instruction I_FCOL = new Instruction("FCOL", 0x45, 1, "b");
        public static Instruction I_BCOL = new Instruction("BCOL", 0x46, 1, "b");
        public static Instruction I_PIX = new Instruction("PIX", 0x47, 3, "bbb");
        public static Instruction I_PIXR = new Instruction("PIXR", 0x48, 3, "bbb");
        public static Instruction I_PIXA = new Instruction("PIXA", 0x49, 3, "bbb");
        public static Instruction I_PIXAR = new Instruction("PIXAR", 0x4A, 3, "bbb");
        public static Instruction I_CURSX = new Instruction("CURSX", 0x4B, 1, "b");
        public static Instruction I_CURSY = new Instruction("CURSY", 0x4C, 1, "b");
        public static Instruction I_CURGX = new Instruction("CURGX", 0x4D, 1, "b");
        public static Instruction I_CURGY = new Instruction("CURGY", 0x4E, 1, "b");

        // initialize instructions
        public static void Initialize()
        {
            // init list
            List = new List<Instruction>();

            // math instructions
            List.Add(I_ADD);
            List.Add(I_ADDR);
            List.Add(I_ADDI);
            List.Add(I_SUB);
            List.Add(I_SUBR);
            List.Add(I_SUBI);
            List.Add(I_AND);
            List.Add(I_OR);
            List.Add(I_XOR);
            List.Add(I_SHL);
            List.Add(I_SHR);
            List.Add(I_SE);
            List.Add(I_SNE);
            List.Add(I_SER);
            List.Add(I_SNER);
            List.Add(I_RAND);

            List.Add(I_LOAD);
            List.Add(I_LOADW);
            List.Add(I_LOADI);
            List.Add(I_STOR);
            List.Add(I_STORA);
            List.Add(I_STORW);
            List.Add(I_STORI);

            List.Add(I_JUMP);
            List.Add(I_JUMPI);
            List.Add(I_CALL);
            List.Add(I_CALLI);
            List.Add(I_RET);

            List.Add(I_HALT);
            List.Add(I_WAIT);
            List.Add(I_EXIT);

            List.Add(I_CLS);
            List.Add(I_PRINT);
            List.Add(I_PRINTL);
            List.Add(I_PRINTI);
            List.Add(I_PRINTIL);
            List.Add(I_FCOL);
            List.Add(I_BCOL);
            List.Add(I_PIX);
            List.Add(I_PIXR);
            List.Add(I_PIXA);
            List.Add(I_PIXAR);
            List.Add(I_CURSX);
            List.Add(I_CURSY);
            List.Add(I_CURGX);
            List.Add(I_CURGY);
        }

        // get instruction from opcode
        public static Instruction GetInstruction(byte op)
        {
            for (int i = 0; i < List.Count; i++) { if (List[i].OpCode == op) { return List[i]; } }
            return new Instruction("NOP", 0x00, 0);
        }

        public static ushort BytesToAddress(byte left, byte right)
        { return BitConverter.ToUInt16(new byte[] { right, left }, 0); }

        public static Color ByteToColor(byte b) { int data = b; return (Color)data; }

        #region Math Instructions
        public static bool F_ADD()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte b = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.SetRegister(15, (byte)(CPU.Registers[r] + b > 255 ? 1 : 0));
            CPU.SetRegister(r, (byte)(CPU.Registers[r] + b));
            CPU.StepProgramCounter(I_ADD.Arguments);
            return true;
        }

        public static bool F_ADDR()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte ry = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.SetRegister(15, (byte)(CPU.Registers[rx] + CPU.Registers[ry] > 255 ? 1 : 0));
            CPU.SetRegister(rx, (byte)(CPU.Registers[rx] + CPU.Registers[ry]));
            CPU.StepProgramCounter(I_ADDR.Arguments);
            return true;
        }

        public static bool F_ADDI()
        {
            byte b = Memory.Read((ushort)(CPU.ProgCtr + 1));
            CPU.SetI((ushort)(CPU.I + b));
            CPU.StepProgramCounter(I_ADDI.Arguments);
            return true;
        }

        public static bool F_SUB()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte b = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.SetRegister(15, (byte)(CPU.Registers[r] > b ? 1 : 0));
            CPU.SetRegister(r, (byte)(CPU.Registers[r] - b));
            CPU.StepProgramCounter(I_SUB.Arguments);
            return true;
        }

        public static bool F_SUBR()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte ry = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.SetRegister(15, (byte)(CPU.Registers[rx] > CPU.Registers[ry] ? 1 : 0));
            CPU.SetRegister(rx, (byte)(CPU.Registers[rx] - CPU.Registers[ry]));
            CPU.StepProgramCounter(I_SUBR.Arguments);
            return true;
        }

        public static bool F_SUBI()
        {
            byte b = Memory.Read((ushort)(CPU.ProgCtr + 1));
            CPU.SetI((ushort)(CPU.I - b));
            CPU.StepProgramCounter(I_SUBI.Arguments);
            return true;
        }

        public static bool F_OR()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte ry = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.SetRegister(rx, (byte)(CPU.Registers[rx] | CPU.Registers[ry]));
            CPU.StepProgramCounter(I_OR.Arguments);
            return true;
        }

        public static bool F_AND()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte ry = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.SetRegister(rx, (byte)(CPU.Registers[rx] & CPU.Registers[ry]));
            CPU.StepProgramCounter(I_AND.Arguments);
            return true;
        }

        public static bool F_XOR()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte ry = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.SetRegister(rx, (byte)(CPU.Registers[rx] ^ CPU.Registers[ry]));
            CPU.StepProgramCounter(I_XOR.Arguments);
            return true;
        }

        public static bool F_RAND()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte b = Memory.Read((ushort)(CPU.ProgCtr + 2));
            try
            {
                byte rand = (byte)DataUtils.RandNum(b);
                CPU.SetRegister(r, rand);
                CPU.StepProgramCounter(I_RAND.Arguments);
            }
            catch (Exception ex)
            {
                Runner.IsRunning = false;
                CLI.Write("[FATAL] " + Color.Red); CLI.WriteLine(ex.Message, Color.White);
            }
            return true;
        }

        public static bool F_SE()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte b = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte next = Memory.Read((ushort)(CPU.ProgCtr + 3));

            CPU.StepProgramCounter(I_SE.Arguments);
            if (CPU.Registers[rx] == b)
            { CPU.StepProgramCounter(GetInstruction(next).Arguments + 1); }
            return true;
        }

        public static bool F_SER()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte ry = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte next = Memory.Read((ushort)(CPU.ProgCtr + 3));

            CPU.StepProgramCounter(I_SER.Arguments);
            if (CPU.Registers[rx] == CPU.Registers[ry])
            { CPU.StepProgramCounter(GetInstruction(next).Arguments + 1); }
            return true;
        }

        public static bool F_SNE()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte b = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte next = Memory.Read((ushort)(CPU.ProgCtr + 3));

            CPU.StepProgramCounter(I_SNE.Arguments);
            if (CPU.Registers[rx] != b)
            { CPU.StepProgramCounter(GetInstruction(next).Arguments + 1); }
            return true;
        }

        public static bool F_SNER()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte ry = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte next = Memory.Read((ushort)(CPU.ProgCtr + 3));

            CPU.StepProgramCounter(I_SNER.Arguments);
            if (CPU.Registers[rx] != CPU.Registers[ry])
            { CPU.StepProgramCounter(GetInstruction(next).Arguments + 1); }
            return true;
        }

        public static bool F_SHL()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte v = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.Registers[15] = (byte)(((CPU.Registers[r] & 0x80) == 0x80) ? 1 : 0);
            CPU.Registers[r] = (byte)(CPU.Registers[r] << v);
            return true;
        }

        public static bool F_SHR()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte v = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.Registers[15] = (byte)(((CPU.Registers[r] & 0x80) == 0x80) ? 1 : 0);
            CPU.Registers[r] = (byte)(CPU.Registers[r] >> v);
            return true;
        }
        #endregion

        #region Flow Instructions
        public static bool F_JUMP()
        {
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.MoveProgramCounter(BytesToAddress(left, right));
            return true;
        }

        public static bool F_JUMPI()
        {
            CPU.MoveProgramCounter(CPU.I);
            return true;
        }

        public static bool F_CALL()
        {
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.PushStack(CPU.ProgCtr);
            CPU.MoveProgramCounter(BytesToAddress(left, right));
            return true;
        }

        public static bool F_CALLI()
        {
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 2));
            CPU.PushStack(CPU.ProgCtr);
            CPU.MoveProgramCounter(CPU.I);
            return true;
        }

        public static bool F_RET()
        {
            CPU.MoveProgramCounter(CPU.PopStack());
            // skips the call fuction it goes back to
            CPU.StepProgramCounter(I_CALL.Arguments);
            return true;
        }
        #endregion

        #region Data Instructions

        public static bool F_LOAD()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 3));
            ushort addr = BytesToAddress(left, right);
            CPU.SetRegister(r, Memory.Read(addr));
            CPU.StepProgramCounter(I_LOAD.Arguments);
            return true;
        }

        public static bool F_LOADW()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            CPU.SetRegister(r, CPU.W);
            CPU.StepProgramCounter(I_LOADW.Arguments);
            return true;
        }

        public static bool F_LOADI()
        {
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 2));
            ushort addr = BytesToAddress(left, right);
            CPU.SetI(addr);
            CPU.StepProgramCounter(I_LOADI.Arguments);
            return true;
        }

        public static bool F_STOR()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 3));
            Memory.Write(BytesToAddress(left, right), CPU.Registers[r]);
            CPU.StepProgramCounter(I_STOR.Arguments);
            return true;
        }

        public static bool F_STORA()
        {
            byte b = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 3));
            ushort addr = BytesToAddress(left, right);
            Memory.Write(addr, b);
            CPU.StepProgramCounter(I_STORA.Arguments);
            return true;
        }

        public static bool F_STORW()
        {
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 2));
            Memory.Write(BytesToAddress(left, right), CPU.W);
            CPU.StepProgramCounter(I_STORW.Arguments);
            return true;
        }

        public static bool F_STORI()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            Memory.Write(CPU.I, CPU.Registers[r]);
            CPU.StepProgramCounter(I_STORI.Arguments);
            return true;
        }

        #endregion

        #region System Instructions

        public static bool F_WAIT()
        {
            // get key
            ConsoleKeyInfo key = Console.ReadKey(true);
            char c = (char)0;

            // character
            if (key.KeyChar >= 32 && key.KeyChar < 127) { c = key.KeyChar; }

            // special keys
            if (key.Key == ConsoleKey.Enter) { c = (char)240; }
            if (key.Key == ConsoleKey.Backspace) { c = (char)241; }
            if (key.Key == ConsoleKey.LeftArrow) { c = (char)242; }
            if (key.Key == ConsoleKey.RightArrow) { c = (char)243; }
            if (key.Key == ConsoleKey.UpArrow) { c = (char)244; }
            if (key.Key == ConsoleKey.DownArrow) { c = (char)245; }

            // send data
            CPU.SetW((byte)c);


            // redraw debug text
            if (CPU.DebugVisible) { CPU.DrawDebug(); }

            CPU.StepProgramCounter(I_WAIT.Arguments + 1);
            return true;
        }

        public static bool F_EXIT()
        {
            Runner.IsRunning = false;
            return true;
        }

        public static bool F_HALT()
        {
            CPU.Halt();
            return true;
        }
        #endregion

        #region Graphics Instructions

        public static bool F_CLS() 
        {
            CLI.ForceClear(CLI.BackColor);
            return true; 
        }

        public static bool F_FCOL()
        {
            byte col = Memory.Read((ushort)(CPU.ProgCtr + 1));
            CLI.ForeColor = ByteToColor(col);
            CPU.StepProgramCounter(I_FCOL.Arguments);
            return true;
        }

        public static bool F_BCOL()
        {
            byte col = Memory.Read((ushort)(CPU.ProgCtr + 1));
            CLI.BackColor = ByteToColor(col);
            CPU.StepProgramCounter(I_BCOL.Arguments);
            return true;
        }

        public static bool F_PRINT()
        {
            // fetch data
            byte len = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 3));
            ushort addr = BytesToAddress(left, right);
            char key = (char)Memory.Read(addr);

            // exit if out of range
            if (addr + len >= Memory.Size) { return false; }

            string text = "";
            // if not valid print space
            if (key == 0 || key == 32) { text = ""; CLI.Write(" "); }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    char c = ((char)Memory.Read(addr + i));
                    if (c >= 32 && c < 127) { text += c.ToString(); }
                }
                if (text.Length > 0) { CLI.Write(text); }
            }

            CPU.StepProgramCounter(I_PRINT.Arguments);
            return true;
        }

        public static bool F_PRINTI()
        {
            // fetch data
            byte len = Memory.Read((ushort)(CPU.ProgCtr + 1));
            char key = (char)Memory.Read(CPU.I);

            // exit if out of range
            if (CPU.I + len >= Memory.Size) { return false; }

            string text = "";
            // if not valid print space
            if (key == 0 || key == 32) { text = ""; CLI.Write(" "); }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    char c = ((char)Memory.Read(CPU.I + i));
                    if (c >= 32 && c < 127) { text += c.ToString(); }
                }
                if (text.Length > 0) { CLI.Write(text); }
            }

            CPU.StepProgramCounter(I_PRINTI.Arguments);
            return true;
        }


        public static bool F_PRINTL()
        {
            // fetch data
            byte len = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte left = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte right = Memory.Read((ushort)(CPU.ProgCtr + 3));
            ushort addr = BytesToAddress(left, right);
            char key = (char)Memory.Read(addr);

            // exit if out of range
            if (addr + len >= Memory.Size) { return false; }

            string text = "";
            // if not valid print space
            if (key == 0 || key == 32) { text = ""; CLI.Write(" "); }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    char c = ((char)Memory.Read(addr + i));
                    if (c >= 32 && c < 127) { text += c.ToString(); }
                }
                CLI.WriteLine(text);
            }

            CPU.StepProgramCounter(I_PRINTL.Arguments);
            return true;
        }


        public static bool F_PRINTIL()
        {
            // fetch data
            byte len = Memory.Read((ushort)(CPU.ProgCtr + 1));
            char key = (char)Memory.Read(CPU.I);

            // exit if out of range
            if (CPU.I + len >= Memory.Size) { return false; }

            string text = "";
            // if not valid print space
            if (key == 0 || key == 32) { text = ""; CLI.Write(" "); }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    char c = ((char)Memory.Read(CPU.I + i));
                    if (c >= 32 && c < 127) { text += c.ToString(); }
                }
                CLI.WriteLine(text);
            }

            CPU.StepProgramCounter(I_PRINTIL.Arguments);
            return true;
        }

        public static bool F_PIX()
        {
            byte col = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte xx = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte yy = Memory.Read((ushort)(CPU.ProgCtr + 3));
            if (col > 0xF) { col = 0xF; }
            TextGraphics.SetPixel(xx, yy, (Color)col);
            CPU.StepProgramCounter(I_PIX.Arguments);
            return true;
        }

        public static bool F_PIXR()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte xx = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte yy = Memory.Read((ushort)(CPU.ProgCtr + 3));
            if (CPU.Registers[r] <= 0xF)
            {  TextGraphics.SetPixel(xx, yy, (Color)CPU.Registers[r]); }
            else { TextGraphics.SetPixel(xx, yy, Color.White); }
            CPU.StepProgramCounter(I_PIXR.Arguments);
            return true;
        }

        public static bool F_PIXA()
        {
            byte col = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte ry = Memory.Read((ushort)(CPU.ProgCtr + 3));
            if (col > 0xF) { col = 0xF; }
            TextGraphics.SetPixel(CPU.Registers[rx], CPU.Registers[ry], (Color)col);
            CPU.StepProgramCounter(I_PIXA.Arguments);
            return true;
        }

        public static bool F_PIXAR()
        {
            byte r = Memory.Read((ushort)(CPU.ProgCtr + 1));
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 2));
            byte ry = Memory.Read((ushort)(CPU.ProgCtr + 3));
            if (CPU.Registers[r] <= 0xF)
            { TextGraphics.SetPixel(CPU.Registers[rx], CPU.Registers[ry], (Color)CPU.Registers[r]); }
            else { TextGraphics.SetPixel(CPU.Registers[rx], CPU.Registers[ry], Color.White); }
            CPU.StepProgramCounter(I_PIXAR.Arguments);
            return true;
        }

        public static bool F_CURSX()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            CLI.SetCursorPos(CPU.Registers[rx], CLI.CursorY);
            CPU.StepProgramCounter(I_CURSX.Arguments);
            return true;
        }

        public static bool F_CURSY()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            CLI.SetCursorPos(CLI.CursorX, CPU.Registers[rx]);
            CPU.StepProgramCounter(I_CURSY.Arguments);
            return true;
        }

        public static bool F_CURGX()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            CPU.Registers[rx] = (byte)CLI.CursorX;
            CPU.StepProgramCounter(I_CURGX.Arguments);
            return true;
        }

        public static bool F_CURGY()
        {
            byte rx = Memory.Read((ushort)(CPU.ProgCtr + 1));
            CPU.Registers[rx] = (byte)CLI.CursorY;
            CPU.StepProgramCounter(I_CURGY.Arguments);
            return true;
        }

        #endregion
    }
}
