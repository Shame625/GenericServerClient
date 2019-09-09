using System.Runtime.InteropServices;

namespace Infrastructure.Packets.Message
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class SMSG_Message : BasePacket
    {
        public SMSG_Message() : base(OpCodes.SMSG_Message) { }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Message;
    }
}
