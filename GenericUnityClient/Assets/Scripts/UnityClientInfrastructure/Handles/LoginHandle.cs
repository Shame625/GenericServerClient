using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Login;
using System;

namespace UnityClientInfrastructure.Handles
{
    public static class LoginHandle
    {
        public static Result LoginChallenge(BasePacket bp)
        {
            var loginPacket = (SMSG_Login)bp;

            if(loginPacket.status == LoginStatus.Ok)
            {
                ClientStates.UserName = loginPacket.UserName;
                Console.WriteLine("Welcome " + ClientStates.UserName);
                ClientStates.loginStatus = 1;
            }
            else
            {
                Console.WriteLine("Failed to login!");
            }

            return new Result { IsVoidResult = true };
        }
    }
}
