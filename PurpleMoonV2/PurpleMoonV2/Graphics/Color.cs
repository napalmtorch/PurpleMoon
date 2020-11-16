using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoonV2.Graphics
{
    public enum Color
    {
        // this class only really exists for ease of use as I found ConsoleColor to be a long name to type constantly
        // literally just copying ConsoleColor to a different enum

        Black = (int)ConsoleColor.Black,
        DarkGray = (int)ConsoleColor.DarkGray,
        Gray = (int)ConsoleColor.Gray,
        White = (int)ConsoleColor.White,
        DarkRed = (int)ConsoleColor.DarkRed,
        Red = (int)ConsoleColor.Red,
        DarkBlue = (int)ConsoleColor.DarkBlue,
        Blue = (int)ConsoleColor.Blue,
        DarkGreen = (int)ConsoleColor.DarkGreen,
        Green = (int)ConsoleColor.Green,
        DarkCyan = (int)ConsoleColor.DarkCyan,
        Cyan = (int)ConsoleColor.Cyan,
        DarkMagenta = (int)ConsoleColor.DarkMagenta,
        Magenta = (int)ConsoleColor.Magenta,
        DarkYellow = (int)ConsoleColor.DarkYellow,
        Yellow = (int)ConsoleColor.Yellow,
    }
}
