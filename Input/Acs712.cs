using Microsoft.SPOT.Hardware;

namespace PowerReader.Input
{
    /// <summary>
    /// Reader for ACS712 Hall Effect-Based Linear Current Sensor IC with 2.1 kVRMS Isolation and a Low-Resistance Current Conductor
    /// </summary>
    public class Acs712 : Analog
    {
        public enum Range
        {
            FiveAmps, TwentyAmps, ThirtyAmps
        }

        public Acs712(Cpu.Pin channel, Range range) : base(channel)
        {
            double sensitivity = 0;
            switch (range)
            {
                case Range.FiveAmps:
                    sensitivity = 0.185;
                    break;
                case Range.TwentyAmps:
                    sensitivity = 0.1;
                    break;
                case Range.ThirtyAmps:
                    sensitivity = 0.66;
                    break;

            }

            Scale /= sensitivity;
            Offset /= sensitivity;
        }
    }
}