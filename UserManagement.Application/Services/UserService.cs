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

        public async Task<dynamic> GetUserDetails(string username)
        {
            var users = await _unitOfWork.Users.GetAllUsers();

            var user = users.Where(u => u.Email.ToLower() == username.ToLower()).FirstOrDefault() ?? new User();

            var tasks = await _unitOfWork.Users.GetAllCamundaTasks();
            var taskFieldMappings = await _unitOfWork.Users.GetUserTaskFieldMappings();

            var mapping = taskFieldMappings.Where(tfm => tfm.UserId == user.Id).Select(x => new { x.TaskId, x.AccessTaskFields});

            var result = mapping.Join(tasks,
                           s => s.TaskId,
                           g => g.TaskId,
                           (s, g) => new
                           {
                               s.AccessTaskFields,
                               g.TaskName
                           });


            var userDetails = new { firstName = user.FirstName,
                                    lastName = user.LastName,
                                    username = user.Email,
                                    role = user.Role,
                                    accessControls = result,
                                  };

            return userDetails;

        }
    }
}
