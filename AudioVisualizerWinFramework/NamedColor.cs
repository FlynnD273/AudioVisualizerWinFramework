using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AudioVisualizerWinFramework
{
    class NamedColor
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public NamedColor(string n, Color c)
        {
            Name = n;
            Color = c;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
