﻿using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace AudioVisualizerWinFramework
{
    class RenderBasicFreq : RenderBase
    {
        public RenderBasicFreq(Settings s, string n) : base(s, n) 
        {
            Settings.Colors.Add(new NamedColor("Left", Color.BlueViolet));
            Settings.Colors.Add(new NamedColor("Right", Color.OrangeRed));
            Settings.Colors.Add(new NamedColor("Top", Color.ForestGreen));
        }

        public override void Render(Graphics g, float[] samples)
        {
            float[] heights = FFT.SampleToFreq(samples, Settings.SampleCount);

            List<PointF> points = new List<PointF>();

            points.Add(new PointF(0,  Settings.WindowSize.Height));

            for (int i = 0; i < heights.Length; i++)
            {
                float height = Smooth(heights, i, Settings.Smoothing);
                points.Add(new PointF(i / (float)heights.Length *  Settings.WindowSize.Width * Settings.XScale, (float)(Settings.WindowSize.Height - 20.0f - height * Settings.YScale)));

                if (i / (float)heights.Length *  Settings.WindowSize.Width * Settings.XScale >  Settings.WindowSize.Width)
                {
                    break;
                }
            }

            points.Add(new PointF( Settings.WindowSize.Width,  Settings.WindowSize.Height));

            LinearGradientBrush b = new LinearGradientBrush(new PointF(0, 0), new PointF( Settings.WindowSize.Width, 0), Settings.GetColor("Left"), Settings.GetColor("Right"));

            g.FillPolygon(b, points.ToArray());

            b = new LinearGradientBrush(new PointF(0, 0), new PointF(0,  Settings.WindowSize.Height), Settings.GetColor("Top"), Color.FromArgb(0, Settings.GetColor("Top")));

            g.FillPolygon(b, points.ToArray());
        }
    }
}
