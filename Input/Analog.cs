using SecretLabs.NETMF.Hardware;

namespace PowerReader.Input
{
    /// <summary>
    /// Netduino 2 Plus's A/D is 12 bit, but the real precision is lower, let use 10 bit.
    /// With 3.3V max, one segment is 3.3V / 1023 = 3.2mV long, setting the offset to the middle of it's segment
    /// </summary>
    public class Analog : AnalogInput
    {
        private const double SegmentLength = 3.3 / 1023;
        protected double Scale = SegmentLength;
        protected double Offset = SegmentLength / 2;

        public Analog(Microsoft.SPOT.Hardware.Cpu.Pin channel) : base(channel)
        {
        }

        public double ReadConverted()
        {
            int value = Read() / 4; // trim last 2 bits
            return value * Scale + Offset;
        }
    }
}
