using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.Services
{
    public class UserService // : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        // , IUserService userService
        public UserService(IUnitOfWork unitOfWork) { 
             _unitOfWork = unitOfWork;
        }

        public async Task AddUser(User user)
        {
            await _unitOfWork.Users.AddUser(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            await _unitOfWork.Users.DeleteUser(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.Users.GetAllUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Users.GetUserById(id);
        }

        public async Task UpdateUser(User user)
        {
            await _unitOfWork.Users.UpdateUser(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
