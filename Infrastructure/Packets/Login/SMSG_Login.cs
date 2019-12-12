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

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public Character[] Characters;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Character
        {
            public long CharacterId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string Name;

            public uint Level;
            public Class Class;
            public override string ToString()
            {
                return string.Format($"CharId: {CharacterId} Name: {Name} Level: {Level} Class: {Class.ToString()}");
            }
        }
    }
}
