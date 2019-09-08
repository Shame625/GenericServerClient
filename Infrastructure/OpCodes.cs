using Infrastructure.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public enum OpCodes
    {
        MSG_Handshake_1 = 0x0001,
        MSG_Handshake_2 = 0x0002,
        MSG_Handshake_Final = 0x0003,
        CMSG_GetTime = 0x2000,
        SMSG_GetTime = 0x2001,
    }

    public delegate byte[] GenericDelegate(BasePacket bp, ref Connection c);

    public static class PacketStorage
    {
        public static Dictionary<OpCodes, OpCodeFunction> packets = new Dictionary<OpCodes, OpCodeFunction>()
        {

        };
    }

    public class OpCodeFunction
    {
        public Type type;
        public GenericDelegate operation;

        public OpCodeFunction(Type type, GenericDelegate operation)
        {
            this.type = type;
            this.operation = operation;
        }
    }
}
