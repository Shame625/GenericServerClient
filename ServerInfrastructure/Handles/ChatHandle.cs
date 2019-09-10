using DatabaseCore;
using Infrastructure;
using Infrastructure.Packets;
using Infrastructure.Packets.Message;

namespace ServerInfrastructure.Handles
{
    public static class ChatHandle
    {
        public static byte[] MessageSent(BasePacket bp, ref Connection c)
        {
            var messagePacket = (CMSG_Message)bp;

            var response = new SMSG_Message() { UserName = c.User.UserName, Message = messagePacket.Message };

            using (var context = new ApplicationContext())
            {

            }

            return response.Serialize();
        }
    }
}
