using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Test;
using System;
using UnityEngine;

namespace UnityClientInfrastructure.Handles
{
    public static class TestHandle
    {
        public static Result SpawnObject(BasePacket bp)
        {
            var spawnObjectPacket = (SMSG_SpawnObject)bp;

            Debug.Log(spawnObjectPacket);

            try
            {
                switch (spawnObjectPacket.objType)
                {
                    case Objects.Cube:
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(spawnObjectPacket.x, spawnObjectPacket.y, spawnObjectPacket.z);
                        break;

                    case Objects.Sphere:
                        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        sphere.transform.position = new Vector3(spawnObjectPacket.x, spawnObjectPacket.y, spawnObjectPacket.z);
                        break;
                    default:
                        Debug.Log("Failed to spawn object");
                        break;
                }
            }
            catch(Exception ex)
            {
                Debug.Log("Exception: " + ex.Message);
            }
            return new Result { IsVoidResult = true };
        }
    }
}
