using CarBookingApp.DTO;
using CarBookingApp.IServices;
using CarBookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserServiceManagement _userService;

        public UsersController(IUserServiceManagement userService)
        {
            _userService = userService;
        }
        [HttpPost("register Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto admin)
        {
            var user = new UserManagement
            {
                Username = admin.Username,
                Password = admin.Password,
                Email = admin.Email,
                PhoneNumber = admin.PhoneNumber
            };
            var success = await _userService.RegisterAdmin(user);

            if (!success)
            {
                return Ok("Admin already exists."); // Return conflict if an admin already exists
            }

            return Ok("Admin registered successfully.");
        }
        [HttpPost("registerCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterDto customer)
        {
            var user = new UserManagement
            {
                Username = customer.Username,
                Password = customer.Password,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
            var success = await _userService.RegisterCustomer(user);
            if (!success) return Ok("Customer already exists.");

            return Ok("Customer registered successfully.");
        }
        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<UserManagement> item = await _userService.GetAllUser();
            if (item == null)
            {
                return NotFound("No Customer Found");
            }
            else
            {
                return Ok(item);
            }

        }
        //// GET: api/Users/5
       // [Authorize(Roles ="Customer")]
        [HttpGet("{id}")]
      
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById;

                if (user == null)
                {
                    return NotFound("User not found.");
                  
                }

                return Ok(user); // Return user details if found and not Admin
            }
            catch (Exception ex)
            {
                if (ex.Message == "It's an admin ID.")
                {
                    return Ok("It's an admin ID."); // Return 400 BadRequest if Admin ID
                }

                return StatusCode(500, "An error occurred: " + ex.Message); // Handle other errors
            }
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string result = await _userService.LoginUser(loginDto.Username, loginDto.Password);

            if (result == "Login successful.")
            {
                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpDelete("{id}")]
       // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            string result = await _userService.DeleteUser(id);
            try
            {

                if (result == "Admin cannot be Deleted.")
                {
                    return Ok(result);
                }
                else if (result == "User not found.")
                {
                    return Ok(result); // 404 if user is not found
                }
                else
                {
                    return Ok(result); // 200 OK if user is successfully deleted
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        [HttpPut("{id}")]
      //  [Authorize(Roles ="Customer")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] RegisterDto updateduser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request if the input is invalid
            }

            string result = await _userService.UpdateUser(id, updateduser);

            if (result == "Admin Id cannot be modify")
            {
                return Ok(result); // 404 Not Found if the user is an admin
            }
            else if (result == "User not found.")
            {
                return Ok(result); // 404 Not Found if the user does not exist
            }

            return Ok(result); // 200 OK if the user is successfully updated
        }


    }
}


