using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientInfrastructure.Handles
{
    public static class TestHandle
    {
        public static Result SpawnObject(BasePacket bp)
        {
            var spawnObjectPacket = (SMSG_SpawnObject)bp;
            Console.WriteLine("Server spawned: " + spawnObjectPacket.objType.ToString());

            return new Result { IsVoidResult = true };
        }
    }
}
