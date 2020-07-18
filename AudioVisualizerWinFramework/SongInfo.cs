using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AudioVisualizerWinFramework
{
    class SongInfo : NotifyPropertyChangedBase
    {
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

        public SongInfo()
        {
            SongName = "";
        }

        public SongInfo(string filePath)
        {
            FilePath = filePath;

            SongName = Path.GetFileName(FilePath);

            if (Path.GetExtension(FilePath) == ".mp3")
            {
                Mp3ToWav(FilePath, "Temp.wav");
                FilePath = "Temp.wav";
            }

            ReadSong();
        }

        private void ReadSong()
        {
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
                }
            }
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
