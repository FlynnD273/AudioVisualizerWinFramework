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
    class RenderRainbowCircle : RenderBase
    {
        public RenderRainbowCircle(Settings s, string n) : base(s, n) { }

        public override void Render(Graphics g, float[] samples)
        {
            float[] heights = FFT.SampleToFreq(samples, Settings.SampleCount);
            GetLastIndex(heights);

            DrawOutline(g, heights, 3f);
            DrawCircle(g, heights);
        }

        private void DrawOutline(Graphics g, float[] heights, float scale)
        {
            List<PointF> outerEdge = GetCircularPoints(heights, scale, g);

            PathGradientBrush b = new PathGradientBrush(outerEdge.ToArray());

            Hsv color = new Hsv { H = (double)maxAngle * 180 / Math.PI, S = 1, V = 1 };
            IRgb rgb = color.ToRgb();

            b.CenterColor = Color.FromArgb(255, (int)rgb.R, (int)rgb.G, (int)rgb.B);
            b.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };

            Region rgn = new Region();

            g.FillPolygon(b, outerEdge.ToArray());
        }

        private void DrawCircle(Graphics g, float[] heights)
        {
            List<PointF> points = GetCircularPoints(heights, 1, g);

            PathGradientBrush b = new PathGradientBrush(points.ToArray());

            b.CenterColor = Color.FromArgb(0, Color.White);
            List<Color> colors = MakeRainbow(lastIndex, 255);

            b.SurroundColors = colors.ToArray();

            g.FillPolygon(b, points.ToArray());
        }
    }
}
