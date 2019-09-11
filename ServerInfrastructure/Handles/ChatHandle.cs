using DatabaseCore;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Message;

namespace ServerInfrastructure.Handles
{
    public static class ChatHandle
    {
        public static Result MessageSent(BasePacket bp, ref Connection c)
        {
            var messagePacket = (CMSG_Message)bp;

            var response = new SMSG_Message() { UserName = c.User.UserName, Message = messagePacket.Message };

            using (var context = new ApplicationContext())
            {

            }

            return new Result() { ByteResult = response.Serialize(), IsVoidResult = false };
        }
    }
}
