using Infrastructure.Enums;
using System.Runtime.InteropServices;

namespace Infrastructure.Models
{
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
