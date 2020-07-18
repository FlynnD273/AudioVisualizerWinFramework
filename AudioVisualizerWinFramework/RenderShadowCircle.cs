using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using ColorMine.ColorSpaces;
using System.Linq;

namespace AudioVisualizerWinFramework
{
    class RenderShadowCircle : RenderBase
    {
        public RenderShadowCircle(Settings s, string n) : base(s, n) 
        {
            Settings.Colors.Add(new NamedColor("Inner Shadow", Color.BlueViolet));
            Settings.Colors.Add(new NamedColor("Outer Shadow", Color.Black));
            Settings.Colors.Add(new NamedColor("Center", Color.Black));
        }

        public override void Render(Graphics g, float[] samples)
        {
            float[] heights = FFT.SampleToFreq(samples, Settings.SampleCount);
            GetLastIndex(heights);

            DrawOutline(g, heights);
            DrawCircle(g, heights);
        }
        
        private void DrawOutline(Graphics g, float[] heights)
        {
            List<PointF> points = GetCircularPoints(heights, 3.0f, g, 200f);
            
            PathGradientBrush b = new PathGradientBrush(points.ToArray());
            b.CenterColor = Settings.GetColor("Inner Shadow");
            b.SurroundColors = new Color[] { Settings.GetColor("Outer Shadow") };

            g.FillPolygon(b, points.ToArray());
        }

        private void DrawCircle(Graphics g, float[] heights)
        {
            List<PointF> points = GetCircularPoints(heights, 1.0f, g);
            g.FillPolygon(new SolidBrush(Settings.GetColor("Center")), points.ToArray());
        }
    }
}
