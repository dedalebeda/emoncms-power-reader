using Microsoft.SPOT.Hardware;

namespace PowerReader.Input
{
    /// <summary>
    /// Reader for voltage divider <see cref="https://en.wikipedia.org/wiki/Voltage_divider"/>. Output voltage is measured on resistor2.
    /// </summary>
    public class VoltageDivider : Analog
    {
        /// <param name="resistor1">Value of resistor 1 in Ohms</param>
        /// <param name="resistor2">Value of resistor 2 in Ohms</param>
        public VoltageDivider(Cpu.Pin channel, double resistor1, double resistor2) : base(channel)
        {
            double divider = resistor2 / (resistor1 + resistor2);

            Scale *= divider;
            Offset *= divider;
        }
    }
}
