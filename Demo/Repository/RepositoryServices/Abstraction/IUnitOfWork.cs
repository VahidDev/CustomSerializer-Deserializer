using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IUnitOfWork
    {
        IAddressRepository AddressRepository { get; }
        IPersonRepository PersonRepository { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
