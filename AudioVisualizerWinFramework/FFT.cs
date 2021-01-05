using MathNet.Numerics.IntegralTransforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AudioVisualizerWinFramework
{
    class FFT
    {
        public static float[] SampleToFreq(float[] wav, int sampleCount)
        {
            float[] temp = CalcFFT(ApplyWindow(wav, sampleCount));
            float[] result = new float[temp.Length / 2];
            Array.Copy(temp, result, result.Length);

            return result;
        }

        private static float[] ApplyWindow(float[] wav, int sampleCount)
        {
            int windowSize = sampleCount;

            float[] windowedVals = new float[windowSize];
            float[] data = new float[wav.Length];
            Array.Copy(wav, data, wav.Length);

            double[] k = MathNet.Numerics.Window.Blackman(windowSize).ToArray();

            for (int i = 0; i < windowSize; i++)
            {
                if (i < windowedVals.Length && i < data.Length)
                {
                    windowedVals[i] = data[data.Length - i - 1] * (float)k[i];
                }
                else
                {
                    windowedVals[i] = 0;
                }
            }

            return windowedVals;
        }

        private static float[] CalcFFT(float[] samples)
        {
            Complex[] comps = new Complex[samples.Length];

            for (int i = 0; i < comps.Length; i++)
            {
                comps[i] = new Complex(samples[i], 0);
                /*if (comps[i] == null)
                {
                    comps[i] = new Complex(0, 0);
                }*/
            }

            Fourier.Forward(comps);

            //Too slow! I need to learn how fast Fourier transforms work
            //comps = FourierTransform(comps);

            float[] values = comps.Select(o => Convert.ToSingle(o.Magnitude)).ToArray();

            return values;
        }

        public static Complex[] FourierTransform(Complex[] x)
        {
            int N = x.Length;

            Complex[] output = new Complex[N];

            double scale = -2.0 * Math.PI / N;
            for (int k = 0; k < N; k++)
            {
                output[k] = new Complex();
                for (int n = 0; n < N; n++)
                    output[k] += x[n] * Complex.FromPolarCoordinates(1, scale * k * n);
            }

            return output;
        }
    }
}
