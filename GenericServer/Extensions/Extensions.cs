using Infrastructure.Packets;
using ServerInfrastructure;
using System.Net.Sockets;
using static Infrastructure.Enums.Enums;

namespace GenericServer.Extensions
{
    public static class Extensions
    {
        public static Connection HandleSocket(this Socket socket)
        {
            //Check if client is already in dictionary, if not add him
            try
            {
                return Connections.connectedClients[socket];
            }
            //If it is not in dictionary, add, return
            catch
            {
                Connections.connectedClients[socket] = new Connection(socket);
                return Connections.connectedClients[socket];
            }
        }

        public static PacketType GetPacketType(this BasePacket bp)
        {
            return PacketHandler.packets[bp.Id].packetType;
        }
    }
}
