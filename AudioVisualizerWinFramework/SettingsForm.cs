using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using static AudioVisualizerWinFramework.NamedInputState;
using FFMpegCore;
using FFMpegCore.Pipes;
using FFMpegCore.Extend;
using System.ComponentModel;
using System.Linq;
using FFMpegCore.Enums;
using AudioVisualizerWinFramework.Properties;
using Accord.Video.FFMPEG;
//using Accord.Video.VFW;

namespace AudioVisualizerWinFramework
{
    partial class SettingsForm : Form
    {
        public event EventHandler<RenderEventArgs> SettingsChanged;
        public event EventHandler UpdateGraphics;

        private BufferedWaveProvider waveProvider;
        private ISampleProvider provider;
        private IWaveIn input;

        private List<Settings> settingsOptions;
        private List<RenderBase> renderOptions;

        public List<float> Samples { get; private set; }
        private RenderBase Render;
        private Settings Settings;
        public bool ProgramShuttingDown { get; set; }

        private WaveFileReader reader;
        private WaveOut output;

        private System.Timers.Timer progressTimer;

        private SongInfo songInfo;
        public SettingsForm()
        {
            InitializeComponent();

            Samples = new List<float>();

            InitOptions();
            InitUI();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!ProgramShuttingDown)
            {
                e.Cancel = true;
                Visible = false;
            }

            base.OnFormClosing(e);
        }

        private void InitUI()
        {
            xScaleNumberBox.KeyDown += NumericUpDown_KeyDown;
            yScaleNumberBox.KeyDown += NumericUpDown_KeyDown;
            samplePowNumberBox.KeyDown += NumericUpDown_KeyDown;
            smoothingNumberBox.KeyDown += NumericUpDown_KeyDown;

            renderModeComboBox.SelectedIndexChanged += RaiseSettingsChanged;

            renderModeComboBox.DataSource = renderOptions;
            renderModeComboBox.DisplayMember = "Name";
            renderModeComboBox.SelectedIndex = 0;

            inputModeComboBox.DataSource = NamedInputState.FromInputStateEnum();
            inputModeComboBox.DisplayMember = "Name";
            inputModeComboBox.ValueMember = "State";
            inputModeComboBox.SelectedIndex = 0;

            xScaleNumberBox.Minimum = 1M;
            yScaleNumberBox.Increment = 10M;
            yScaleNumberBox.Maximum = 500M;
            samplePowNumberBox.Maximum = 16;
            smoothingNumberBox.Minimum = 1;
            smoothingNumberBox.Maximum = 10000;

            playButton.Image = Resources.Play;
            audioPlaybackPanel.Enabled = false;
            filePanel.Enabled = false;

            UpdateSettings();
        }

        private void RaiseSettingsChanged(object sender, EventArgs e)
        {
            SettingsChanged?.Invoke(sender, new RenderEventArgs(Render));
        }

        private void InitOptions()
        {
            settingsOptions = new List<Settings>
            {
                new Settings(1.0f, 200f, 13, 1), //Waveform
                new Settings(5.0f, 300f, 13, 1), //Frequency
                new Settings(5.0f, 200f, 13, 1), //Reflections
                new Settings(5.0f, 300f, 13, 20), //Frequency Wave
                new Settings(8.0f, 200f, 13, 10), //Circle Outline
                new Settings(8.0f, 200f, 13, 10), //Shadow
                new Settings(8.0f, 200f, 13, 10), //Color Wheel
                new Settings(4.0f, 100f, 13, 10) //Mirrored Circle
            };

            renderOptions = new List<RenderBase>();
            renderOptions.Add(new RenderWaveform(settingsOptions[renderOptions.Count], "Waveform"));
            renderOptions.Add(new RenderBasicFreq(settingsOptions[renderOptions.Count], "Frequency"));
            renderOptions.Add(new RenderReflectedFreq(settingsOptions[renderOptions.Count], "Reflections"));
            renderOptions.Add(new RenderWaveFreq(settingsOptions[renderOptions.Count], "Frequency Wave"));
            renderOptions.Add(new RenderOutlineCircle(settingsOptions[renderOptions.Count], "Circle Outline"));
            renderOptions.Add(new RenderShadowCircle(settingsOptions[renderOptions.Count], "Shadow"));
            renderOptions.Add(new RenderRainbowCircle(settingsOptions[renderOptions.Count], "Color Wheel"));
            renderOptions.Add(new RenderReflectedCircle(settingsOptions[renderOptions.Count], "Mirrored Circle"));

            Settings = settingsOptions[0];
            Render = renderOptions[0];
        }

        private void UpdateSettings()
        {
            Settings = settingsOptions[renderModeComboBox.SelectedIndex];
            Render = renderOptions[renderModeComboBox.SelectedIndex];
            xScaleNumberBox.DataBindings.Clear();
            xScaleNumberBox.DataBindings.Add("Value", Settings, "XScale", false, DataSourceUpdateMode.OnPropertyChanged);
            yScaleNumberBox.DataBindings.Clear();
            yScaleNumberBox.DataBindings.Add("Value", Settings, "YScale", false, DataSourceUpdateMode.OnPropertyChanged);
            samplePowNumberBox.DataBindings.Clear();
            samplePowNumberBox.DataBindings.Add("Value", Settings, "SamplePow", false, DataSourceUpdateMode.OnPropertyChanged);
            smoothingNumberBox.DataBindings.Clear();
            smoothingNumberBox.DataBindings.Add("Value", Settings, "Smoothing", false, DataSourceUpdateMode.OnPropertyChanged);
            colorNamesListBox.DataSource = Settings.Colors;
            colorsListBox.DataSource = Settings.Colors;
        }

        private void InitAudio(InputState state)
        {
            filePanel.Enabled = false;

            switch (state)
            {
                case InputState.SpeakerOut:
                    input = new WasapiLoopbackCapture();

                    InitReader();
                    break;
                case InputState.MicrophoneIn:
                    input = new WaveIn
                    {
                        WaveFormat = new WaveFormat(44100, 32, 2)
                    };

                    InitReader();
                    break;
                case InputState.FileIn:
                    input = new WasapiLoopbackCapture();

                    waveProvider = new BufferedWaveProvider(input.WaveFormat);

                    provider = waveProvider.ToSampleProvider();
                    input.DataAvailable += AddDataFromFile;
                    input.RecordingStopped += (s, a) => { input?.Dispose(); };
                    break;
                default:
                    break;
            }
        }

        private void AddDataFromFile(object s, WaveInEventArgs a)
        {
            Samples.Clear();
            int index = (int)((reader.CurrentTime.TotalMilliseconds - output.DesiredLatency / 2) / reader.TotalTime.TotalMilliseconds * reader.SampleCount);
            for (int i = Math.Max(index - Settings.SampleCount / 2, 0); i < Math.Min(reader.SampleCount, index + Settings.SampleCount / 2); i++)
            {
                Samples.Add(songInfo.Samples[i]);
            }

            UpdateGraphics?.Invoke(this, EventArgs.Empty);
        }

        private void InitReader ()
        {
            waveProvider = new BufferedWaveProvider(input.WaveFormat);

            provider = waveProvider.ToSampleProvider();
            input.DataAvailable += AddData;
            input.RecordingStopped += (s, a) => { input?.Dispose(); };
        }

        private void AddData(object s, WaveInEventArgs a)
        {
            waveProvider.ClearBuffer();
            waveProvider.AddSamples(a.Buffer, 0, a.BytesRecorded);

            float[] temp = new float[a.BytesRecorded / 4];

            provider.Read(temp, 0, temp.Length);

            for (int i = 0; i < temp.Length / 2; i++)
            {
                Samples.Add(temp[i * 2] + temp[i * 2 + 1]);
            }

            while (Samples.Count > Settings.SampleCount)
            {
                Samples.RemoveAt(0);
            }

            UpdateGraphics?.Invoke(this, EventArgs.Empty);
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            StopReading();
        }

        private void StopReading()
        {
            input?.StopRecording();

            Start.Enabled = true;
            Stop.Enabled = false;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartReading();
        }

        private void StartReading()
        {
            RaiseSettingsChanged(this, EventArgs.Empty);
            InitAudio(((NamedInputState)inputModeComboBox.SelectedItem).State);
            input?.StartRecording();

            Start.Enabled = false;
            Stop.Enabled = true;

            if (((NamedInputState)inputModeComboBox.SelectedItem).State == InputState.FileIn)
            {
                filePanel.Enabled = true;
            }
        }

        private void Wave_Paint(object sender, PaintEventArgs e)
        {
            if (input != null && Samples != null && Samples.Count > 100)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
                e.Graphics.Clear(Color.Black);
                Render.Render(e.Graphics, Samples.ToArray());
            }
        }

        private void InputModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (input != null)
            {
                StopReading();
            }

            if (((NamedInputState)inputModeComboBox.SelectedItem).State == InputState.FileIn)
            {
                filePanel.Enabled = true;
            }
            else
            {
                filePanel.Enabled = false;
                output?.Pause();
                playButton.Image = Resources.Play;
            }
        }

        private void RenderModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        private void NumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void ColorNamesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ColorPicker col = new ColorPicker(((NamedColor)colorNamesListBox.SelectedItem).Color);

            if (col.ShowDialog() == DialogResult.OK)
            {
                Settings.Colors[colorNamesListBox.SelectedIndex].Color = col.Color;
                colorsListBox.Invalidate();
            }
        }

        private void ColorsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            //
            // Draw the background of the ListBox control for each item.
            // Create a new Brush and initialize to a Black colored brush
            // by default.
            //
            e.DrawBackground();
            if (e.Index > -1)
            {
                e.Graphics.FillRectangle(new SolidBrush(((NamedColor)((ListBox)sender).Items[e.Index]).Color), e.Bounds);
            }
            //
            // If the ListBox has focus, draw a focus rectangle 
            // around the selected item.
            //
            e.DrawFocusRectangle();
        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Sound Files (*.mp3; *.wav)|*.mp3; *.wav|All Files (*.*)|*.*",
                FilterIndex = 0,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StopReading();
                output?.Dispose();
                reader?.Dispose();
                input?.Dispose();
                output = null;
                reader = null;
                input = null;

                songInfo = new SongInfo(dialog.FileName);

                songProgressBar.Value = 0;
            }

            if (File.Exists(songInfo.FilePath))
            {
                fileNameLabel.Text = songInfo.SongName;

                audioPlaybackPanel.Enabled = true;

                reader = new WaveFileReader(songInfo.FilePath);

                output = new WaveOut
                {
                    NumberOfBuffers = 8
                };
                output.PlaybackStopped += Output_PlaybackStopped;
                output.Init(reader);

                playButton.Image = Resources.Play;
            }
        }

        private void Output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            playButton.Image = Resources.Play;

            songProgressBar.Value = 0;
            reader.Position = 0;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (output.PlaybackState == PlaybackState.Paused || output.PlaybackState == PlaybackState.Stopped)
            {
                output.Play();

                if (Start.Enabled)
                    StartReading();
                playButton.Image = Resources.Pause;

                StartProgressBar();
            }
            else
            {
                output.Pause();
                playButton.Image = Resources.Play;
            }
        }

        private void StartProgressBar()
        {
            progressTimer = new System.Timers.Timer();

            songProgressBar.Maximum = 100;
            songProgressBar.Value = (int)reader.CurrentTime.TotalSeconds;
            progressTimer.Interval = 100;
            progressTimer.Elapsed += UpdateProgressBar;
            progressTimer.Start();
        }
        private void UpdateProgressBar(object sender, EventArgs e)
        {
            songProgressBar?.BeginInvoke(new Action(() => songProgressBar.Value = Math.Min(Math.Max((int)((float)reader.CurrentTime.TotalSeconds / reader.TotalTime.TotalSeconds * 100), songProgressBar.Minimum), songProgressBar.Maximum)));
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog
            {
                Filter = "AVI (*.avi)|*.avi",
                DefaultExt = ".avi",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                CreateVideo(fd.FileName);
            }
        }

        private void CreateVideo(string outputPath)
        {
            //USING AFORGE NOW
            //RawVideoPipeSource pipe = new RawVideoPipeSource(GenerateFrames());
            //pipe.FrameRate = 30;

            //FFMpegArguments.FromPipe(pipe).WithVideoCodec("h.624").ForceFormat("mp4").OutputToFile(outputPath).ProcessSynchronously();

            //VideoFileWriter writer = new VideoFileWriter();
            //writer.Open(outputPath, 1920, 1080, 30, VideoCodec.MpegTs);

            //AVIWriter writer = new AVIWriter("WMV3")
            //{
            //    FrameRate = 30
            //};
            //writer.Open(outputPath, 1920, 1080);

            if (File.Exists("Video.avi"))
            {
                File.Delete("Video.avi");
            }
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            VideoFileWriter writer = new VideoFileWriter();
            writer.Open("Video.avi", 1920, 1080, 30, Accord.Video.FFMPEG.VideoCodec.H264, 4000000);
            GenerateFrames(ref writer);
            writer.Close();

            FFMpeg.ReplaceAudio("Video.avi", songInfo.FilePath, outputPath);
        }

        private int MapRange (int val, int oldMin, int oldMax, int newMin, int newMax)
        {
            return (int)(((double)(val - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin);
        }

        private void GenerateFrames(ref VideoFileWriter writer)
        {
            Invoke(new Action(() => Enabled = false));
            int lastFrame = (int)(reader.TotalTime.TotalSeconds * 30);
            int lastIndex = (int)reader.SampleCount;
            int index = 0;
            List<IVideoFrame> frames = new List<IVideoFrame>();
            songProgressBar.Maximum = lastFrame;
            for (int i = 0; i < lastFrame; i++)
            {
                Samples.Clear();
                index = MapRange(i, 0, lastFrame, 0, lastIndex);
                for (int j = Math.Max(index - Settings.SampleCount / 2, 0); j < Math.Min(reader.SampleCount, index + Settings.SampleCount / 2); j++)
                {
                    Samples.Add(songInfo.Samples[j]);
                }

                using (Bitmap bmp = new Bitmap(1920, 1080))
                {
                    Graphics g = Graphics.FromImage(bmp);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    Render.Render(g, Samples.ToArray());

                    writer.WriteVideoFrame(bmp);
                    //bmp.Save(@"C:\Users\Flynn\Downloads\New folder\" + i + ".bmp");
                }

                songProgressBar.Invoke(new Action(() => songProgressBar.Value = i));
            }

            songProgressBar.Invoke(new Action(() => songProgressBar.Value = 0));
            Invoke(new Action(() => Enabled = true));
        }
    }
}
