using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; }
        public UnitOfWork(AppDbContext context, IUserRepository userRepository) { 
            _context = context;
            Users = userRepository;
        }
        public void Dispose() { 
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
