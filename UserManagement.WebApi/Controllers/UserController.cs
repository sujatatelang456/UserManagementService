using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;

namespace UserManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserService _userService;
        public UserController(UserService userService) { 
            _userService = userService;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id}, user);
        }

        [HttpPut]
        public async Task UpdateUser(User User)
        {
            await _userService.UpdateUser(User);
        }

        [HttpDelete("{id}")]
        public async Task DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
        }

        //[HttpGet]
        //public async Task<User> GetUserDetails(string userName)
        //{
        //    return null;
        //}
    }
}
