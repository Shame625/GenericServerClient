﻿using Infrastructure.Packets.Error;
using Infrastructure.Packets.Login;
using Infrastructure.Packets.Message;
using Infrastructure.Packets.Register;
using Infrastructure.Packets.Test;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public enum OpCodes
    {
        CMSG_Login = 0x0000,
        SMSG_Login = 0x0001,

        CMSG_Register = 0x0020,
        SMSG_Register = 0x0021,

        CMSG_Message = 0x1000,
        SMSG_Message = 0x1001,

        CMSG_LastMessages = 0x1002,
        SMSG_LastMessages = 0x1003,

        SMSG_Error = 0x2000,

        CMSG_SpawnObject = 0xFFEE,
        SMSG_SpawnObject = 0xFFFF
    }

    public static class PacketStorage
    {
        public static Dictionary<OpCodes, Type> packets = new Dictionary<OpCodes, Type>()
        {
            {OpCodes.CMSG_Login, typeof(CMSG_Login)},
            {OpCodes.SMSG_Login, typeof(SMSG_Login)},

            {OpCodes.CMSG_Register, typeof(CMSG_Register)},
            {OpCodes.SMSG_Register, typeof(SMSG_Register)},

            {OpCodes.CMSG_Message, typeof(CMSG_Message)},
            {OpCodes.SMSG_Message, typeof(SMSG_Message)},

            {OpCodes.CMSG_LastMessages, typeof(CMSG_LastMessages)},
            {OpCodes.SMSG_LastMessages, typeof(SMSG_LastMessages)},

            {OpCodes.SMSG_Error, typeof(SMSG_Error)},

            {OpCodes.CMSG_SpawnObject, typeof(CMSG_SpawnObject)},
            {OpCodes.SMSG_SpawnObject, typeof(SMSG_SpawnObject)}
        };
    }
}
