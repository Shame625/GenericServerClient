using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Message;
using System;
using UnityEngine;

namespace UnityClientInfrastructure.Handles
{
     
    public class ChatHandle
    {
        private static ChatManager _chatManager;
        private static ChatManager ChatManager
        {
            get
            {
                if(_chatManager == null)
                    _chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
                return _chatManager;
            }
        }

        public static Result MessageReceived(BasePacket bp)
        {
            var messagePacket = (SMSG_Message)bp;
            ChatManager.MessageReceived(messagePacket);

            return new Result { IsVoidResult = true };
        }

        public static Result LastMessages(BasePacket bp)
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
