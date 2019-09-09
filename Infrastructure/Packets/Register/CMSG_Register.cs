using System.Runtime.InteropServices;

namespace Infrastructure.Packets.Register
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CMSG_Register : BasePacket
    {
        public CMSG_Register() : base(OpCodes.CMSG_Register) { }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string UserName;
    }
}
