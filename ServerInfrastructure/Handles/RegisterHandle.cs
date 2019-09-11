using DatabaseCore;
using DatabaseCore.Models;
using Infrastructure;
using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Register;
using System.Linq;

namespace ServerInfrastructure.Handles
{
    public static class RegisterHandle
    {
        public static Result RegisterChallange(BasePacket bp, ref Connection c)
        {
            var registerPacket = (CMSG_Register)bp;

            var response = new SMSG_Register() { status = Enums.RegistrationStatus.Fail };

            using (var context = new ApplicationContext())
            {
                var exist = context.Users.Any(o => o.UserName == registerPacket.UserName);

                if (exist)
                {
                    response.status = Enums.RegistrationStatus.AlreadyExists;
                }
                else
                {
                    var newUser = new User() { UserName = registerPacket.UserName };
                    context.Users.Add(newUser);
                    context.SaveChanges();

                    response.status = Enums.RegistrationStatus.Ok;
                    response.Token = "TESTTOKEN";
                }
            }

            return new Result() { ByteResult = response.Serialize(), IsVoidResult = false };
        }
    }
}
