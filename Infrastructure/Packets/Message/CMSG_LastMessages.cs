using System.Runtime.InteropServices;

namespace Infrastructure.Packets.Message
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class CMSG_LastMessages : BasePacket
    {
        public CMSG_LastMessages() : base(OpCodes.CMSG_LastMessages) { }
    }
}
