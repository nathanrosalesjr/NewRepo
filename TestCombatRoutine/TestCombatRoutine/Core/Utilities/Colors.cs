using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM = System.Windows.Media;

namespace TestCombatRoutine.Core.Utilities
{
    public class Colors
    {
        private static System.Windows.Media.Color colorFromName(System.Drawing.Color color)
        {
            return WM.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private static WM.Color firebrick;
        public static WM.Color Firebrick => firebrick != null ? firebrick : firebrick = colorFromName(System.Drawing.Color.Firebrick);

        private static WM.Color orangeRed;
        public static WM.Color OrangeRed => orangeRed != null ? orangeRed : orangeRed = colorFromName(System.Drawing.Color.OrangeRed);
        
    }
}
