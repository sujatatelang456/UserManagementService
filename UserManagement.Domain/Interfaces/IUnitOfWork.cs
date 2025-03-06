using UserManagement.Domain.Interfaces;

namespace UserManagement.Domain.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
