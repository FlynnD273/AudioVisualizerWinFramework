using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AudioVisualizerWinFramework
{
    class SongInfo : NotifyPropertyChangedBase
    {
        ProgressBar progress;
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { UpdateField(ref _filePath, Path.GetFullPath(value)); }
        }

        private string _songName;
        public string SongName
        {
            get { return _songName; }
            set { UpdateField(ref _songName, value); }
        }

        private float[] _samples;
        public float[] Samples
        {
            get { return _samples; }
            set { UpdateField(ref _samples, value); }
        }

        private bool TempFile = false;
        private string folder;

        public SongInfo()
        {
            SongName = "";
        }
        
        public SongInfo(string filePath, ProgressBar p = null, string f = "")
        {
            folder = f;
            FilePath = filePath;

            SongName = Path.GetFileNameWithoutExtension(FilePath);

            if (Path.GetExtension(FilePath) == ".mp3")
            {
                Mp3ToWav(FilePath, Path.Combine("Temp", folder, Path.GetFileNameWithoutExtension(FilePath) + ".wav"));
                FilePath = Path.Combine("Temp", folder, Path.GetFileNameWithoutExtension(FilePath) + ".wav");
                TempFile = true;
            }

            if (Path.GetFileNameWithoutExtension(FilePath) != Utils.StripString(Path.GetFileNameWithoutExtension(FilePath)))
            {
                File.Copy(FilePath, Path.Combine("Temp", folder, Utils.StripString(Path.GetFileNameWithoutExtension(FilePath) + ".wav")));
                FilePath = Path.Combine("Temp", folder, Utils.StripString(Path.GetFileNameWithoutExtension(FilePath) + ".wav"));
            }
            progress = p;

            ReadSong();
        }

        public void Cleanup ()
        {
            if (TempFile)
            {
                File.Delete(FilePath);
            }
        }

        private void ReadSong()
        {
            progress?.BeginInvoke(new Action(() => progress.Maximum = 100));
            using (WaveFileReader reader = new WaveFileReader(FilePath))
            {
                Samples = new float[reader.SampleCount];
                reader.Position = 0;

                for (int i = 0; i < Samples.Length; i++)
                {
                    var frame = reader.ReadNextSampleFrame();

                    float temp = 0;

                    if (frame != null && frame.Length > 0)
                    {
                        for (int j = 0; j < frame.Length; j++)
                        {
                            temp += frame[j];
                        }

                        temp /= frame.Length;
                    }

                    Samples[i] = temp;
                    if (i % 1000 == 0 || i == Samples.Length - 5)
                        progress?.Invoke(new Action(() => progress.Value = Math.Min(Math.Max((int)((float)i / Samples.Length * progress.Maximum), progress.Minimum), progress.Maximum)));
                }
            }
            progress?.BeginInvoke(new Action(() => progress.Value = 0));
        }

        public static void Mp3ToWav(string mp3File, string outputFile)
        {
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            using (Mp3FileReader mp3Reader = new Mp3FileReader(mp3File))
            using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(mp3Reader))
                WaveFileWriter.CreateWaveFile(outputFile, pcmStream);
        }
    }
}
