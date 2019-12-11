using DatabaseCore;
using GenericEntity.Dbo;
using Infrastructure.Enums;
using Infrastructure.Packets;
using Infrastructure.Packets.Login;
using ServerInfrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Handles
{
    public static class LoginHandle
    {
        public async static Task<Models.Result> LoginChallenge(BasePacket bp, Connection c)
        {
            var loginPacket = (CMSG_Login)bp;

            var response = new SMSG_Login() { status = LoginStatus.Fail };

            using (var context = new ApplicationContext())
            {
                var userObject = context.Users.SingleOrDefault(o => o.UserName == loginPacket.UserName);

                if(userObject != null)
                {
                    response.UserName = loginPacket.UserName;
                    response.status = LoginStatus.Ok;
                    c.User = new Models.User { UserId = userObject.Id, UserName = userObject.UserName };
                }
            }

            return new Models.Result { Packet = response, IsVoidResult = false };
        }
    }
}
