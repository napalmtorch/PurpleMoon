using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Graphics;
using PurpleMoonV2.Hardware;

namespace PurpleMoonV2.Core
{
    public static class DataUtils
    {
        // convert integer to boolean
        public static bool IntToBool(int val)
        {
            if (val > 0) { return true; }
            else { return false; }
        }

        // last index of character in string
        public static int LastIndexOf(string str, char c)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c) { return i; }
            }
            return 0;
        }

        // convert string to utf-8 character
        public static char StringToChar(string val)
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
        public static int StringToInt(string val)
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
        public static bool IsBlank(string txt)
        {
            bool output = true;
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] > 32) { output = false; break; }
                else { output = true; }
            }
            return output;
        }

        // remove all of character in string
        public static string StringRemoveBlanks(char[] val)
        {
            string output = "";
            for (int i = 0; i < val.Length; i++)
            {
                if (val[i] > 32) { output += val[i]; }
            }
            return output;
        }

        // convert array of strings into list
        public static List<string> StringArrayToList(string[] array)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < array.Length; i++) { items.Add(array[i]); }
            return items;
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
