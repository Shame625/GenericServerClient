﻿using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Login;
using Infrastructure.Packets.Message;
using Infrastructure.Packets.Register;
using Infrastructure.Packets.Test;
using System;
using System.Collections.Generic;
using UnityClientInfrastructure.Handles;

namespace UnityClientInfrastructure
{
    public static class PacketHandler
    {
        public delegate Result GenericDelegate(BasePacket bp);

        public static Dictionary<OpCodes, OpCodeFunction> packets = new Dictionary<OpCodes, OpCodeFunction>()
        {
            { OpCodes.CMSG_Login, new OpCodeFunction(typeof(SMSG_Login), null) },
            { OpCodes.SMSG_Login, new OpCodeFunction(typeof(SMSG_Login), LoginHandle.LoginChallenge) },

            { OpCodes.CMSG_Register, new OpCodeFunction(typeof(SMSG_Register), null) },
            { OpCodes.SMSG_Register, new OpCodeFunction(typeof(SMSG_Register), RegisterHandle.RegisterChallange) },

            { OpCodes.CMSG_Message, new OpCodeFunction(typeof(CMSG_Message), null) },
            { OpCodes.SMSG_Message, new OpCodeFunction(typeof(SMSG_Message), ChatHandle.MessageReceived) },

            { OpCodes.CMSG_LastMessages, new OpCodeFunction(typeof(CMSG_LastMessages), null) },
            { OpCodes.SMSG_LastMessages, new OpCodeFunction(typeof(SMSG_LastMessages), ChatHandle.LastMessages) },

            { OpCodes.CMSG_SpawnObject, new OpCodeFunction(typeof(CMSG_SpawnObject), null) },
            { OpCodes.SMSG_SpawnObject, new OpCodeFunction(typeof(SMSG_SpawnObject), TestHandle.SpawnObject) }
        };

        public class OpCodeFunction
        {
            public Type type;
            public GenericDelegate operation;
            public bool global;

            public OpCodeFunction(Type type, GenericDelegate operation, bool global = false)
            {
                this.type = type;
                this.operation = operation;
                this.global = global;
            }
        }

    }
    public static class PacketOperations
    {
        public static Result Execute(this BasePacket bp)
        {
            try
            {
                return PacketHandler.packets[bp.Id].operation(bp);
            }
            catch
            {
                return null;
            }
        }
    }

}
