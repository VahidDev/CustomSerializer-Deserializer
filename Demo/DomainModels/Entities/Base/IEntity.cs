using System;

namespace DomainModels.Entities.Base
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime StartDate { get; set; }
        DateTime DeletedDate { get; set; }

    }
}
