using DatabaseCore;
using Infrastructure.Packets;
using Infrastructure.Packets.Login;
using System.Linq;

namespace Infrastructure.Handles
{
    public static class LoginHandle
    {
        public static byte[] LoginChallenge(BasePacket bp, ref Connection c)
        {
            var loginPacket = (CMSG_Login)bp;

            var response = new SMSG_Login() { status = Enums.Enums.LoginStatus.Fail };

            using (var context = new ApplicationContext())
            {
                var userObject = context.Users.FirstOrDefault(o => o.UserName == loginPacket.UserName);
                if(userObject != null)
                {
                    response.UserName = loginPacket.UserName;
                    response.status = Enums.Enums.LoginStatus.Ok;
                    c.User = userObject;
                }
            }

            return response.Serialize();
        }
    }
}
