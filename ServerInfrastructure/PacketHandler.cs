using Infrastructure;
using Infrastructure.Enums;
using Infrastructure.Handles;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Error;
using Infrastructure.Packets.Login;
using Infrastructure.Packets.Message;
using Infrastructure.Packets.Register;
using Infrastructure.Packets.Test;
using ServerInfrastructure.Enums;
using ServerInfrastructure.Handles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerInfrastructure
{
    public static class PacketHandler
    {
        public delegate Task<Result> GenericDelegate(BasePacket bp, Connection c);

        public static Dictionary<OpCodes, OpCodeFunction> packets = new Dictionary<OpCodes, OpCodeFunction>()
        {
            { OpCodes.CMSG_Login, new OpCodeFunction(typeof(SMSG_Login), LoginHandle.LoginChallenge, PacketType.Nothing, PacketFilter.Anonymous, PacketProtectionLevel.Guest) },
            { OpCodes.SMSG_Login, new OpCodeFunction(typeof(SMSG_Login), null, PacketType.ReturnToSender, PacketFilter.Anonymous, PacketProtectionLevel.Guest) },

            { OpCodes.CMSG_Register, new OpCodeFunction(typeof(SMSG_Register), RegisterHandle.RegisterChallange, PacketType.Nothing, PacketFilter.Anonymous, PacketProtectionLevel.Guest) },
            { OpCodes.SMSG_Register, new OpCodeFunction(typeof(SMSG_Register), null, PacketType.ReturnToSender) },

            { OpCodes.CMSG_Message, new OpCodeFunction(typeof(CMSG_Message), ChatHandle.MessageSent, PacketType.Nothing, PacketFilter.LoggedIn, PacketProtectionLevel.User) },
            { OpCodes.SMSG_Message, new OpCodeFunction(typeof(SMSG_Message), null, PacketType.Local) },

            { OpCodes.CMSG_LastMessages, new OpCodeFunction(typeof(CMSG_LastMessages), ChatHandle.LastMessages, PacketType.Nothing, PacketFilter.LoggedIn, PacketProtectionLevel.User) },
            { OpCodes.SMSG_LastMessages, new OpCodeFunction(typeof(SMSG_LastMessages), null, PacketType.ReturnToSender) },

            { OpCodes.CMSG_SpawnObject, new OpCodeFunction(typeof(CMSG_SpawnObject), TestHandle.SpawnObject, PacketType.Nothing, PacketFilter.LoggedIn, PacketProtectionLevel.Admin) },
            { OpCodes.SMSG_SpawnObject, new OpCodeFunction(typeof(SMSG_SpawnObject), null, PacketType.Local) },

            { OpCodes.SMSG_Error, new OpCodeFunction(typeof(SMSG_Error), null, PacketType.ReturnToSender) },
        };

        public class OpCodeFunction
        {
            public Type type;
            public GenericDelegate operation;
            public PacketType packetType;
            public PacketFilter filter;
            public PacketProtectionLevel protectionLevel;

            public OpCodeFunction(Type type, GenericDelegate operation, PacketType packetType = PacketType.Nothing, PacketFilter filter = PacketFilter.LoggedIn, PacketProtectionLevel protectionLevel = PacketProtectionLevel.Admin)
            {
                this.type = type;
                this.operation = operation;
                this.packetType = packetType;
                this.filter = filter;
                this.protectionLevel = protectionLevel;
            }
        }
    }
    public static class PacketOperations
    {
        public static Task<Result> Execute(this BasePacket bp, Connection c)
        {
            try
            {
                if ((uint)PacketHandler.packets[bp.Id].filter == (uint)c.GetPacketFilter())
                {
                    if ((uint)PacketHandler.packets[bp.Id].protectionLevel <= (uint)c.GetPacketProtectionLevel())
                    {
                        return PacketHandler.packets[bp.Id].operation(bp, c);
                    }
                    else
                    {
                        var errorPacket = new Result { IsVoidResult = false, Packet = new SMSG_Error { errorType = ErrorType.ProtectionLevelError } };
                        return Task.FromResult(errorPacket);
                    }
                }
                else
                {
                    var errorPacket = new Result { IsVoidResult = false, Packet = new SMSG_Error { errorType = ErrorType.FilterError } };
                    return Task.FromResult(errorPacket);
                }

            }
            catch
            {
                var errorPacket = new Result { IsVoidResult = false, Packet = new SMSG_Error { errorType = ErrorType.ParsingError } };
                return Task.FromResult(errorPacket);
            }
        }
    }

}
