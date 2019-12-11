using System.Runtime.InteropServices;
using System;
using System.Linq;

namespace Infrastructure.Packets.Message
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class SMSG_LastMessages : BasePacket
    {
        public SMSG_LastMessages() : base(OpCodes.SMSG_LastMessages) 
        {
        }

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public MessageHistory[] Messages;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MessageHistory
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Value;

        public static implicit operator string(MessageHistory source)
        {
            return source.Value;
        }

        public static implicit operator MessageHistory(string source)
        {
            return new MessageHistory
            {
                Value = source
            };
        }
    }
}
