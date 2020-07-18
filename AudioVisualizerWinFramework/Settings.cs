using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

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

        private BindingList<NamedColor> _colors;

        public BindingList<NamedColor> Colors
        {
            get => _colors;
            set => UpdateField(ref _colors, value);
        }

        public Settings (float x, float y, int samplePow, int smoothing)
        {
            XScale = x;
            YScale = y;
            SamplePow = samplePow;
            Smoothing = smoothing;
            Colors = new BindingList<NamedColor>();
        }

        public NamedColor GetNamedColor(string name)
        {
            return Colors.First(c => c.Name == name);
        }
        
        public Color GetColor(string name)
        {
            return GetNamedColor(name).Color;
        }
    }
}
