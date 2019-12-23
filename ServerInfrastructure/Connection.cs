using Infrastructure.Models;
using ServerInfrastructure.Enums;
using ServerInfrastructure.Models;
using System.Net.Sockets;

namespace ServerInfrastructure
{
    public class Connection
    {
        public Socket Socket { get; set; }
        public ServerUser User { get; set; }
        public Connection(Socket socket)
        {
            this.Socket = socket;
        }

        public PacketFilter GetPacketFilter()
        {
            if (User != null)
            {
                return User.packetFilter;
            }
            return PacketFilter.Anonymous;
        }

        public PacketProtectionLevel GetPacketProtectionLevel()
        {
            if(User != null)
            {
                return User.packetProtectionLevel;
            }
            return PacketProtectionLevel.Guest;
        }

        public string ConnectionHandle
        {
            get
            {
                if(User != null && User.UserId != 0)
                {
                    return GetHashCode().ToString() + " | " + User.UserId + " | " + User.UserName;
                }
                else
                {
                    return GetHashCode().ToString();
                }
            }
        }
    }
}