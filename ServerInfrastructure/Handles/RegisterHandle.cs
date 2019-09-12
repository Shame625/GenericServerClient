using DatabaseCore;
using DatabaseCore.Models;
using Infrastructure;
using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Register;
using System.Linq;
using System.Threading.Tasks;

namespace ServerInfrastructure.Handles
{
    public static class RegisterHandle
    {
        public async static Task<Result> RegisterChallange(BasePacket bp, Connection c)
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
                    var newUser = new DatabaseCore.Models.User() { UserName = registerPacket.UserName };
                    await context.Users.AddAsync(newUser);
                    await context.SaveChangesAsync();

                    response.status = Enums.RegistrationStatus.Ok;
                    response.Token = "TESTTOKEN";
                }
            }

            return new Result { Packet = response, IsVoidResult = false };
        }
    }
}
