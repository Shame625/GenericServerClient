using System.Runtime.InteropServices;
using static Infrastructure.Enums.Enums;

namespace Infrastructure.Packets.Register
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class SMSG_Register : BasePacket
    {
        public SMSG_Register() : base(OpCodes.SMSG_Register) { }

        public RegistrationStatus status;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Token;
    }
}
