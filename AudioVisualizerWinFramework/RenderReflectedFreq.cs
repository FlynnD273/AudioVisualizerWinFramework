using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace AudioVisualizerWinFramework
{
    class RenderReflectedFreq : RenderBase
    {
        public RenderReflectedFreq(Settings s, string n) : base(s, n) 
        {
            Settings.Colors.Add(new NamedColor("Left", Color.BlueViolet));
            Settings.Colors.Add(new NamedColor("Right", Color.OrangeRed));
            Settings.Colors.Add(new NamedColor("Top", Color.White));
            Settings.Colors.Add(new NamedColor("Bottom", Color.Black));
        }

        public override void Render(Graphics g, float[] samples)
        {
            float[] heights = FFT.SampleToFreq(samples, Settings.SampleCount);

            List<PointF> points = new List<PointF>();
            List<PointF> pointsReflected = new List<PointF>();

            points.Add(new PointF(0,  Settings.WindowSize.Height / 2));
            pointsReflected.Add(new PointF(0,  Settings.WindowSize.Height / 2));

            for (int i = 0; i < heights.Length; i++)
            {
                float height = Smooth(heights, i, Settings.Smoothing);

                points.Add(new PointF(i / (float)heights.Length *  Settings.WindowSize.Width * Settings.XScale, (float)( Settings.WindowSize.Height / 2 - height * Settings.YScale)));
                pointsReflected.Add(new PointF(i / (float)heights.Length *  Settings.WindowSize.Width * Settings.XScale, (float)( Settings.WindowSize.Height / 2 + height * Settings.YScale)));


                if (i / (float)heights.Length *  Settings.WindowSize.Width * Settings.XScale >  Settings.WindowSize.Width)
                {
                    break;
                }
            }

            points.Add(new PointF( Settings.WindowSize.Width,  Settings.WindowSize.Height / 2));
            pointsReflected.Add(new PointF( Settings.WindowSize.Width,  Settings.WindowSize.Height / 2));

            LinearGradientBrush b = new LinearGradientBrush(new PointF(0, 0), new PointF(Settings.WindowSize.Width, 0), Settings.GetColor("Left"), Settings.GetColor("Right"));

            List<PointF> totalShape = new List<PointF>();
            totalShape.AddRange(points);
            totalShape.AddRange(pointsReflected);

            g.FillPolygon(b, totalShape.ToArray());

            //Draw top

            b = new LinearGradientBrush(new PointF(0, 0), new PointF(0, Settings.WindowSize.Height / 2 + 5), Settings.GetColor("Top"), Color.FromArgb(0, Settings.GetColor("Top")));
            b.WrapMode = WrapMode.TileFlipY;

            g.FillPolygon(b, points.ToArray());

            //Draw bottom

            b = new LinearGradientBrush(new PointF(0, Settings.WindowSize.Height / 2 - 1), new PointF(0,  Settings.WindowSize.Height), Color.FromArgb(0, Settings.GetColor("Bottom")), Settings.GetColor("Bottom"));
            b.WrapMode = WrapMode.TileFlipY;

            g.FillPolygon(b, pointsReflected.ToArray());
        }
    }
}
