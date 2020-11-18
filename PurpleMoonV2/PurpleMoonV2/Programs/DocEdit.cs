using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Programs
{
    public static class DocEdit
    {
        // dimensions
        static int StartX = 0, StartY = 1;
        static int Width = CLI.Width, Height = CLI.Height - 2;
        static int ScreenX, ScreenY, DocX, DocY;
        static int Scroll = 0;
        static string CurrentFile = "";

        // document
        static List<string> Lines = new List<string>();

        // menu
        static int MenuIndex;

        public static void Start()
        {
            // clear screen & draw top bar
            Shell.TitleBarText = "TextEdit";
            DrawFresh();

            // clear doc
            Clear(true);

            // cursor start pos
            SetScreenPos(StartX, StartY);
            SendScreenPos();

            // run main loop
            Draw();
            ReadInput();
        }

        public static void Start(string file)
        {
            // clear screen & draw top bar
            Shell.TitleBarText = "TextEdit";
            DrawFresh();

            // clear doc
            Clear(true);

            // cursor start pos
            SetScreenPos(StartX, StartY);
            SendScreenPos();
            LoadFile(file);
        }

        public static void DrawFresh()
        {
            CLI.ForceClear(Color.White);
            Shell.DrawTitleBar();
            CLI.SetCursorPos(0, 1);
        }

        public static void Exit()
        {
            MenuIndex = 0;
            Clear(false);
            SystemInfo.LoadConfig(PMFAT.ConfigFile, true);
            Shell.DrawFresh();
            Shell.GetInput();
        }

        private static void Clear(bool nl)
        {
            Lines.Clear();
            if (nl) { Lines.Add(""); }
            SetScreenPos(StartX, StartY); 
            SetDocPos(0, 0);
            Scroll = 0;
            CurrentFile = "";
            Draw();
        }

        private static void SendScreenPos() { CLI.SetCursorPos(ScreenX, ScreenY); }
        private static void SetScreenPos(int x, int y) { ScreenX = x; ScreenY = y; }
        private static void SetDocPos(int x, int y) { DocX = x; DocY = y; }

        private static void ReadInput()
        {
            try
            {
                ConsoleKeyInfo key = CLI.ReadKey(true);

                // character
                if (key.KeyChar >= 32 && key.KeyChar < 127) { KeyCharPressed(key); }

                // backspace
                else if (key.Key == ConsoleKey.Backspace) { BackspacePressed(); }

                // enter
                else if (key.Key == ConsoleKey.Enter) { NewLine(); DocX = 0; DocY++; }

                // end of line
                else if (key.Key == ConsoleKey.End) { DocX = Lines[DocY].Length; ScreenX = DocX; }

                // delete char
                else if (key.Key == ConsoleKey.Delete) { DeleteCurrentChar(); }

                // tab indent
                else if (key.Key == ConsoleKey.Tab) { AddTabIndent(); }

                // navigation
                else if (key.Key == ConsoleKey.UpArrow) { MoveUp(); }
                else if (key.Key == ConsoleKey.DownArrow) { MoveDown(); }
                else if (key.Key == ConsoleKey.LeftArrow) { MoveLeft(); }
                else if (key.Key == ConsoleKey.RightArrow) { MoveRight(); }

                // exit
                else if (key.Key == ConsoleKey.Escape) { ReadInputMenu(); }

                // repeat
                Draw();
                ReadInput();
            }
            catch (Exception ex)
            {
                Clear(false);
                CLI.Write("[FATAL] ", Color.Red);
                CLI.WriteLine(ex.Message, Color.White);
            }
        }

        private static void ReadInputMenu()
        {
            // draw document
            Draw();

            // draw menu
            TextGraphics.FillRect(0, 1, 20, 5, ' ', Color.White, Shell.TitleBarColor);
            TextGraphics.DrawString(1, 1, "New", Color.White, Shell.TitleBarColor);
            TextGraphics.DrawString(1, 2, "Open...", Color.White, Shell.TitleBarColor);
            TextGraphics.DrawString(1, 3, "Save", Color.White, Shell.TitleBarColor);
            TextGraphics.DrawString(1, 4, "Save As...", Color.White, Shell.TitleBarColor);
            TextGraphics.DrawString(1, 5, "Exit", Color.White, Shell.TitleBarColor);

            TextGraphics.DrawChar(0, StartY + MenuIndex, '>', Color.Yellow, Shell.TitleBarColor);
            CLI.SetCursorPos(0, StartY + MenuIndex);

            try
            {
                ConsoleKeyInfo key = CLI.ReadKey(true);

                // return to document
                if (key.Key == ConsoleKey.Escape) { Draw(); ReadInput(); }

                // move up
                else if (key.Key == ConsoleKey.UpArrow) { if (MenuIndex > 0) { MenuIndex--; } }
                // move down
                else if (key.Key == ConsoleKey.DownArrow) { if (MenuIndex < 4) { MenuIndex++; } }

                // select option
                else if (key.Key == ConsoleKey.Enter)
                {
                    // new document
                    if (MenuIndex == 0) { Clear(true); }

                    // load document
                    if (MenuIndex == 1) { Load(); }

                    // save document
                    if (MenuIndex == 2)
                    {
                        if (PMFAT.FileExists(CurrentFile)) { Save(); }
                        else { SaveAs(); }
                    }

                    // save document as
                    if (MenuIndex == 3) { SaveAs(); }

                    // exit
                    if (MenuIndex == 4) { Exit(); }

                    Draw();
                    ReadInput();
                }

                ReadInputMenu();
            }
            catch (Exception ex)
            {
                Clear(false);
                CLI.Write("[FATAL] ", Color.Red);
                CLI.WriteLine(ex.Message, Color.White);
            }
        }

        private static void Draw()
        {
            // draw fresh
            DrawFresh();

            // draw document
            for (int i = 0; i < CLI.Height; i++)
            {
                int pos = i + Scroll;
                if (pos < Lines.Count)
                {
                    TextGraphics.DrawString(StartX, StartY + i, Lines[pos], Color.Black, Color.White);
                }
            }

            // draw doc pos
            TextGraphics.DrawString(Shell.TitleBarTime.Length, 0, " | CUR: " + DocX.ToString() + "   LINE:" + DocY.ToString(), Shell.TitleColor, Shell.TitleBarColor);

            // return cursor position to document
            SendScreenPos();
        }

        private static void KeyCharPressed(ConsoleKeyInfo key)
        {
            if (key.KeyChar >= 32 && key.KeyChar < 127)
            {
                if (Lines[DocY].Length >= CLI.Width) { NewLine(); DocY++; DocX = 0; }
                if (DocX < Lines[DocY].Length) { Lines[DocY] = Lines[DocY].Insert(DocX, key.KeyChar.ToString()); }
                else { Lines[DocY] += key.KeyChar.ToString(); }
                DocX++; ScreenX++;
            }
        }

        private static void BackspacePressed()
        {
            if (!RemoveLast())
            {
                if (DocY > 0)
                {
                    bool remove = true;
                    if (Lines[DocY].Length > 0) { if (Lines.Count > 0) { Lines.RemoveAt(DocY - 1); remove = false; } }
                    else { Lines.RemoveAt(DocY); }
                    DocY--;
                    if (Scroll > 0) { Scroll--; }
                    else { ScreenY--; }
                    if (remove)
                    {
                        DocX = Lines[DocY].Length;
                        ScreenX = Lines[DocY].Length;
                        RemoveLast(); 
                    }
                }
            }
        }

        private static bool RemoveLast()
        {
            if (DocX > 0)
            {
                Lines[DocY] = Lines[DocY].Remove(DocX - 1, 1);
                DocX--;
                ScreenX--;
                return true;
            }
            else { return false; }
        }

        private static void NewLine()
        {
            if (DocX < Lines[DocY].Length)
            {
                string part1 = Lines[DocY].Substring(0, DocX);
                string part2 = Lines[DocY].Substring(DocX, Lines[DocY].Length - DocX);
                Lines[DocY] = part1;
                Lines.Insert(DocY + 1, "");
                Lines[DocY + 1] = part2;
            }
            else { Lines.Insert(DocY + 1, ""); }
            ScreenX = 0;
            if (ScreenY >= CLI.Height - 1) { Scroll++; }
            else { ScreenY++; }
            SendScreenPos();
        }

        private static void MoveUp()
        {
            if (DocY > 0)
            {
                if (Scroll > 0) { Scroll--; }
                else { ScreenY--; }
                DocY--;
                if (DocX >= Lines[DocY].Length) { DocX = Lines[DocY].Length; ScreenX = DocX; }
            }
        }

        private static void MoveDown()
        {
            if (DocY < Lines.Count - 1)
            {
                if (ScreenY >= CLI.Height - 1) { Scroll++; }
                else { ScreenY++; }
                DocY++;
                if (DocX >= Lines[DocY].Length) { DocX = Lines[DocY].Length; ScreenX = DocX; }
            }
            else
            {
                if (DocX >= Lines[DocY].Length) { DocX = Lines[DocY].Length; ScreenX = DocX; }
            }
        }

        private static void MoveLeft()
        {
            if (DocX > 0) { DocX--; ScreenX--; }
            if (DocX >= Lines[DocY].Length) { DocX = Lines[DocY].Length; ScreenX = DocX; }
        }

        private static void MoveRight()
        {
            if (DocX < Lines[DocY].Length) { DocX++; ScreenX++; }
            if (DocX >= Lines[DocY].Length) { DocX = Lines[DocY].Length; ScreenX = DocX; }
        }

        private static void AddTabIndent()
        {
            if (Lines[DocY].Length < Width - 4)
            {
                Lines[DocY] = Lines[DocY].Insert(DocX, "    ");
                DocX += 4;
                ScreenX = DocX;
            }
        }

        private static void DeleteCurrentChar()
        {
            if (Lines[DocY].Length > 0) { Lines[DocY] = Lines[DocY].Remove(DocX, 1); }
        }

        public static void LoadFile(string file)
        {
            // format filename
            if (!file.StartsWith(@"0:\")) { file = @"0:\" + file; }

            // load file
            bool error = false;
            if (PMFAT.FileExists(file))
            {
                try
                {
                    string[] lines = PMFAT.ReadLines(file);
                    Clear(false);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        Lines.Add(lines[i]);
                    }
                    TextGraphics.DrawLineH(0, StartY, Width, ' ', Color.White, Color.Black);
                    TextGraphics.DrawString(0, StartY, "Successfully loaded \"" + file + "\"", Color.Green, Color.Black);
                    error = false;
                }
                catch (Exception ex) { error = true; }
            }
            else { error = true; }

            if (error)
            {
                TextGraphics.DrawLineH(0, StartY, Width, ' ', Color.White, Color.Black);
                TextGraphics.DrawString(0, StartY, "Unable to load file \"" + file + "\"", Color.Red, Color.Black);
            }

            if (!error) { CurrentFile = file; }
            Kernel.Delay(500);
            Draw();
            ReadInput();
        }

        private static void Load()
        {
            // draw
            Draw();
            TextGraphics.DrawLineH(0, StartY, Width, ' ', Color.White, Color.Black);
            TextGraphics.DrawString(0, StartY, "FILENAME: ", Color.White, Color.Black);
            CLI.SetCursorPos(10, StartY);

            // get file
            string file = Console.ReadLine();

            // load file
            LoadFile(file);
        }

        private static void Save()
        {
            // save file
            string document = "";

            try
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    document += Lines[i] + "\n";
                }
                PMFAT.WriteAllText(CurrentFile, document);
                TextGraphics.DrawLineH(0, StartY, Width, ' ', Color.White, Color.Black);
                TextGraphics.DrawString(0, StartY, "Successfully saved \"" + CurrentFile + "\"", Color.Green, Color.Black);
            }
            catch (Exception ex)
            {
                TextGraphics.DrawLineH(0, StartY, Width, ' ', Color.White, Color.Black);
                TextGraphics.DrawString(0, StartY, "Unable to save file \"" + CurrentFile + "\"", Color.Red, Color.Black);
            }

            // finish
            Kernel.Delay(500);
            Draw();
        }

        private static void SaveAs()
        {
            // draw
            Draw();
            TextGraphics.DrawLineH(0, StartY, Width, ' ', Color.White, Color.Black);
            TextGraphics.DrawString(0, StartY, "FILENAME: ", Color.White, Color.Black);
            CLI.SetCursorPos(10, StartY);
            
            // get file
            string file = Console.ReadLine();

            // format filename
            if (!file.StartsWith(@"0:\")) { file = @"0:\" + file; }

            // save file
            string document = "";
            try
            { 
                for (int i = 0; i < Lines.Count; i++)
                {
                    document += Lines[i] + "\n";
                }
                PMFAT.WriteAllText(file, document);
                TextGraphics.DrawLineH(0, StartY, Width, ' ', Color.White, Color.Black);
                TextGraphics.DrawString(0, StartY, "Successfully saved \"" + file + "\"", Color.Green, Color.Black);
            }
            catch (Exception ex)
            {
                TextGraphics.DrawLineH(0, StartY, Width, ' ', Color.White, Color.Black);
                TextGraphics.DrawString(0, StartY, "Unable to save file \"" + file + "\"", Color.Red, Color.Black);
            }

            // finish
            CurrentFile = file;
            Kernel.Delay(500);
            Draw();
        }
    }
}
