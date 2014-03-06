// PortamentoSineWaveOscillator by Charles Petzold, November 2009
using System;
using NAudio.Wave;

namespace LeapVoxSample
{
    class PortamentoSineWaveOscillator : WaveProvider16
    {
        double phaseAngle;
        double previousPitch;

        public PortamentoSineWaveOscillator(int sampleRate):
            base(sampleRate, 1)
        {
        }

        public PortamentoSineWaveOscillator(int sampleRate, double pitch)
            : this(sampleRate)
        {
            Pitch = pitch;
            previousPitch = pitch;
        }

        public double Pitch { set; get; }
        public short Amplitude { set; get; }

        public override int Read(short[] buffer, int offset, int sampleCount)
        {
            for (int index = 0; index < sampleCount; index++)
            {
                double pitch;

                if (Pitch != previousPitch)
                {
                    pitch = ((sampleCount - index - 1) * previousPitch + Pitch)  / (sampleCount - index);
                    previousPitch = pitch;
                }
                else
                    pitch = Pitch;

                double frequency = PitchToFrequency(pitch);
                buffer[offset + index] = (short)(Amplitude * Math.Sin(phaseAngle));
                phaseAngle += 2 * Math.PI * frequency / WaveFormat.SampleRate;

                if (phaseAngle > 2 * Math.PI)
                    phaseAngle -= 2 * Math.PI;
            }
            return sampleCount;
        }

        double PitchToFrequency(double pitch)
        {
            return 440 * Math.Pow(2, (pitch - 69) / 12);
        }
    }
}
