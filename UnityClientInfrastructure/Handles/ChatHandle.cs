using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Message;
using System;
using UnityEngine;

namespace UnityClientInfrastructure.Handles
{
    public class ChatHandle
    {
        public static Result MessageReceived(BasePacket bp)
        {
            var messagePacket = (SMSG_Message)bp;
            Debug.Log(messagePacket.GetTimestamp().ToString("dd/MM/yyyy hh:mm:ss") + " [" + messagePacket.UserName + "]: " + messagePacket.Message);

            return new Result { IsVoidResult = true };
        }

        static internal  Result LastMessages(BasePacket bp)
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
