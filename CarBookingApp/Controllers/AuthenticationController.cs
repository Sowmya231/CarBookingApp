using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


    using CarBookingApp.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    //using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
//using Training_Auth_Demo.Authentication;

    using CarBookingApp.DTO;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;


namespace CarBookingApp.Controllers
{
    [Route("api/[controller]")]
        [ApiController]
        public class AuthenticationController : ControllerBase
        {
            private readonly CarBookingApp.AuthenticationServices.IAuthenticationService _authService;

            public AuthenticationController(CarBookingApp.AuthenticationServices.IAuthenticationService authService)
            {
                _authService = authService; // Dependency injection
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterDto model)
            {
                if (model == null)
                {
                    return BadRequest("Invalid registration data.");
                }

                var result = await _authService.RegisterAsync(model); // Calling RegisterAsync
                if (result)
                {
                    return Ok(new { Status = "Success", Message = "User registered successfully." });
                }
                return BadRequest(new { Status = "Error", Message = "User registration failed." });
            }

            [HttpPost("login")]
            public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
            {
                if (model == null)
                {
                    return BadRequest("Invalid login data.");
                }

                var token = await _authService.LoginAsync(model.Username, model.Password); // Calling LoginAsync
                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(new { Token = token }); // Return token
                }
                return Unauthorized(new { Status = "Error", Message = "Invalid credentials." }); // Unauthorized if login fails
            }
        }
    }




