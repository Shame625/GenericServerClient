using GenericEntity.Dbo;

namespace ServerInfrastructure.Models
{
    public class ServerUser : Infrastructure.Models.User
    {
        public User UserData { get; set; }
    }
}
