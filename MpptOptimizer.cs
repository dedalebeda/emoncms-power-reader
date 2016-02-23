using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace PowerReader
{
    public static class MpptOptimizer
    {
        private static readonly Thread _worker = new Thread(Work);
        private static double _previousPower, _currentPower;
        private static bool _raiseDuty;
        private static PWM _port;
        private const double DutyStep = 0.05;
        private const double MaxDuty = (100000 - 240) / 100000; //120us idle

        public static void Start()
        {
            _port = new PWM(PWMChannels.PWM_PIN_D9, 100, 0.5, false);
            _port.Frequency = 100;
            _port.DutyCycle = 0.5;

            _worker.Start();
            _port.Start();
        }

        public static void Push(double current, double voltage)
        {
            lock (_worker)
            {
                _currentPower = current * voltage;
            }
        }

        private static void Work()
        {
            double deltaPower;

            while (true)
            {
                lock (_worker)
                {
                    deltaPower = _currentPower - _previousPower;
                    _previousPower = _currentPower;
                }

                if (deltaPower < 0)
                    _raiseDuty = !_raiseDuty;

                if (_raiseDuty)
                    _port.DutyCycle += DutyStep;
                else
                    _port.DutyCycle -= DutyStep;

                Thread.Sleep(100);
            }
        }
    }
}
