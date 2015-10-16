using System.Net.Sockets;
using Advantech.Adam;

namespace Advantech.Adam
{
    public class Adam6017
    {
        private string ip;
        private AdamSocket _adamSocket;
        private static readonly int TotalAnalogInputChannels = AnalogInput.GetChannelTotal(Adam6000Type.Adam6017);
        private static readonly int TotalAnalogOutputChannels = AnalogOutput.GetChannelTotal(Adam6000Type.Adam6017);

        public Adam6017(string ip)
        {
           this.ip = ip;
        }

        public bool Connect()
        {
            _adamSocket = new AdamSocket();
            return  _adamSocket.Connect(ip, ProtocolType.Tcp, 502);
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

        public bool[] GetEnabledChannels()
        {
            bool[] enabled;
            _adamSocket.AnalogInput().GetChannelEnabled(TotalAnalogInputChannels, out enabled);
            return enabled;
        }

        public byte ReadInputChannels(int channel)
        {
            byte byRange;
            _adamSocket.AnalogInput().GetInputRange(channel, out byRange);
            float startup;
            var res = _adamSocket.AnalogOutput().SetCurrentValue(0, 10);
            _adamSocket.AnalogOutput().GetStartupValue(0, out startup);

            return byRange;
        }
    }
}
