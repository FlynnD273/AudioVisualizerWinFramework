using ColorMine.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AudioVisualizerWinFramework
{
    class HSVColor : Hsv
    {
        public HSVColor (double h, double s, double v)
        {
            H = h;
            S = s;
            V = v;
        }
        public HSVColor(Hsv hsv)
        {
            H = hsv.H;
            S = hsv.S;
            V = hsv.V;
        }

        public Color ToColor()
        {
            Rgb col = To<Rgb>();
            HSVColor temp = new HSVColor(H, S, V);
            return Color.FromArgb(255, (int)col.R, (int)col.G, (int)col.B);
        }
    }
}
