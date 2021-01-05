using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            await Task.Delay(100);
            WindowState = FormWindowState.Maximized;
            settingsForm = new SettingsForm(ClientSize);
            settingsForm.UpdateGraphics += (s, e) => Invalidate();
            settingsForm.SettingsChanged += UpdateSettings;
            settingsForm.WindowChanged += updatWindowSize;
            settingsForm.Show();
            await Task.Delay(100);
            settingsForm.Activate();
        }

        private void updatWindowSize(object sender, WindowSizeChangedEventArgs e)
        {
            ClientSize = e.Size;
        }

        private void UpdateSettings(object sender, RenderEventArgs e)
        {
            activeRender = e.Render;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            try
            {
                if (settingsForm != null && settingsForm.ShouldPaint && settingsForm.Samples != null && settingsForm.Samples.Count > 0)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
                    e.Graphics.Clear(Color.Black);
                    activeRender.Render(e.Graphics, settingsForm.Samples.ToArray());
                }
            }
            catch (Exception exception)
            {

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
                settingsForm?.WindowSizeChanged(ClientSize, WindowState);
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
