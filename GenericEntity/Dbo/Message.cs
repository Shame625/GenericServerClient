using GenericEntity.MainEntity;
using System.ComponentModel.DataAnnotations;

namespace GenericEntity.Dbo
{
    public class Message : EntityBase
    {
        [MaxLength(256)]
        public string Text { get; set; }
        public virtual User User { get; set; }
    }
}
