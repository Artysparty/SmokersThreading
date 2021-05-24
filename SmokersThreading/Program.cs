using System;
using System.Threading;

namespace SmokersThreading
{
    class SmokersThreading
    {
        private static Mutex _mutex = new Mutex(false);
        private static AutoResetEvent _signalFromServant1 = new AutoResetEvent(false);
        private static AutoResetEvent _signalFromServant2 = new AutoResetEvent(false);
        private static AutoResetEvent _signalFromServant3 = new AutoResetEvent(false);
        private static Boolean _isTableBusy = false;
        private static void Main(string[] args)
        {
            Thread Servant = new Thread(servant);
            Thread SmokerMatch = new Thread(smokerMatch);
            Thread SmokerTobacco = new Thread(smokerTobacco);
            Thread SmokerPaper = new Thread(smokerPaper);

            SmokerMatch.Start();
            SmokerTobacco.Start();
            SmokerPaper.Start();
            Servant.Start();
        }

        static void servant()
        {
            while (true)
            {
                _mutex.WaitOne();

                int rand = new Random().Next(1, 4);

                if (rand == 1 && _isTableBusy == false) {
                    Console.WriteLine("Слуга взял бумагу и табак и положил их на стол.\n");
                    Thread.Sleep(new Random().Next(100, 2000));
                    _isTableBusy = true;
                    _signalFromServant1.Set();

                } else if (rand == 2 && _isTableBusy == false) {
                    Console.WriteLine("Слуга взял бумагу и спички и положил их на стол.\n");
                    Thread.Sleep(new Random().Next(100, 2000));
                    _isTableBusy = true;
                    _signalFromServant2.Set();
                } else if (_isTableBusy == false)
                {
                    Console.WriteLine("Слуга взял спички и табак и положил их на стол.\n");
                    Thread.Sleep(new Random().Next(100, 2000));
                    _isTableBusy = true;
                    _signalFromServant3.Set();
                }

                _mutex.ReleaseMutex();
            }
        }

        static void smokerMatch()
        {
            while (true)
            {
                _signalFromServant1.WaitOne();
                _mutex.WaitOne();

                Console.WriteLine("Курильщик С взял табак и бумагу\n");
                _isTableBusy = false;

                _mutex.ReleaseMutex();
                _signalFromServant1.Reset();

                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик С курит\n");
                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик С докурил\n");
            }
        }

        static void smokerTobacco()
        {
            while (true)
            {
                _signalFromServant2.WaitOne();
                _mutex.WaitOne();

                Console.WriteLine("Курильщик Т взял бумагу и спички\n");
                _isTableBusy = false;

                _mutex.ReleaseMutex();
                _signalFromServant2.Reset();

                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик Т курит\n");
                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик Т докурил\n");
            }
        }

        static void smokerPaper() 
        {
            while (true)
            {
                _signalFromServant3.WaitOne();
                _mutex.WaitOne();

                Console.WriteLine("Курильщик Б взял табак и спички\n");
                _isTableBusy = false;

                _mutex.ReleaseMutex();
                _signalFromServant3.Reset();

                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик Б курит\n");
                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик Б докурил\n");
            }
        }
    }
}