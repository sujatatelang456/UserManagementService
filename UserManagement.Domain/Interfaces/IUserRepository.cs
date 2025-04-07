using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Interfaces
{
    public interface IUserRepository
    {        
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task AddUser(User User);
        Task UpdateUser(User User);
        Task DeleteUser(int id);

        Task<List<ManualTask>> GetAllCamundaTasks();
        Task<List<TaskFields>> GetAllCamundaTaskFields();

        Task<List<UserTaskFieldMapping>> GetUserTaskFieldMappings();


    }
}
