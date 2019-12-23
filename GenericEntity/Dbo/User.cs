using GenericEntity.Enums;
using GenericEntity.MainEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericEntity.Dbo
{
    public class User : EntityBase
    {
        public User()
        {
            Characters = new HashSet<Character>();
            Messages = new List<Message>();
        }

        [MaxLength(32)]
        public string UserName { get; set; }

        public Roles Role { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
