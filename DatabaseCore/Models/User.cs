using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseCore.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [MaxLength(32)]
        public string UserName { get; set; }

        public ICollection<Message> Messages { get; set; }

        public User()
        {
            Messages = new List<Message>();
        }
    }
}
