using System;

namespace Advantech.Adam
{
    class Program
    {
        static void Main()
        {
            // code ideas from here: https://github.com/Mib314/Sol2Reg.ShortService/tree/master/Sol2Reg.IO.ADAM6000Com
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

                // Read enabled channels
                var enabledChannels = adam6017.GetEnabledChannels();

                var inputChannel0 = adam6017.ReadInputChannels(0);
                adam6017.Disconnect();
                // write values to screen
                Console.WriteLine("Start Adam6017 demo");
                Console.WriteLine("Adam6017 connected: " + true);
                Console.WriteLine("Adam6017 firmware:" + firmwareVersion);
                Console.WriteLine("Adam6017 enabled channels:" + String.Join(", ", enabledChannels));
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
            // initialize
            var adam6060 = new Adam6060(ipAdam6060);
            // make connection
            var isConnected = adam6060.Connect();

            if (isConnected)
            {
                // read firmware version
                var firmwareVersion = adam6060.GetFirmWareVersion();
                // write true value to output port 0 (is equal to channel 17)
                adam6060.WriteOutputChannel(0, true);
                // read input and output channels
                var inputChannels = adam6060.ReadInputChannels();
                var outputChannels = adam6060.ReadOutputChannels();
                // disconnect
                adam6060.Disconnect();

                // write values to screen
                Console.WriteLine("Start Adam6060 demo");
                Console.WriteLine("Adam6060 connected: " + true);
                Console.WriteLine("Adam606 firmware:" + firmwareVersion);
                Console.WriteLine("Adam6060 Input channels: " + String.Join(", ", inputChannels));
                Console.WriteLine("Adam6060 Output channels:" + String.Join(", ", outputChannels));
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
