using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Infrastructure.Packets
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public abstract class BasePacket
    {
        public OpCodes Id { get; set; }

        public byte[] Execute(ref Connection c)
        {
            try
            {
                return PacketStorage.packets[Id].operation(this, ref c);
            }
            catch
            {
                return null;
            }
        }
    }

    public static class PacketOperations
    {
        public static byte[] Execute(BasePacket bp, ref Connection c)
        {
            try
            {
                return PacketStorage.packets[bp.Id].operation(bp, ref c);
            }
            catch
            {
                return null;
            }
        }
    }
}
