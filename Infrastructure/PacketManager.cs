using Infrastructure.Packets;
using System;
using System.Runtime.InteropServices;

namespace Infrastructure
{
    public static class PacketManager
    {
        public static byte[] Serialize(this BasePacket packet)
        {
            int structsize = Marshal.SizeOf(packet);
            IntPtr buffer = Marshal.AllocHGlobal(structsize);
            Marshal.StructureToPtr(packet, buffer, false);
            byte[] streamdata = new byte[structsize];
            Marshal.Copy(buffer, streamdata, 0, structsize);
            Marshal.FreeHGlobal(buffer);
            return streamdata;
        }

        public static BasePacket Deserialize(this byte[] data)
        {
            var packetId = data.GetPacketId();
            Type type = PacketStorage.packets[packetId];

            int rawsize = Marshal.SizeOf(type);
            if (rawsize > data.Length) return null;
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.Copy(data, 0, buffer, rawsize);
            object retobj = Marshal.PtrToStructure(buffer, type);
            Marshal.FreeHGlobal(buffer);

            return (BasePacket)retobj;
        }

        public static OpCodes GetPacketId(this byte[] data)
        {
            byte[] typeBytes = new byte[2];
            typeBytes[0] = data[0];
            typeBytes[1] = data[1];

            return (OpCodes)BitConverter.ToUInt16(typeBytes);
        }
    }
}
