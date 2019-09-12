using DatabaseCore;
using DatabaseCore.Models;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Message;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServerInfrastructure.Handles
{
    public static class ChatHandle
    {
        public async static Task<Result> MessageSent(BasePacket bp, Connection c)
        {
            var messagePacket = (CMSG_Message)bp;

            var response = new SMSG_Message() { UserName = c.User.UserName, Message = messagePacket.Message };

            using (var context = new ApplicationContext())
            {
                var dbUser = context.Users.SingleOrDefault(o => o == c.User);
                var message = new Message { Text = messagePacket.Message, User = dbUser };
                await context.Messages.AddAsync(message);
                await context.SaveChangesAsync();
            }

            return new Result { Packet = response, IsVoidResult = false };
        }
    }
}
