using System.Net.Sockets;

namespace Advantech.Adam
{
    public class Adam6017
    {
        private string ip;
        private AdamSocket _adamSocket;
        private static readonly int TotalAnalogInputChannels = AnalogInput.GetChannelTotal(Adam6000Type.Adam6017);
        private int _channelsTotal;
        private Adam6000Type adamtype= Adam6000Type.Adam6017;

        public Adam6017(string ip)
        {
           this.ip = ip;
        }

        public bool Connect()
        {
            _adamSocket = new AdamSocket();
            _channelsTotal = AnalogInput.GetChannelTotal(adamtype);
            return _adamSocket.Connect(AdamType.Adam6000, ip, ProtocolType.Tcp);
        }

        public string GetFloatFormat(byte b)
        {
            return AnalogInput.GetFloatFormat(adamtype, b);
        }

        public void Disconnect()
        {
            _adamSocket.Disconnect();
        }

        public int Channels => _channelsTotal;

        public string GetFirmWareVersion()
        {
            string firmWareVersion;
            _adamSocket.Configuration().GetFirmwareVer(out firmWareVersion);
            return firmWareVersion;
        }

        public int[] ReadInputRegs()
        {
            const int start = 1;
            int[] iData;
            _adamSocket.Modbus().ReadInputRegs(start, _channelsTotal, out iData);
            return iData;
        }

        public bool[] GetEnabledChannels()
        {
            bool[] enabled;
            _adamSocket.AnalogInput().GetChannelEnabled(TotalAnalogInputChannels, out enabled);
            return enabled;
        }

        public byte ReadInputChannelAsByte(int channel)
        {
            byte b;
            _adamSocket.AnalogInput().GetInputRange(channel, out b);
            return b;
        }

        public float ReadInputChannelAsFloat(int channel)
        {
            var b = ReadInputChannelAsByte(channel);
            var inputRegs = ReadInputRegs();
            var res = AnalogInput.GetScaledValue(adamtype, b, inputRegs[channel]);
            return res;
        }

        public string ReadInputChannelAsString(int channel)
        {
            var b = ReadInputChannelAsByte(channel);
            var ff = GetFloatFormat(b);
            var inputRegs = ReadInputRegs();
            var res = AnalogInput.GetScaledValue(adamtype, b, inputRegs[channel]);
            var s = res.ToString(ff) + " " + AnalogInput.GetUnitName(adamtype, b);
            return s;
        }
    }
}
