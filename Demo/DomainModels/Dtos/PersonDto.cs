using DomainModels.Entities;
namespace DomainModels.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? AddressId { get; set; }
        public virtual AddressDto Address { get; set; }
    }
}
