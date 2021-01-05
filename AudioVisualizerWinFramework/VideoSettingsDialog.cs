using FFMpegCore.Arguments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioVisualizerWinFramework
{
    public partial class VideoSettingsDialog : Form
    {
        public Size VideoSize { get; private set; }
        public int FPS { get; private set; }
        public int BitDepth { get; private set; }
        public VideoSettingsDialog()
        {
            InitializeComponent();
            VideoSize = new Size((int)videoWidthNumberBox.Value, (int)videoHeightNumberBox.Value);
            FPS = (int)fpsNumberBox.Value;
            BitDepth = (int)bitDepthNumberBox.Value;
        }

        private void VideoWidthNumberBox_ValueChanged(object sender, EventArgs e)
        {
            VideoSize = new Size((int)videoWidthNumberBox.Value, VideoSize.Height);
        }

        private void videoHeightNumberBox_ValueChanged(object sender, EventArgs e)
        {
            VideoSize = new Size(VideoSize.Width, (int)videoHeightNumberBox.Value);
        }

        private void fpsNumberBox_ValueChanged(object sender, EventArgs e)
        {
            FPS = (int)fpsNumberBox.Value;
        }

        private void bitDepthNumberBox_ValueChanged(object sender, EventArgs e)
        {
            BitDepth = (int)bitDepthNumberBox.Value;
        }
    }
}
