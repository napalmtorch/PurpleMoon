using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL.Drivers.PCI.Video;

using MelodyOS.Graphics;

namespace MelodyOS.Hardware
{
    public static class SVGADriver
    {
        public static VMWareSVGAII Driver;
        public static bool HasChanged { get; private set; }

        public static uint[] Buffer;
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        public static bool Initialize()
        {
            try
            {
                // init display
                Driver = new VMWareSVGAII();
                SetResolution(512, 384, true);
                return true;
            }
            catch (Exception ex)
            {
                // error
                return false;
            }
        }

        public static void Clear(Color c)
        {
            for (int i = 0; i < Width * Height; i++) { Buffer[i] = ColorToUInt(c); }
        }

        public static void SetResolution(int w, int h, bool apply)
        {
            Width = w; Height = h;

            if (apply)
            {
                Buffer = new uint[Width * Height];
                Driver.SetMode((uint)Width, (uint)Height);
                HasChanged = true;
            }
        }

        public static void Draw()
        {
            if (HasChanged)
            {
                for (int i = 0; i < Width * Height; i++)
                {
                    Driver.SetPixel((uint)(i % Width), (uint)(i / Width), Buffer[i]);
                }
                Driver.Update(0, 0, (uint)Width, (uint)Height);
                HasChanged = false;
            }
        }

        public static bool SetPixel(int x, int y, Color c)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                int i = x + (y * Width);
                uint col = ColorToUInt(c);
                if (Buffer[i] != col) { Buffer[i] = col; }
                HasChanged = true;
                return true;
            }
            else { return false; }
        }

        public static bool SetPixel(int i, Color c)
        {
            if (i < Width * Height)
            {
                Buffer[i] = ColorToUInt(c);
                HasChanged = true;
                return true;
            }
            else { return false; }
        }

        public static void PutChar(int x, int y, char c, Color fg, Font font)
        {
            int cw = font.CharWidth, ch = font.CharHeight;
            FontCharacter fontChar = Font.CharToFontChar(c);
            for (int yy = 0; yy < ch; yy++)
            {
                for (int xx = 0; xx < cw; xx++)
                {
                    if (font.Characters[(int)fontChar][xx + (yy * cw)] == 1)
                    {
                        SetPixel(x + xx, y + yy, fg);
                    }
                }
            }
        }

        public static void PutChar(int x, int y, char c, Color fg, Color bg, Font font)
        {
            int cw = font.CharWidth, ch = font.CharHeight;
            FontCharacter fontChar = Font.CharToFontChar(c);
            for (int yy = 0; yy < ch; yy++)
            {
                for (int xx = 0; xx < cw; xx++)
                {
                    if (font.Characters[(int)fontChar][xx + (yy * cw)] == 1)
                    { SetPixel(x + xx, y + yy, fg); }
                    else if (bg != Drawing.BackColor) { SetPixel(x + xx, y + yy, fg); }
                }
            }
        }

        public static void DrawString(int x, int y, string txt, Color fg, Font font)
        {
            int xx = x, yy = y;
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] != '\n') { PutChar(xx, yy, txt[i], fg, font); xx += font.CharWidth; }
                else { yy += font.CharHeight; xx = x; }
            }
        }

        public static void DrawString(int x, int y, string txt, Color fg, Color bg, Font font)
        {
            int xx = x, yy = y;
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] != '\n') { PutChar(xx, yy, txt[i], fg, bg, font); xx += font.CharWidth; }
                else { yy += font.CharHeight; xx = x; }
            }
        }

        public static void DrawBitmap(int x, int y, int w, int h, uint[] bmp)
        {
            for (int i = 0; i < w * h; i++)
            {
                int sx = x + (i % w), sy = y + (i / w);
                int index = (sx + (sy * Width));
                Buffer[index] = bmp[i];
            }
            HasChanged = true;
        }

        public static void DrawBitmap(int x, int y, int w, int h, Color c, uint[] bmp)
        {
            for (int i = 0; i < w * h; i++)
            {
                int sx = x + (i % w), sy = y + (i / w);
                int index = (sx + (sy * Width));
                if (bmp[i] > 0) { Buffer[index] = ColorToUInt(c); }
            }
            HasChanged = true;
        }

        public static uint ColorToUInt(Color c)
        {
            return (uint)(256 * 256 * c.R + 256 * c.G + c.B);
        }
    }
}
