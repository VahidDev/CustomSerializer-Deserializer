using DomainModels.Entities;
using Microsoft.EntityFrameworkCore;
namespace Repository.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Address> Addresses{ get; set; }
        public DbSet<Person> People { get; set; }
    }
}
