using GenericEntity.MainEntity;
using Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace GenericEntity.Dbo
{
    public class Character : EntityBase
    {
        [StringLength(12)]
        public string Name { get; set; }
        public uint Level { get; set; }
        public Class Class { get; set; }

        public float Pos_X { get; set; }
        public float Pos_Y { get; set; }
        public float Pos_Z { get; set; }

        public virtual User User { get; set; }
    }
}
