using Infrastructure;
using Infrastructure.Handles;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Login;
using Infrastructure.Packets.Message;
using Infrastructure.Packets.Register;
using ServerInfrastructure.Handles;
using System;
using System.Collections.Generic;
using static Infrastructure.Enums.Enums;

namespace ServerInfrastructure
{
    public static class PacketHandler
    {
        public delegate Result GenericDelegate(BasePacket bp, ref Connection c);

        public static Dictionary<OpCodes, OpCodeFunction> packets = new Dictionary<OpCodes, OpCodeFunction>()
        {
            { OpCodes.CMSG_Login, new OpCodeFunction(typeof(SMSG_Login), LoginHandle.LoginChallenge) },
            { OpCodes.SMSG_Login, new OpCodeFunction(typeof(SMSG_Login), null) },

            { OpCodes.CMSG_Register, new OpCodeFunction(typeof(SMSG_Register), RegisterHandle.RegisterChallange) },
            { OpCodes.SMSG_Register, new OpCodeFunction(typeof(SMSG_Register), null) },

            { OpCodes.CMSG_Message, new OpCodeFunction(typeof(CMSG_Message), ChatHandle.MessageSent, PacketType.Local) },
            { OpCodes.SMSG_Message, new OpCodeFunction(typeof(SMSG_Message), null, PacketType.Local) },
        };

        public class OpCodeFunction
        {
            public Type type;
            public GenericDelegate operation;
            public PacketType packetType;

            public OpCodeFunction(Type type, GenericDelegate operation, PacketType packetType = PacketType.ReturnToSender)
            {
                this.type = type;
                this.operation = operation;
                this.packetType = packetType;
            }
        }
    }
    public static class PacketOperations
    {
        public static Result Execute(this BasePacket bp, Connection c)
        {
            try
            {
                return PacketHandler.packets[bp.Id].operation(bp, ref c);
            }
            catch
            {
                return null;
            }
        }
    }

}
