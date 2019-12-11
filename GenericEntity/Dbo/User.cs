using GenericEntity.MainEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericEntity.Dbo
{
    public class User : EntityBase
    {
        [MaxLength(32)]
        public string UserName { get; set; }

        public ICollection<Message> Messages { get; set; }

        public User()
        {
            Messages = new List<Message>();
        }
    }
}
