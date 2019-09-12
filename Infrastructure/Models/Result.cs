using Infrastructure.Packets;

namespace Infrastructure.Models
{
    public class Result
    {
        public bool IsVoidResult { get; set; }
        public BasePacket Packet { get; set; }

        public byte[] PacketBytes => Packet.Serialize();
    }
}
