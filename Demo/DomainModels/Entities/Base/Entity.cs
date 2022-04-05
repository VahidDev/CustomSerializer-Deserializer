using System;

namespace DomainModels.Entities.Base
{
    public class Entity:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime DeletedDate { get; set; }
    }
}
