using System;
using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using PowerReader.Input;

namespace PowerReader
{
    public class Program
    {
        public static void Main()
        {
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
            led.Write(true);

            VoltageDivider voltageReader = new VoltageDivider(Pins.GPIO_PIN_A1, 470000, 4700);
            Acs712 currentReader = new Acs712(Pins.GPIO_PIN_A2, Acs712.Range.ThirtyAmps);

            EmonCmsProxy.Start();
            MpptOptimizer.Start();

            led.Write(false);
            led.Dispose();
            GC.WaitForPendingFinalizers();

            //Random r = new Random((int) DateTime.Now.Ticks);
            while (true)
            {
                double current = //r.NextDouble() / double.MaxValue * 10;
                    currentReader.Read();
                double voltage = //r.NextDouble() / double.MaxValue * 3; 
                    voltageReader.Read();

                EmonCmsProxy.Push(current, voltage);
                MpptOptimizer.Push(current, voltage);

                Thread.Sleep(50);
            }
        }

    }
}
