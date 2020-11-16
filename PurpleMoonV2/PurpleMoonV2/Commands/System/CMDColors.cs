using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoonV2.Core;
using PurpleMoonV2.Hardware;
using PurpleMoonV2.Graphics;

namespace PurpleMoonV2.Commands
{
    public class CMDColors : Command
    {
        public CMDColors()
        {
            this.Name = "COLORS";
            this.Help = "Show all colors available to the display";
        }

        public override void Execute(string line, string[] args)
        {
            SetupScreen();
            DrawMessage();
            CLI.SetCursorPos(CLI.Width - 13, 0);
            CLI.ReadKey(true);
            Shell.DrawFresh();
        }

        private void SetupScreen()
        {
            CLI.ForceClear(Color.Black);
            Shell.DrawTitleBar();
            TextGraphics.FillRect(2, 2, CLI.Width - 4, CLI.Height - 3, ' ', Color.White, Color.Blue);
            CLI.SetCursorPos(2, 2);
        }

        private void DrawMessage()
        {
            TextGraphics.DrawLineH(2, 2, CLI.Width - 4, ' ', Color.White, Color.Cyan);
            TextGraphics.DrawString(2, 2, " C    COLOR                  VALUE", Color.Black, Color.Cyan);
            TextGraphics.DrawLineH(0, 3, CLI.Width, ' ', Color.White, Color.Black);
            int dx = 3, dy = 4;
            Color bg = Color.Black;
            // black
            TextGraphics.DrawChar(dx, dy, '#', Color.Black, bg);
            TextGraphics.DrawString(dx + 5, dy, "BLACK", Color.White, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.Black, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // dark gray
            TextGraphics.DrawChar(dx, dy, '#', Color.DarkGray, bg);
            TextGraphics.DrawString(dx + 5, dy, "DARK GRAY", Color.DarkGray, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.DarkGray, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // gray
            TextGraphics.DrawChar(dx, dy, '#', Color.Gray, bg);
            TextGraphics.DrawString(dx + 5, dy, "GRAY", Color.Gray, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.Gray, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // white
            TextGraphics.DrawChar(dx, dy, '#', Color.White, bg);
            TextGraphics.DrawString(dx + 5, dy, "WHITE", Color.White, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.White, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // dark red
            TextGraphics.DrawChar(dx, dy, '#', Color.DarkRed, bg);
            TextGraphics.DrawString(dx + 5, dy, "DARK RED", Color.DarkRed, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.DarkRed, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // red
            TextGraphics.DrawChar(dx, dy, '#', Color.Red, bg);
            TextGraphics.DrawString(dx + 5, dy, "RED", Color.Red, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.Red, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // dark yellow
            TextGraphics.DrawChar(dx, dy, '#', Color.DarkYellow, bg);
            TextGraphics.DrawString(dx + 5, dy, "DARK YELLOW", Color.DarkYellow, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.DarkYellow, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // yellow
            TextGraphics.DrawChar(dx, dy, '#', Color.Yellow, bg);
            TextGraphics.DrawString(dx + 5, dy, "YELLOW", Color.Yellow, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.Yellow, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // dark green
            TextGraphics.DrawChar(dx, dy, '#', Color.DarkGreen, bg);
            TextGraphics.DrawString(dx + 5, dy, "DARK GREEN", Color.DarkGreen, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.DarkGreen, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // green
            TextGraphics.DrawChar(dx, dy, '#', Color.Green, bg);
            TextGraphics.DrawString(dx + 5, dy, "GREEN", Color.Green, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.Green, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // dark cyan
            TextGraphics.DrawChar(dx, dy, '#', Color.DarkCyan, bg);
            TextGraphics.DrawString(dx + 5, dy, "DARK CYAN", Color.DarkCyan, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.DarkCyan, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // cyan
            TextGraphics.DrawChar(dx, dy, '#', Color.Cyan, bg);
            TextGraphics.DrawString(dx + 5, dy, "CYAN", Color.Cyan, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.Cyan, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // dark blue
            TextGraphics.DrawChar(dx, dy, '#', Color.DarkBlue, bg);
            TextGraphics.DrawString(dx + 5, dy, "DARK BLUE", Color.Blue, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.DarkBlue, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // blue
            TextGraphics.DrawChar(dx, dy, '#', Color.Blue, bg);;
            TextGraphics.DrawString(dx + 5, dy, "BLUE", Color.Blue, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.Blue, "X2"), Color.Yellow, Color.Blue);;
            dy++;
            // dark magenta
            TextGraphics.DrawChar(dx, dy, '#', Color.DarkMagenta, bg);
            TextGraphics.DrawString(dx + 5, dy, "DARK MAGENTA", Color.DarkMagenta, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.DarkMagenta, "X2"), Color.Yellow, Color.Blue);
            dy++;
            // magenta
            TextGraphics.DrawChar(dx, dy, '#', Color.Magenta, bg);
            TextGraphics.DrawString(dx + 5, dy, "MAGENTA", Color.Magenta, Color.Blue);
            TextGraphics.DrawString(dx + 28, dy, DataUtils.IntToHex((int)Color.Magenta, "X2"), Color.Yellow, Color.Blue);
            dy++;

            // exit message
            TextGraphics.DrawString(2, CLI.Height - 2, "Press any key to exit...", Color.White, Color.Blue);
        }
    }
}
