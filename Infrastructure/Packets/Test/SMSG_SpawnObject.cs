using Infrastructure.Enums;
using System.Runtime.InteropServices;

namespace Infrastructure.Packets.Test
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class SMSG_SpawnObject : BasePacket
    {
        public SMSG_SpawnObject() : base(OpCodes.SMSG_SpawnObject) { }

        public Objects objType; 
        public float x;
        public float y;
        public float z;
    }
}
