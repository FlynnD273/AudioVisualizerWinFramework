using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AudioVisualizerWinFramework
{
    class WindowSizeChangedEventArgs : EventArgs
    {
        public Size Size { get; private set; }

        public WindowSizeChangedEventArgs(Size s)
        {
            Size = s;
        }
    }
}
