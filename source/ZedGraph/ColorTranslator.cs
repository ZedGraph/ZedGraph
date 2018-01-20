using System.Drawing;

namespace ZedGraph
{
    internal class ColorTranslator
    {
        private const int _win32RedShift = 0;
        private const int _win32GreenShift = 8;
        private const int _win32BlueShift = 16;

        public static int ToWin32(Color c)
        {
            return c.R << _win32RedShift | c.G << _win32GreenShift | c.B << _win32BlueShift;
        }
    }
}
