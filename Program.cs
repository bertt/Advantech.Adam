using System;

namespace Advantech.Adam
{
    class Program
    {
        static void Main()
        {
            const string ipAdam6060 = "192.168.24.86";
            const string ipAdam6017 = "192.168.24.76";
            Adam6060Demo(ipAdam6060);
            Adam6017Demo(ipAdam6017);
            Console.ReadKey();
        }

        private static void Adam6017Demo(string ipAdam6017)
        {
            // initialize
            var adam6017 = new Adam6017(ipAdam6017);
            // make connection
            var isConnected = adam6017.Connect();
            if (isConnected)
            {
                // read firmware version
                var firmwareVersion = adam6017.GetFirmWareVersion();

                Console.WriteLine("Start Adam6017 demo");
                Console.WriteLine("Adam6017 connected: " + true);
                Console.WriteLine("Adam6017 firmware:" + firmwareVersion);

                for (var i = 0; i < adam6017.Channels; i++)
                {
                    var val = adam6017.ReadInputChannelAsString(i);
                    Console.WriteLine("Ch-"+ i + ": " + val);
                }

                adam6017.Disconnect();
                Console.WriteLine("End Adam6017 demo");
            }
            else
            {
                Console.WriteLine("No connection to Adam6017 :-(");
            }
            Console.WriteLine();
        }

        private static void Adam6060Demo(string ipAdam6060)
        {
            var adam6060 = new Adam6060(ipAdam6060);
            var isConnected = adam6060.Connect();

            if (isConnected)
            {
                var firmwareVersion = adam6060.GetFirmWareVersion();
                adam6060.WriteOutputChannel(0, true);
                var inputChannels = adam6060.ReadInputChannels();
                var outputChannels = adam6060.ReadOutputChannels();
                adam6060.Disconnect();

                Console.WriteLine("Start Adam6060 demo");
                Console.WriteLine("Adam6060 connected: " + true);
                Console.WriteLine("Adam606 firmware:" + firmwareVersion);
                Console.WriteLine("Adam6060 Input channels: " + string.Join(", ", inputChannels));
                Console.WriteLine("Adam6060 Output channels:" + string.Join(", ", outputChannels));
                Console.WriteLine("End Adam6060 demo");
            }
            else
            {
                Console.WriteLine("No connection to Adam6060 :-(");
            }
            Console.WriteLine();
        }
    }
}
