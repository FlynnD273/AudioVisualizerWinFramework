using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

//The settings for the individual visualizers
//This stores all of the customizable values for a visualizer style

namespace AudioVisualizerWinFramework
{
    class Settings : NotifyPropertyChangedBase
    {
        private float _xScale;
        public float XScale
        {
            get => _xScale;
            set => UpdateField(ref _xScale, value);
        }

        private float _yScale;

        public float YScale
        {
            get => _yScale;
            set => UpdateField(ref _yScale, value);
        }

        private int _smoothing;

        public int Smoothing
        {
            get => _smoothing;
            set => UpdateField(ref _smoothing, value);
        }

        private int _samplePow;

        public int SamplePow
        {
            get => _samplePow;
            set => UpdateField(ref _samplePow, value, OnSamplePowChanged);
        }

        private void OnSamplePowChanged(int obj)
        {
            SampleCount = (int)Math.Pow(2, _samplePow);
            OnPropertyChanged(nameof(SampleCount));
        }

        public int SampleCount { get; private set; }

        private Size _windowSize;
        public Size WindowSize
        {
            get { return _windowSize; }
            set { UpdateField(ref _windowSize, value); }
        }

        public int WindowWidth { get => WindowSize.Width; set => WindowSize = new Size(value, WindowSize.Height); }
        public int WindowHeight { get => WindowSize.Height; set => WindowSize = new Size(WindowSize.Width, value); }

        private BindingList<NamedColor> _colors;

        public BindingList<NamedColor> Colors
        {
            get => _colors;
            set => UpdateField(ref _colors, value);
        }

        public Settings(float x, float y, int samplePow, int smoothing, Size windowSize) : this(x, y, samplePow, smoothing, windowSize, new BindingList<NamedColor>()) { }

        public Settings(float x, float y, int samplePow, int smoothing, Size windowSize, BindingList<NamedColor> colors)
        {
            XScale = x;
            YScale = y;
            SamplePow = samplePow;
            Smoothing = smoothing;
            Colors = colors;
            WindowSize = windowSize;
        }

        public NamedColor GetNamedColor(string name)
        {
            if (ContainsName(name))
                return Colors.First(c => c.Name == name);
            return null;
        }

        private bool ContainsName(string name)
        {
            foreach(NamedColor c in Colors)
            {
                if (c.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        
        public Color GetColor(string name)
        {
            return GetNamedColor(name).Color;
        }

        public Settings Copy()
        {
            return new Settings(XScale, YScale, SamplePow, Smoothing, WindowSize, Colors);
        }
    }
}
