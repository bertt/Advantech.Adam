using System.Net.Sockets;

namespace Advantech.Adam
{
    public class Adam6060
    {
        private readonly string _ip;
        private AdamSocket _adamSocket;
        private const int IdSartForInputChanel = 1;
        private const int IdSartForOutputChannel = 17;
        private static readonly int TotalDigitalOutputChannels=DigitalOutput.GetChannelTotal(Adam6000Type.Adam6060);
        private static readonly int TotalDigitalInputChannels=DigitalInput.GetChannelTotal(Adam6000Type.Adam6060);

        public Adam6060(string ip)
        {
           _ip = ip;
        }

        public bool Connect()
        {
            _adamSocket = new AdamSocket();
            return  _adamSocket.Connect(_ip, ProtocolType.Tcp, 502);
        }

        public void Disconnect()
        {
            _adamSocket.Disconnect();
        }

        public string GetFirmWareVersion()
        {
            string firmWareVersion;
            _adamSocket.Configuration().GetFirmwareVer(out firmWareVersion);
            return firmWareVersion;
        }

        public bool[] ReadInputChannels(){
            bool[] digitalInputChannels;
            var modbus = _adamSocket.Modbus();
            modbus.ReadCoilStatus(IdSartForInputChanel, TotalDigitalInputChannels, out digitalInputChannels);
            return digitalInputChannels;
        }

        public bool[] ReadOutputChannels(){
            bool[] digitalOutputChannels;
            var modbus = _adamSocket.Modbus();
            modbus.ReadCoilStatus(IdSartForOutputChannel, TotalDigitalOutputChannels, out digitalOutputChannels);
            return digitalOutputChannels;
        }
        public bool WriteOutputChannel(int channel, bool val)
        {
            var modbus = _adamSocket.Modbus();
            var res = modbus.ForceSingleCoil(IdSartForOutputChannel+channel, val);
            return res;
        }
    }
}
