using DatabaseCore;
using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Packets;
using Infrastructure.Packets.Login;
using Microsoft.EntityFrameworkCore;
using ServerInfrastructure;
using ServerInfrastructure.Models;
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
                var userObject = context.Users.Include(o => o.Characters).SingleOrDefault(o => o.UserName == loginPacket.UserName);

                if(userObject != null)
                {
                    response.UserName = loginPacket.UserName;
                    response.status = LoginStatus.Ok;
                    c.User = new ServerUser { UserId = userObject.Id, UserName = userObject.UserName, UserData = userObject };

                    if (userObject.Characters.Count() != 10)
                    {
                        response.Characters = Enumerable.Range(0, 10).Select(n => new Character { CharacterId = 0, Class = Class.Priest, Level = 0, Name = ""}).ToArray();

                        var Characters = userObject.Characters.ToArray();
                        for (var i = 0; i < Characters.Length; i++)
                        {
                            response.Characters[i] = new Character { CharacterId = Characters[i].Id, Name = Characters[i].Name, Class = Characters[i].Class, Level = Characters[i].Level };
                        }
                    }
                    else
                        response.Characters = userObject.Characters.Select(o => new Character { Name = o.Name, Class = o.Class, Level = o.Level }).ToArray();
                }
            }

            return new Models.Result { Packet = response, IsVoidResult = false };
        }
    }
}
