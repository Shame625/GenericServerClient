using ClientInfrastructure.Handles;
using Infrastructure;
using Infrastructure.Handles;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Login;
using Infrastructure.Packets.Message;
using Infrastructure.Packets.Register;
using System;
using System.Collections.Generic;

namespace ClientInfrastructure
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
            { OpCodes.SMSG_Message, new OpCodeFunction(typeof(SMSG_Message), new ChatHandle().MessageReceived) },

            { OpCodes.CMSG_LastMessages, new OpCodeFunction(typeof(CMSG_LastMessages), null) },
            { OpCodes.SMSG_LastMessages, new OpCodeFunction(typeof(SMSG_LastMessages), new ChatHandle().LastMessages) },
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
