using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersThreading
{
    class ConsoleHelper
    {
        public static object LockObject = new Object();
        public static void TakeMaterials()
        {
            lock (LockObject)
            {
                Console.WriteLine("Слуга взял материалы и передал курильщику _");
            }
        }

        public static void Smoke()
        {
            lock (LockObject)
            {
                Console.WriteLine("Курильщик _ курит");
            }
        }
    }
}
