using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseCore.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MessageId { get; set; }

        [MaxLength(256)]
        public string Text { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime InsertDate { get; set; }

        public virtual User User { get; set; }
    }
}
