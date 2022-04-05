using System.ComponentModel.DataAnnotations.Schema;
using DomainModels.Entities.Base;
namespace DomainModels.Entities
{
    public class Person:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public long? AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
