using DatabaseCore;
using GenericEntity.Dbo;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Message;
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
                var dbUser = context.Users.SingleOrDefault(o => o.Id == c.User.UserId);
                var message = new Message { Text = messagePacket.Message, User = dbUser };
                await context.Messages.AddAsync(message);
                await context.SaveChangesAsync();
            }

            return new Result { Packet = response, IsVoidResult = false };
        }
        public async static Task<Result> LastMessages(BasePacket bp, Connection c)
        {
            var messagePacket = (CMSG_LastMessages)bp;

            var response = new SMSG_LastMessages() { };

            using (var context = new ApplicationContext())
            {
                var lastMessages = context.Messages.OrderByDescending(o => o.InsertDate).Take(10).Select(o => new MessageHistory { Value = o.Text }).ToArray();

                if (lastMessages.Count() != 10)
                {
                    response.Messages = Enumerable.Range(0, 10).Select(n => new MessageHistory { Value = "" }).ToArray();
                    for (var i = 0; i < lastMessages.Count(); i++)
                    {
                        response.Messages[i] = lastMessages[i];
                    }
                }
                else
                    response.Messages = lastMessages;
            }

            return new Result { Packet = response, IsVoidResult = false };
        }
    }
}
