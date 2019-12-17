using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Message;
using System;

namespace ClientInfrastructure.Handles
{
    public class ChatHandle
    {
        public virtual Result MessageReceived(BasePacket bp)
        {
            var messagePacket = (SMSG_Message)bp;
            Console.WriteLine(messagePacket.GetTimestamp().ToString("dd/MM/yyyy hh:mm:ss") + " [" + messagePacket.UserName + "]: " + messagePacket.Message);

            return new Result { IsVoidResult = true };
        }

        internal virtual Result LastMessages(BasePacket bp)
        {
            var messagePacket = (SMSG_LastMessages)bp;

            int i = 0;
            foreach(var m in messagePacket.Messages)
            {
                i++;
                Console.WriteLine(i + ". " + m);
            }
            return new Result { IsVoidResult = true };
        }
    }
}
