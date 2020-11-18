using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.VM
{
    public static class Assembler
    {
        private static List<AssemblyLabel> Labels = new List<AssemblyLabel>();
        private static List<AssemblyVar> Variables = new List<AssemblyVar>();
        private static List<string> Input = new List<string>();
        private static List<string> LabelOutput = new List<string>();
        private static List<string> VarOutput = new List<string>();
        private static List<byte> Output = new List<byte>();

        // save assembled code to file
        public static bool AssembleFile(string fileIn, string fileOut)
        {
            try
            {
                if (PMFAT.FileExists(fileIn))
                {
                    // read file
                    Input = DataUtils.StringArrayToList(PMFAT.ReadLines(fileIn));

                    // store variables, then labels

                    // store labels
                    StoreVariables();
                    if (!StoreLabels()) { CLI.Write("[ERROR] ", Color.Red); CLI.WriteLine("Problem occured while storing labels."); return false; }
                    if (!ReplaceLabels()) { CLI.Write("[ERROR] ", Color.Red); CLI.WriteLine("Problem occured while replacing labels with jumps."); return false; }
                    if (!AssembleCode(fileOut)) { CLI.Write("[ERROR] ", Color.Red); CLI.WriteLine("Problem occured while converting assmebler to bytecode."); return false; }
                    return true;
                }
                else
                {
                    CLI.WriteLine("File system error!", Color.Red);
                    CLI.WriteLine("Input: " + fileIn, Color.White);
                    CLI.WriteLine("Output: " + fileOut, Color.White);
                    return false;
                }

            }
            catch (Exception ex)
            {
                CLI.WriteLine("Unknown error has occured while assembling!", Color.Red);
                CLI.Write("[INTERNAL] ", Color.Red); CLI.WriteLine(ex.Message, Color.White);
            }
            return false;
        }

        // store labels into a list
        private static bool StoreLabels()
        {
            // clear list
            Labels.Clear();

            // simulated program counter
            int PC = 0;

            // parse lines
            for (int i = 0; i < Input.Count; i++)
            {
                // get current line
                string line = Input[i].Replace(",", " ");

                // get arguments
                List<string> args = StringArrayToList(line.Split(' '));

                // remove blank arguments
                for (int j = 0; j < args.Count; j++)
                {
                    args[j] = StringRemoveBlanks(args[j].ToCharArray());
                    if (IsBlank(args[j])) { args.RemoveAt(j); j--; }
                }

                // is line is commented or blank ?
                if (!line.StartsWith(";") && line.Length > 0 && args[0] != "WORD")
                {
                    // is formatted and only 1 argument ?
                    if (args.Count == 1 && line.Contains(":"))
                    {
                        // remove symbols
                        string label = args[0];
                        label = label.Replace(":", "");

                        // store label
                        Labels.Add(new AssemblyLabel(label, PC));
                    }

                    // command
                    if (!line.Contains(":"))
                    {
                        for (int j = 0; j < Instructions.List.Count; j++)
                        {
                            if (args[0].ToUpper() == Instructions.List[j].Name)
                            {
                                // increment program counter
                                PC += Instructions.List[j].Arguments + 1;
                                break;
                            }
                        }
                    }
                }
            }

            return true;
        }

        // replace labels with jump commands
        private static bool ReplaceLabels()
        {
            // prepare list
            LabelOutput.Clear();
            for (int z = 0; z < Input.Count; z++) { LabelOutput.Add(Input[z]); }

            // loop through lines
            for (int i = 0; i < LabelOutput.Count; i++)
            {
                // remove if blank
                if (IsBlank(LabelOutput[i])) { LabelOutput.RemoveAt(i); i--; }

                // get current line
                LabelOutput[i] = LabelOutput[i].Replace(",", "");
                int lastIndex = LastIndexOf(LabelOutput[i], ';');
                if (lastIndex > 0 && lastIndex < LabelOutput[i].Length)
                {
                    LabelOutput[i] = LabelOutput[i].Substring(0, lastIndex);
                }
                if (LabelOutput[i].StartsWith(";")) { LabelOutput[i] = ""; }
                string line = LabelOutput[i];

                // get arguments
                List<string> args = StringArrayToList(line.Split(' '));

                // remove blank arguments
                for (int j = 0; j < args.Count; j++)
                {
                    args[j] = StringRemoveBlanks(args[j].ToCharArray());
                    if (IsBlank(args[j])) { args.RemoveAt(j); j--; }
                }

                // is line is commented or blank ?
                if (!line.StartsWith(";") && line.Length > 0 && args[0] != "WORD")
                {
                    // is formatted and only 1 argument ?
                    if (line.Contains(":"))
                    {
                        LabelOutput.RemoveAt(i); i--;
                    }
                    else
                    {
                        // replace labels
                        for (int j = 0; j < Labels.Count; j++)
                        {
                            // if jump
                            if (args[0].ToUpper() == Instructions.I_JUMP.Name)
                            {
                                if (args[1] == Labels[j].Name)
                                {
                                    LabelOutput[i] = "JUMP $" + IntToHex(Labels[j].PC);
                                }
                            }
                        }

                        for (int k = 0; k < Instructions.List.Count; k++)
                        {
                            if (args[0].ToUpper() == Instructions.List[k].Name)
                            {
                                for (int p = 0; p < Variables.Count; p++)
                                {
                                    for (int z = 0; z < args.Count; z++)
                                    {
                                        if (args[z] == Variables[p].Name)
                                        {
                                            if (Variables[p].Length <= 2)
                                            {
                                                string repl = "$";
                                                for (int n = 0; n < Variables[p].Length; n++) { repl += IntToHex(Variables[p].Data[n]); }
                                                LabelOutput[i] = LabelOutput[i].Replace(Variables[p].Name, repl);
                                            }
                                            // word
                                            else
                                            {
                                                if (args[0] == "STORA")
                                                {
                                                    int addr = ParseArgument(args[2]) + Variables[p].Length - 1;
                                                    LabelOutput[i] = "";
                                                    for (int n = Variables[p].Length - 1; n >= 0; n--)
                                                    {
                                                        LabelOutput.Insert(i, "STORA $" + IntToHex(Variables[p].Data[n]) + ", $" + IntToHex(addr));
                                                        addr--;
                                                    }
                                                }
                                                else if (args[0] == "PRINT")
                                                {
                                                    if (args[1] == Variables[p].Name)
                                                    {
                                                        int addr = ParseArgument(args[2]);
                                                        LabelOutput[i] = "";
                                                        LabelOutput.Insert(i, "PRINT $" + IntToHex(Variables[p].Length) + ", $" + IntToHex(addr));
                                                        break;
                                                    }
                                                }
                                                else if (args[0] == "PRINTL")
                                                {
                                                    if (args[1] == Variables[p].Name)
                                                    {
                                                        int addr = ParseArgument(args[2]);
                                                        LabelOutput[i] = "";
                                                        LabelOutput.Insert(i, "PRINTL $" + IntToHex(Variables[p].Length) + ", $" + IntToHex(addr));
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        // store variables into a list
        private static bool StoreVariables()
        {
            // clear list
            Variables.Clear();

            // parse lines
            for (int i = 0; i < Input.Count; i++)
            {
                // get current line
                string line = Input[i].Replace(",", " ");

                // get arguments
                List<string> args = StringArrayToList(line.Split(' '));

                // remove blank arguments
                for (int j = 0; j < args.Count; j++)
                {
                    args[j] = StringRemoveBlanks(args[j].ToCharArray());
                    if (IsBlank(args[j])) { args.RemoveAt(j); j--; }
                }

                // is line is commented or blank ?
                if (!line.StartsWith(";") && line.Length > 0)
                {
                    if (args[0] == "BYTE" || args[0] == "CHAR")
                    {
                        Variables.Add(new AssemblyVar(args[1], (byte)ParseArgument(args[2])));
                    }
                    else if (args[0] == "SHORT")
                    {
                        Variables.Add(new AssemblyVar(args[1], (ushort)ParseArgument(args[2])));
                    }
                    else if (args[0] == "WORD")
                    {
                        string var = "";
                        for (int n = 2; n < args.Count; n++)
                        {
                            var += args[n].Replace("\"", "");
                            if (n < args.Count - 1) { var += " "; }
                        }
                        Variables.Add(new AssemblyVar(args[1], var));
                    }
                }
            }
            return true;
        }

        // convert assembly code to bytecode
        private static bool AssembleCode(string file)
        {
            Output.Clear();

            // parse lines
            for (int i = 0; i < LabelOutput.Count; i++)
            {
                // get current line
                LabelOutput[i] = LabelOutput[i].Replace(",", "");
                string line = LabelOutput[i];

                // get arguments
                List<string> args = StringArrayToList(line.Split(' '));

                // remove blank arguments
                for (int j = 0; j < args.Count; j++)
                {
                    args[j] = StringRemoveBlanks(args[j].ToCharArray());
                    if (IsBlank(args[j])) { args.RemoveAt(j); j--; }
                }

                if (LabelOutput[i].Length > 1 && args[0] != "SHORT" && args[0] != "BYTE" && args[0] != "CHAR" && args[0] != "WORD")
                {
                    for (int j = 0; j < Instructions.List.Count; j++)
                    {
                        // is instruction valid?
                        if (args[0] == Instructions.List[j].Name)
                        {
                            // add instruction
                            Output.Add(Instructions.List[j].OpCode);
                            string format = Instructions.List[j].Format;
                            if (Instructions.List[j].Arguments == 0) { break; }

                            if (format == "b")
                            {
                                byte b = (byte)ParseArgument(args[1]);
                                Output.Add(b);
                            }
                            else if (format == "s")
                            {
                                ushort addr = (ushort)ParseArgument(args[1]);
                                byte left = (byte)((addr & 0xFF00) >> 8);
                                byte right = (byte)(addr & 0x00FF);
                                Output.Add(left);
                                Output.Add(right);
                            }
                            else if (format == "bb")
                            {
                                byte b1 = (byte)(ParseArgument(args[1]));
                                byte b2 = (byte)(ParseArgument(args[2]));
                                Output.Add(b1);
                                Output.Add(b2);
                            }
                            else if (format == "bs")
                            {
                                bool ignore = false;
                                for (int n = 0; n < Variables.Count; n++)
                                {
                                    if (args[1] == Variables[n].Name) { ignore = true; break; }
                                    else { ignore = false; }
                                }
                                if (!ignore)
                                {
                                    byte b = (byte)(ParseArgument(args[1]));
                                    ushort addr = (ushort)ParseArgument(args[2]);
                                    byte left = (byte)((addr & 0xFF00) >> 8);
                                    byte right = (byte)(addr & 0x00FF);
                                    Output.Add(b);
                                    Output.Add(left);
                                    Output.Add(right);
                                }
                            }
                            else if (format == "bbb")
                            {
                                byte b1 = (byte)(ParseArgument(args[1]));
                                byte b2 = (byte)(ParseArgument(args[2]));
                                byte b3 = (byte)(ParseArgument(args[3]));
                                Output.Add(b1);
                                Output.Add(b2);
                                Output.Add(b3);
                            }
                            else { CLI.Write("[ERROR] ", Color.Red); CLI.WriteLine("Invalid arguments at line " + i.ToString()); }
                            break;
                        }
                    }
                }
            }

            // write output
            CLI.WriteLine("Displaying binary output: ", Color.Gray);
            int l = CLI.CursorY;
            for (int j = 0; j < Output.Count; j++)
            {
                if (l >= CLI.Height) { Console.ReadLine(); Console.Clear(); l = 0; }
                CLI.Write(IntToHex(Output[j], "X2"), Color.Magenta);
                if (CLI.CursorX < 77) { CLI.Write(" "); }
                else { CLI.WriteLine(""); l++; }
            }
            CLI.WriteLine("");

            // save file
            PMFAT.WriteAllBytes(file, Output.ToArray());

            return true;
        }

        // convert argument to value
        public static int ParseArgument(string arg)
        {
            // hexadecimal value
            if (arg.StartsWith("$"))
            {
                // remove dollar sign
                string valueHex = arg.Replace("$", "");
                int num = 0;

                // register
                if (valueHex.StartsWith("R")) { num = HexToInt(valueHex.Replace("R", "")); }
                // absolute
                else { num = HexToInt(valueHex); }

                // integer value
                return num;
            }
            // char value
            else if (arg.StartsWith("\'"))
            {
                // remove apostrophes
                string valueChar = arg.Replace("\'", "");

                // char value 
                char c = StringToChar(valueChar);
                return (int)c;
            }
            // decimal register value
            else if (arg.StartsWith("R"))
            {
                string valueReg = arg.Replace("R", "");
                int num = StringToInt(valueReg);
                return num;
            }
            // decimal value
            else
            {
                int num = StringToInt(arg);
                return num;
            }
        }

        public static int LastIndexOf(string str, char c)
        {
            for (int i = 0; i < str.Length; i++)
                if (str[i] == c)
                    return i;
            return 0;
        }

        // convert string to utf-8 character
        private static char StringToChar(string val)
        {
            char c = (char)0;
            try
            {
                c = char.Parse(val);
            }
            catch (Exception ex)
            {
                CLI.Write("[ERROR] ", Color.Red); CLI.WriteLine("Could not convert value \"" + val + "\" to char.");
                CLI.Write("[DEBUG INFO] ", Color.Yellow); CLI.WriteLine(ex.Message, Color.Gray);
            }
            return c;
        }

        // convert string to byte
        private static int StringToInt(string val)
        {
            int output = 0;
            try
            {
                output = int.Parse(val);
            }
            catch (Exception ex)
            {
                CLI.Write("[ERROR] ", Color.Red); CLI.WriteLine("Could not convert value \"" + val + "\" to byte.");
                CLI.Write("[DEBUG INFO] ", Color.Yellow); CLI.WriteLine(ex.Message, Color.Gray);
            }
            return output;
        }

        // check if line is blank or white space
        private static bool IsBlank(string txt)
        {
            bool output = true;
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] > 32) { output = false; break; }
                else { output = true; }
            }
            return output;
        }

        // convert array of strings into list
        private static List<string> StringArrayToList(string[] array)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < array.Length; i++) { items.Add(array[i]); }
            return items;
        }

        // remove all of character in string
        private static string StringRemoveBlanks(char[] val)
        {
            string output = "";
            for (int i = 0; i < val.Length; i++)
            {
                if (val[i] > 32) { output += val[i]; }
            }
            return output;
        }


        // convert hex to string to integer
        public static int HexToInt(string hexVal)
        {
            int len = hexVal.Length;
            int base1 = 1;
            int dec_val = 0;

            for (int i = len - 1; i >= 0; i--)
            {
                if (hexVal[i] >= '0' &&
                    hexVal[i] <= '9')
                {
                    dec_val += (hexVal[i] - 48) * base1;
                    base1 = base1 * 16;
                }
                else if (hexVal[i] >= 'A' && hexVal[i] <= 'F')
                {
                    dec_val += (hexVal[i] - 55) * base1;
                    base1 = base1 * 16;
                }
            }
            return dec_val;
        }

        // convert integer to hex string
        public static string IntToHex(int decn)
        {
            string output = "";
            int q, dn = 0, m, l;
            int tmp;
            int s;
            q = decn;
            for (l = q; l > 0; l = l / 16)
            {
                tmp = l % 16;
                if (tmp < 10)
                    tmp = tmp + 48;
                else
                    tmp = tmp + 55;
                dn = dn * 100 + tmp;
            }
            for (m = dn; m > 0; m = m / 100)
            {
                s = m % 100;
                output += ((char)s).ToString();
            }
            if (output == "") { output = "0"; }
            return output;
        }

        // convert integer to hex string - formatted
        public static string IntToHex(int decn, string format)
        {
            if (format == "X1") { if (decn == 0) { return "0"; } }
            else if (format == "X2") { if (decn == 0) { return "00"; } }
            else if (format == "X3") { if (decn == 0) { return "000"; } }
            else if (format == "X4") { if (decn == 0) { return "0000"; } }

            string output = "";
            int q, dn = 0, m, l;
            int tmp;
            int s;
            q = decn;
            for (l = q; l > 0; l = l / 16)
            {
                tmp = l % 16;
                if (tmp < 10)
                    tmp = tmp + 48;
                else
                    tmp = tmp + 55;
                dn = dn * 100 + tmp;
            }
            for (m = dn; m > 0; m = m / 100)
            {
                s = m % 100;
                char c = (char)s;

                if (format == "X1")
                {
                    output += c.ToString();
                }
                else if (format == "X2")
                {
                    if (decn < 16) { output += "0" + c.ToString(); }
                    else { output += c.ToString(); }
                }
                else if (format == "X3")
                {
                    if (decn < 16) { output += "000" + c.ToString(); }
                    else if (decn >= 16 && decn < 256) { output += "00" + c.ToString(); }
                    else { output += c.ToString(); }
                }
                else if (format == "X4")
                {
                    if (decn < 16) { output += "000" + c.ToString(); }
                    else if (decn >= 16 && decn < 256) { output += "00" + c.ToString(); }
                    else if (decn >= 256 && decn < 4096) { output += "0" + c.ToString(); }
                    else { output += c.ToString(); }
                }
            }
            return output;
        }
    }
}
