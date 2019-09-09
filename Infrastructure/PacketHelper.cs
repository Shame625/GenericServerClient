using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class PacketHelper
    {
        public static void PrintData(this byte[] data, bool isSent)
        {
            var temp = new byte[] { data[0], data[1] };

            var packettype = (OpCodes)BitConverter.ToUInt16(temp);

            try
            {
                var dataAsString = BitConverter.ToString(data).Replace("-", " ");
                var prefix = isSent ? "[SENT] " : "[REC] ";
                Console.WriteLine(prefix + DateTime.Now + "\nPacket: " + PacketStorage.packets[packettype] + "\t\tPacketSize: " + data.Length + "\n" + dataAsString);
            }
            catch
            {
                var prefix = isSent ? "[SENT] " : "[REC] ";
                Console.WriteLine(prefix + DateTime.Now + "\nPacket: Unknown Type \t\tPacketSize: " + data.Length +
                                  "\n" + BitConverter.ToString(data).Replace("-", " "));
            }
        }
    }
}
