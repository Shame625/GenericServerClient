using Infrastructure.Enums;
using System.Runtime.InteropServices;

namespace Infrastructure.Packets.Login
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class SMSG_Login : BasePacket
    {
        public SMSG_Login() : base(OpCodes.SMSG_Login) { }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string UserName;

        public LoginStatus status;
    }
}
