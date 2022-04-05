using System.Collections.Generic;
using DomainModels.Entities.Base;
namespace DomainModels.Entities
{
    public class Address:Entity
    {
        public string City { get; set; }
        public string AddressLine { get; set; }
        public List<Person> People { get; set; }
    }
}
