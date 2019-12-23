using GenericEntity.Dbo;
using ServerInfrastructure.Enums;

namespace ServerInfrastructure.Models
{
    public class ServerUser : Infrastructure.Models.User
    {
        public User UserData { get; set; }

        public void SetFilterAndPortectionlevel(PacketFilter packetFilter, PacketProtectionLevel packetProtectionLevel)
        {
            this.packetFilter = packetFilter;
            this.packetProtectionLevel = packetProtectionLevel;
        }

        public PacketFilter packetFilter = PacketFilter.Anonymous;
        public PacketProtectionLevel packetProtectionLevel = PacketProtectionLevel.Guest;
    }
}
