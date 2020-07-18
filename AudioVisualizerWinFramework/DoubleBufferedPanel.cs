using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AudioVisualizerWinFramework
{
    public partial class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel() : base()
        {
            DoubleBuffered = true;
        }

        protected override void OnPaint (PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
    }
}
