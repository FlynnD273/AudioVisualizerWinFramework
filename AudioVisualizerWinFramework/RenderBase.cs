using ColorMine.ColorSpaces;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AudioVisualizerWinFramework
{
    abstract class RenderBase
    {
        protected double maxAngle { get; set; }
        protected float maxHeight { get; set; }
        protected int lastIndex { get; set; }

        public Settings Settings { get; set; }
        public string Name { get; private set; }

        public RenderBase (Settings s, string n)
        {
            Settings = s;
            Name = n;
        }

        public abstract void Render(Graphics g, float[] samples);
        protected int GetLastIndex(float[] heights)
        {
            return lastIndex = (int)Math.Min((heights.Length / Settings.XScale), heights.Length);
        }

        protected List<PointF> GetCircularPoints(float[] heights, float scale, Graphics g, float radius = -1)
        {
            if (radius == -1)
            {
                radius = Math.Min(Settings.WindowWidth, Settings.WindowHeight) / 8;
            }

            List<PointF> points = new List<PointF>();
            maxAngle = 0;
            maxHeight = 0;

            for (int i = 0; i < lastIndex; i++)
            {
                double angle = (double)i / heights.Length * Math.PI * 2 * Settings.XScale;

                float height = SmoothCircular(heights, i, Settings.Smoothing, lastIndex) * scale * Settings.YScale;

                double x = Math.Cos(angle - Math.PI / 2) * (height + radius) +  Settings.WindowSize.Width / 2;
                double y = Math.Sin(angle - Math.PI / 2) * (height + radius) +  Settings.WindowSize.Height / 2;

                points.Add(new PointF((float)x, (float)y));

                if (height > maxHeight)
                {
                    maxHeight = height;
                    maxAngle = angle;
                }
            }

            return points;
        }
        protected static List<Color> MakeRainbow(int length, int alpha, float val = 1)
        {
            List<Color> colors = new List<Color>();

            for (int i = 0; i < length; i++)
            {
                Hsv color = new Hsv { H = (double)i / length * 360, S = 1, V = val };
                IRgb rgb = color.ToRgb();
                colors.Add(Color.FromArgb(alpha, (int)rgb.R, (int)rgb.G, (int)rgb.B));
            }

            return colors;
        }

        protected float SmoothCircular(float[] heights, int index, int step, int lastIndex)
        {
            int sampleCount = step;
            if (sampleCount < 3)
            {
                return heights[index];
            }

            double[] k = Window.Cosine(sampleCount);
            float result = 0;
            float total = 0;
            for (int i = 0; i < sampleCount; i++)
            {
                result += (float)(heights[(i + index) % lastIndex] * k[i]);
                total += (float)k[i];
            }

            result /= total;

            return result;
        }

        protected float Smooth(float[] heights, int index, int step)
        {
            int sampleCount = Math.Min(heights.Length - index - 1, step);
            if (sampleCount < 3)
            {
                return heights[index];
            }

            double[] k = Window.Cosine(sampleCount);
            float result = 0;
            float total = 0;
            for (int i = 0; i < sampleCount; i++)
            {
                result += (float)(heights[i + index] * k[i]);
                total += (float)k[i];
            }

            result /= total;

            return result;
        }
    }
}
