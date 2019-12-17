using Infrastructure.Enums;
using System.Runtime.InteropServices;

namespace Infrastructure.Packets.Test
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CMSG_SpawnObject : BasePacket
    {
        public CMSG_SpawnObject() : base(OpCodes.CMSG_SpawnObject) { }

        public Objects objType;
        public float x;
        public float y;
        public float z;
    }
}
