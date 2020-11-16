using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Hardware;

namespace PurpleMoonV2.Graphics
{
    public static class TextGraphics
    {
        // set bg and clear screen
        public static void Clear(Color c) { CLI.BackColor = c; Console.Clear(); }

        // draw blank char at position
        public static void SetPixel(int x, int y, Color c) { DrawChar(x, y, ' ', Color.Black, c); }

        // draw horizontal line
        public static void DrawLineH(int x, int y, int w, char c, Color fg, Color bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }
            for (int i = 0; i < w; i++) { DrawChar(x + i, y, drawChar, fg, bg); }
        }

        // draw vertical line
        public static void DrawLineV(int x, int y, int h, char c, Color fg, Color bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }
            for (int i = 0; i < h; i++) { DrawChar(x, y + i, drawChar, fg, bg); }
        }

        // fill rectangle
        public static void FillRect(int x, int y, int w, int h, char c, Color fg, Color bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; } // if char is invalid, replace with blank

            // fill area with character
            for (int i = 0; i < w * h; i++)
            {
                int xx = x + (i % w);
                int yy = y + (i / w);
                if (xx * yy < CLI.Width * CLI.Height) { DrawChar(xx, yy, drawChar, fg, bg); }
            }
        }

        // draw rectangle
        public static void DrawRect(int x, int y, int w, int h, char c, Color fg, Color bg)
        {
            // if char is invalid, replace with blank
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }

            // horizontal lines
            DrawLineH(x, y, w, c, fg, bg);
            DrawLineH(x, y + h, w, c, fg, bg);

            // vertical lines
            DrawLineV(x, y, h, c, fg, bg);
            DrawLineV(x + w, y, h, c, fg, bg);

            // if drawn to the very last location in the buffer(bottom, right)
            // pixel will not be visible, otherwise the screen will auto scroll and shift everything up 1 row
        }

        // draw character to position on screen
        public static bool DrawChar(int x, int y, char c, Color fg, Color bg)
        {
            if (x >= 0 && x < CLI.Width && y >= 0 && y < CLI.Height)
            {
                int oldX = CLI.CursorX, oldY = CLI.CursorY;
                CLI.SetCursorPos(x, y);
                CLI.Write(c.ToString(), fg, bg);
                CLI.SetCursorPos(oldX, oldY);
                return true;
            }
            else { return false; }
        }

        // draw string to position on screen
        public static void DrawString(int x, int y, string txt, Color fg, Color bg)
        {
            for (int i = 0; i < txt.Length; i++) { DrawChar(x + i, y, txt[i], fg, bg); }
        }
    }
}
