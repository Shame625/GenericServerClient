using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Test;
using System.Threading.Tasks;

namespace ServerInfrastructure.Handles
{
    public static class TestHandle
    {
        public async static Task<Result> SpawnObject(BasePacket bp, Connection c)
        {
            var spawnObjectPacket = (CMSG_SpawnObject)bp;

            var result = new SMSG_SpawnObject { objType = spawnObjectPacket.objType, x = spawnObjectPacket.x, y = spawnObjectPacket.y, z = spawnObjectPacket.z };

            return new Result { Packet = result, IsVoidResult = false };
        }
    }
}
