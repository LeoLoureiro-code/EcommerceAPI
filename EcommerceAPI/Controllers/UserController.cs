using EcommerceAPI.Services;
using ECommerceAPI.DataAccess.EF.Models;
using ECommerceAPI.DataAccess.EF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly PasswordService _passwordService;

        public UserController(UserRepository userRepository, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var user = await _userRepository.GetAllUsersAsync();
            return Ok(user);
        }

        //GET:api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //POST: api/Users
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            user.PasswordHash = _passwordService.HashPassword(user.PasswordHash);
            await _userRepository.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        //PUT: api/Users/5

        public async Task<ActionResult> UpdateUser (int id, [FromBody] User user)
        {

            try
            {
                var hashedPassword = _passwordService.HashPassword(user.PasswordHash);
                await _userRepository.UpdateUser(id, user.FirstName, user.LastName, user.Email, user.PasswordHash);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
