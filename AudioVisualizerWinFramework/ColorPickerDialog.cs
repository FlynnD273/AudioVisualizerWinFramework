using ColorMine.ColorSpaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace AudioVisualizerWinFramework
{
    public partial class ColorPickerDialog : Form
    {
        private HSVColor _selectedColor;
        public Color Color
        {
            get
            {
                return _selectedColor.ToColor();
            }
        }

        private RectangleF hueBounds;
        private RectangleF satValBounds;

        private DoubleBufferedPanel satValPanel;
        private DoubleBufferedPanel huePanel;
        private DoubleBufferedPanel colorPanel;

        public ColorPickerDialog(Color color)
        {
            InitializeMoreComponents();
            InitializeComponent();


            //float[] col = new float[] { color.GetHue(), color.GetSaturation(), color.GetBrightness() };
            Hsl hsl = new Hsl() { H = color.GetHue(), S = color.GetSaturation() * 100, L = color.GetBrightness() * 100 };
            Hsv hsv = hsl.To<Hsv>();
            _selectedColor = new HSVColor(hsv);

            InvalidatePanels();
        }

        private void InitializeMoreComponents()
        {
            satValPanel = new DoubleBufferedPanel();
            huePanel = new DoubleBufferedPanel();
            colorPanel = new DoubleBufferedPanel();

            satValPanel.Location = new Point(12, 12);
            satValPanel.Name = "satValPanel";
            satValPanel.Size = new Size(360, 360);
            satValPanel.Visible = true;
            satValPanel.TabIndex = 2;
            satValPanel.MouseMove += new MouseEventHandler(this.satValPanel_MouseMove);
            satValPanel.MouseDown += new MouseEventHandler(this.satValPanel_MouseMove);

            huePanel.Location = new Point(378, 12);
            huePanel.Name = "huePanel";
            huePanel.Size = new Size(50, 360);
            huePanel.Visible = true;
            huePanel.TabIndex = 3;
            huePanel.MouseMove += new MouseEventHandler(this.huePanel_MouseMove);
            huePanel.MouseDown += new MouseEventHandler(this.huePanel_MouseMove);

            colorPanel.Location = new Point(131, 404);
            colorPanel.Name = "colorPanel";
            colorPanel.Size = new Size(181, 33);
            colorPanel.Visible = true;
            colorPanel.TabIndex = 4;

            satValPanel.Paint += SatVal_Paint;
            huePanel.Paint += Hue_Paint;
            colorPanel.Paint += Color_Paint;

            Controls.Add(colorPanel);
            Controls.Add(huePanel);
            Controls.Add(satValPanel);
        }

        private void Color_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, _selectedColor.ToColor())), e.Graphics.VisibleClipBounds);
        }

        private void Hue_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            RectangleF size = g.VisibleClipBounds;
            hueBounds = size;

            int step = 50;

            for (int y = 0; y < size.Height; y += step)
            {
                LinearGradientBrush b = new LinearGradientBrush(new PointF(0, size.Top + y), new PointF(0, size.Top + y + step), new HSVColor(y / size.Height * 360, 1.0, 1.0).ToColor(), new HSVColor((y + step) / size.Height * 360, 1.0, 1.0).ToColor());

                g.FillRectangle(b, size.Left, size.Top + y, size.Left + size.Width / 2, size.Top + y + step);
            }

            g.FillPolygon(Brushes.Black, new PointF[] { new PointF(size.Left + size.Width / 2, (float)_selectedColor.H / 360 * size.Height), new PointF(size.Left + size.Width, (float)_selectedColor.H / 360 * size.Height - 20), new PointF(size.Left + size.Width, (float)_selectedColor.H / 360 * size.Height + 20) });
        }

        private void SatVal_Paint(object s, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            RectangleF size = g.VisibleClipBounds;
            satValBounds = size;

            LinearGradientBrush b = new LinearGradientBrush(new PointF(size.Left, 0), new PointF(size.Right, 0), new HSVColor(_selectedColor.H, 0, 1.0).ToColor(), new HSVColor(_selectedColor.H, 1.0, 1.0).ToColor());
            g.FillRectangle(b, size.Left, size.Top, size.Width, size.Height);

            b = new LinearGradientBrush(new PointF(0, size.Top), new PointF(0, size.Bottom), Color.FromArgb(0, Color.Black), Color.Black);
            g.FillRectangle(b, size.Left, size.Top, size.Width, size.Height);

            Crosshair(new PointF((float)(_selectedColor.S * size.Width + size.Left), (float)((1 - _selectedColor.V) * size.Height + size.Top)), g);
        }

        private void Crosshair (PointF center, Graphics g)
        {
            Brush b = Brushes.Black;
            g.FillRectangle(b, (float)(center.X + 5), (float)(center.Y - 2.5), 20, 5);
            g.FillRectangle(b, (float)(center.X - 25), (float)(center.Y - 2.5), 20, 5);
            g.FillRectangle(b, (float)(center.X - 2.5), (float)(center.Y + 5), 5, 20);
            g.FillRectangle(b, (float)(center.X - 2.5), (float)(center.Y - 25), 5, 20);
        }

        private void huePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _selectedColor.H = Clamp(e.Y / hueBounds.Height * 360, 0, 360);
                InvalidatePanels();
            }
        }

        private void satValPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _selectedColor.S = Clamp(e.X / satValBounds.Width, 0, 1);
                _selectedColor.V = Clamp(1 - e.Y / satValBounds.Height, 0, 1);
                InvalidatePanels();
            }
        }

        private double Clamp (double val, double min, double max)
        {
            return Math.Min(Math.Max(val, min), max);
        }

        private void InvalidatePanels()
        {
            huePanel.Invalidate();
            satValPanel.Invalidate();
            colorPanel.Invalidate();
        }
    }
}
