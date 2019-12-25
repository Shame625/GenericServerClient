using Infrastructure.Enums;
using System.Runtime.InteropServices;

namespace Infrastructure.Packets.Message
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class SMSG_Message : BasePacket
    {
        public SMSG_Message() : base(OpCodes.SMSG_Message) { }
        public MessageType MessageType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string UserName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Message;
    }
}
