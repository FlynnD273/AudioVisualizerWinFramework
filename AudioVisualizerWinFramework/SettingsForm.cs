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
using AudioVisualizerWinFramework.Properties;
using Accord.Video.FFMPEG;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

//The form for editing the visualizer settings

namespace AudioVisualizerWinFramework
{
    partial class SettingsForm : Form
    {
        public event EventHandler<RenderEventArgs> SettingsChanged;
        public event EventHandler<WindowSizeChangedEventArgs> WindowChanged;
        public event EventHandler UpdateGraphics;
        public bool ShouldPaint { get; private set; }

        private BufferedWaveProvider waveProvider;
        private ISampleProvider provider;
        private IWaveIn input;

        private List<Settings> settingsOptions;
        private List<RenderBase> renderOptions;

        public List<float> Samples { get; private set; }

        private RenderBase render;
        private Settings settings;
        private Settings videoSettings;

        public bool ProgramShuttingDown { get; set; }

        private WaveFileReader reader;
        private WaveOut output;

        private System.Timers.Timer progressTimer;

        private SongInfo songInfo;

        public string Folder;

        public SettingsForm(Size size)
        {
            ShouldPaint = false;
            InitializeComponent();

            Samples = new List<float>();

            InitOptions(size);
            InitUI(size);
            Folder = DateTime.Now.GetHashCode().ToString();
            Directory.CreateDirectory("Temp");
            Directory.CreateDirectory(Path.Combine("Temp", Folder));
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!ProgramShuttingDown)
            {
                e.Cancel = true;
                Visible = false;
            }
            else
            {
                reader?.Dispose();
                foreach (string f in Directory.GetFiles(Path.Combine("Temp", Folder)))
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch(IOException err)
                    {

                    }
                }
                try
                {
                    Directory.Delete(Path.Combine("Temp", Folder));
                }
                catch (IOException err)
                {

                }
            }

            base.OnFormClosing(e);
        }

        private void InitUI(Size size)
        {
            xScaleNumberBox.KeyDown += NumericUpDown_KeyDown;
            yScaleNumberBox.KeyDown += NumericUpDown_KeyDown;
            samplePowNumberBox.KeyDown += NumericUpDown_KeyDown;
            smoothingNumberBox.KeyDown += NumericUpDown_KeyDown;
            windowHeightNumberBox.KeyDown += NumericUpDown_KeyDown;
            windowWidthNumberBox.KeyDown += NumericUpDown_KeyDown;


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
            windowWidthNumberBox.Maximum = size.Width;
            windowHeightNumberBox.Maximum = size.Height;

            playButton.Image = Resources.Play;
            audioPlaybackPanel.Enabled = false;
            filePanel.Enabled = false;

            UpdateSettings();
        }

        private void RaiseSettingsChanged(object sender, EventArgs e)
        {
            SettingsChanged?.Invoke(sender, new RenderEventArgs(render));
        }

        private void InitOptions(Size s)
        {
            settingsOptions = new List<Settings>
            {
                new Settings(1.0f, 200f, 13, 1, s), //Waveform
                new Settings(5.0f, 300f, 13, 1, s), //Frequency
                new Settings(5.0f, 200f, 13, 1, s), //Reflections
                new Settings(5.0f, 300f, 13, 20, s), //Frequency Wave
                new Settings(8.0f, 200f, 13, 10, s), //Circle Outline
                new Settings(8.0f, 200f, 13, 10, s), //Shadow
                new Settings(8.0f, 200f, 13, 10, s), //Color Wheel
                new Settings(4.0f, 100f, 13, 10, s) //Mirrored Circle
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

            settings = settingsOptions[0];
            render = renderOptions[0];
        }

        private void UpdateSettings()
        {
            settings = settingsOptions[renderModeComboBox.SelectedIndex];
            render = renderOptions[renderModeComboBox.SelectedIndex];
            xScaleNumberBox.DataBindings.Clear();
            xScaleNumberBox.DataBindings.Add("Value", settings, "XScale", false, DataSourceUpdateMode.OnPropertyChanged);
            yScaleNumberBox.DataBindings.Clear();
            yScaleNumberBox.DataBindings.Add("Value", settings, "YScale", false, DataSourceUpdateMode.OnPropertyChanged);
            samplePowNumberBox.DataBindings.Clear();
            samplePowNumberBox.DataBindings.Add("Value", settings, "SamplePow", false, DataSourceUpdateMode.OnPropertyChanged);
            smoothingNumberBox.DataBindings.Clear();
            smoothingNumberBox.DataBindings.Add("Value", settings, "Smoothing", false, DataSourceUpdateMode.OnPropertyChanged);
            colorNamesListBox.DataSource = settings.Colors;
            colorsListBox.DataSource = settings.Colors;
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
            for (int i = Math.Max(index - settings.SampleCount / 2, 0); i < Math.Min(reader.SampleCount, index + settings.SampleCount / 2); i++)
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

            while (Samples.Count > settings.SampleCount)
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
            ShouldPaint = true;


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
                render.Render(e.Graphics, Samples.ToArray());
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
                Start.Enabled = false;
            }
            else
            {
                Start.Enabled = true;
                Stop.Enabled = false;

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
            ColorPickerDialog col = new ColorPickerDialog(((NamedColor)colorNamesListBox.SelectedItem).Color);

            if (col.ShowDialog() == DialogResult.OK)
            {
                settings.Colors[colorNamesListBox.SelectedIndex].Color = col.Color;
                colorsListBox.Invalidate();
            }
        }

        private void ColorsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1)
            {
                e.Graphics.FillRectangle(new SolidBrush(((NamedColor)((ListBox)sender).Items[e.Index]).Color), e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            playButton.Image = Resources.Play;
            ShouldPaint = false;

            StopReading();
            output?.Stop();

            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Sound Files (*.mp3; *.wav)|*.mp3; *.wav|All Files (*.*)|*.*",
                FilterIndex = 0,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
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

                songInfo = new SongInfo(dialog.FileName, songProgressBar, Folder);

                songProgressBar.Value = 0;

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
                output?.Play();

                if (Start.Enabled)
                    StartReading();
                playButton.Image = Resources.Pause;

                StartProgressBar();
                ShouldPaint = true;
            }
            else
            {
                output.Pause();
                playButton.Image = Resources.Play;
                ShouldPaint = false;
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
            if (reader != null)
                songProgressBar?.BeginInvoke(new Action(() => songProgressBar.Value = Math.Min(Math.Max((int)((float)(reader?.CurrentTime.TotalSeconds ?? 0) / (reader?.TotalTime.TotalSeconds ?? 1) * 100), songProgressBar.Minimum), songProgressBar.Maximum)));
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (output.PlaybackState == PlaybackState.Playing)
                    PlayButton_Click(this, EventArgs.Empty);
                bool isVideoFolder = Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Music Videos"));
                SaveFileDialog fd = new SaveFileDialog
                {
                    Filter = "AVI (*.avi)|*.avi",
                    DefaultExt = ".avi",
                    InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), isVideoFolder ? "Music Videos" : ""),
                    FileName = songInfo.SongName
                };

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    if (!CreateVideo(fd.FileName))
                    {
                        Enabled = true;
                        MessageBox.Show("Video exported successfully", "Success!");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error exporting video", $"Error: {err.Message}");
            }
        }

        private bool CreateVideo(string outputPath)
        {
            output.Dispose();
            reader.Position = 0;

            string videoPath = Path.GetFullPath(Path.Combine("Temp", Folder, songInfo.SongName + "_Video.avi"));
            try
            {
                if (File.Exists(videoPath))
                {
                    File.Delete(videoPath);
                }
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show($"An error occurred\n\n{e.Message}", "ERROR");
                return false;
            }
            VideoFileWriter writer = null;
            VideoSettingsDialog v = new VideoSettingsDialog();
            if (v.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    writer = new VideoFileWriter();
                    writer.Open(videoPath, v.VideoSize.Width, v.VideoSize.Height, v.FPS, Accord.Video.FFMPEG.VideoCodec.H264, v.BitDepth);
                    GenerateFrames(ref writer, v.FPS, v.VideoSize.Width, v.VideoSize.Height);
                    writer.Close();

                    try
                    {
                        Process.Start("ffmpeg.exe", $"-i \"{videoPath}\" -i \"{songInfo.FilePath}\" -c:v copy \"{outputPath}\"").WaitForExit();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show($"An error occurred copying the video\n\n{err.Message}\n\nConsole: ffmpeg.exe -i \"{videoPath}\" -i \"{songInfo.FilePath}\" -c:v copy \"{outputPath}\"", "ERROR");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"An error occurred generating the video\n\n{e.Message}", "ERROR");
                    writer?.Close();
                    return false;
                }
            }

            return true;
        }

        private int Map (int val, int oldMin, int oldMax, int newMin, int newMax)
        {
            return (int)(((double)(val - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin);
        }

        private void GenerateFrames(ref VideoFileWriter writer, int fps, int width, int height)
        {
            lock (songInfo.Samples)
            {
                videoSettings = settings.Copy();
                videoSettings.WindowSize = new Size(width, height);
                render.Settings = videoSettings;
                Invoke(new Action(() => Enabled = false));
                int lastFrame = (int)(reader.TotalTime.TotalSeconds * fps);
                int lastIndex = (int)reader.SampleCount;
                int index = 0;
                List<IVideoFrame> frames = new List<IVideoFrame>();
                songProgressBar.Maximum = lastFrame;
                for (int i = 0; i < lastFrame; i++)
                {
                    Samples.Clear();
                    index = Map(i, 0, lastFrame, 0, lastIndex);
                    for (int j = Math.Max(index - songInfo.Samples.Length / 2, 0); j < Math.Min(songInfo.Samples.Length, index + songInfo.Samples.Length / 2); j++)
                    {
                        Samples.Add(songInfo.Samples[j]);
                    }

                    using (Bitmap bmp = new Bitmap(width, height))
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            render.Render(g, Samples.ToArray());
                            writer.WriteVideoFrame(bmp);
                        }

                    if (i % 100 == 0)
                        songProgressBar.Invoke(new Action(() => songProgressBar.Value = i));
                }

                songProgressBar.Invoke(new Action(() => songProgressBar.Value = 0));
                Invoke(new Action(() => Enabled = true));
                render.Settings = settings;
            }
        }

        private void windowSizeNumberBox_ValueChanged(object sender, EventArgs e)
        {
            settings.WindowSize = new Size((int)windowWidthNumberBox.Value, (int)windowHeightNumberBox.Value);
            WindowChanged?.Invoke(sender, new WindowSizeChangedEventArgs(settings.WindowSize));
        }

        public void WindowSizeChanged(Size s, FormWindowState state)
        {
            windowWidthNumberBox.Value = s.Width;
            windowHeightNumberBox.Value = s.Height;
            if (state == FormWindowState.Maximized)
            {
                windowWidthNumberBox.Enabled = false;
                windowHeightNumberBox.Enabled = false;
            }
            else
            {
                windowWidthNumberBox.Enabled = true;
                windowHeightNumberBox.Enabled = true;
            }
        }
    }
}
