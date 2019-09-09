using System.Runtime.InteropServices;

namespace Infrastructure.Packets.Login
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CMSG_Login : BasePacket
    {
        public CMSG_Login() : base(OpCodes.CMSG_Login) { }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string UserName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Token;
    }
}
