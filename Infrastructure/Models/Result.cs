using Infrastructure.Packets;

namespace Infrastructure.Models
{
    public class Result
    {
        public bool IsVoidResult { get; set; }
        public byte[] ByteResult { get; set; }
        public BasePacket Packet { get; set; }
    }
}
