using System;

namespace GenericEntity.MainEntity
{
    interface IEntityBase
    {
        public long Id { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
