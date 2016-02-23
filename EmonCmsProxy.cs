using System;
using Microsoft.SPOT;
using System.Collections;
using System.Threading;
using System.Net;

namespace PowerReader
{
    /// <summary>
    /// Class is static only for better performance
    /// </summary>
    public static class EmonCmsProxy
    {
        public static string ApiKey = "d5662f82cbbfc7e4a9950de818d1afe7";
        public static string Node = "1";
        public static string ServerUrl = "http://212.20.105.58/emoncms";
        public static int WriteIntervalMiliseconds = 10000;

        private static readonly Thread _worker = new Thread(Work);
        private static string _requestUri;

        private static double _voltageReadSum, _currentReadSum;
        private static int _readingCount;

        /// <summary>
        /// Start processing data from queue
        /// </summary>
        public static void Start()
        {
            _requestUri = ServerUrl + "/input/post.json?node=" + Node + "&apikey=" + ApiKey + "&json={current:"; //{2}, voltage:{3}}}""

            _worker.Start();
        }

        public static void Push(double current, double voltage)
        {
            lock (_worker)
            {
                _voltageReadSum += voltage;
                _currentReadSum += current;
                _readingCount++;
            }
        }

        private static void Work()
        {
            while (true)
            {
                if (_readingCount != 0)
                {
                    double voltage, current;
                    lock (_worker)
                    {
                        voltage = _voltageReadSum / _readingCount;
                        current = _currentReadSum / _readingCount;

                        _voltageReadSum = 0;
                        _currentReadSum = 0;
                        _readingCount = 0;
                    }

                    string url = _requestUri + current + ", voltage:" + voltage + "}";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    Debug.Print(url);

                    try
                    {
                        request.GetResponse();
                    }
                    catch (Exception e)
                    {
                        Debug.Print(e.Message);
                    }
                }

                Thread.Sleep(WriteIntervalMiliseconds);
            }
        }
    }
}
