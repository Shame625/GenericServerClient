using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Register;
using Infrastructure.Enums;
using System;

namespace ServerInfrastructure.Handles
{
    public static class RegisterHandle
    {
        public static Result RegisterChallange(BasePacket bp)
        {
            var registerPacket = (SMSG_Register)bp;

            if(registerPacket.status == RegistrationStatus.Ok)
            {
                Console.WriteLine("Successfully registrated");
            }
            else
            {
                Console.WriteLine("Failed to register. Error: " + registerPacket.status.ToString());
            }

            return new Result { IsVoidResult = true };
        }
    }
}
