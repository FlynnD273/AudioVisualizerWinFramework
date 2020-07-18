using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using NAudio;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioVisualizerWinFramework
{
    partial class RenderForm : Form
    {
        private RenderBase activeRender;

        private SettingsForm settingsForm;


        private bool isResizing = false;

        public RenderForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            InitSettingsForm();
        }

        private async void InitSettingsForm()
        {
            settingsForm = new SettingsForm();
            settingsForm.UpdateGraphics += (s, e) => Invalidate();
            settingsForm.SettingsChanged += UpdateSettings;
            settingsForm.Show();
            await Task.Delay(100);
            settingsForm.Activate();
        }

        private void UpdateSettings(object sender, RenderEventArgs e)
        {
            activeRender = e.Render;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (settingsForm.Samples != null && settingsForm.Samples.Count > 0)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
                e.Graphics.Clear(Color.Black);
                activeRender.Render(e.Graphics, settingsForm.Samples.ToArray());
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            settingsForm.ProgramShuttingDown = true;
            settingsForm.Close();

            base.OnFormClosed(e);
        }

        private void RenderForm_ClientSizeChanged(object sender, EventArgs e)
        {
            if (!isResizing)
            {
                isResizing = true;
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    FormBorderStyle = FormBorderStyle.Sizable;
                }
                isResizing = false;
            }
        }
        private void RenderForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            settingsForm.Show();
            settingsForm.Activate();
        }
    }
}
