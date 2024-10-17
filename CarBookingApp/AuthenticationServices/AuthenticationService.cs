
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    //using Tourism_Management_System_API.DTO;
    //using Tourism_Management_System_API.Models;
    //using Tourism_Management_System_API_Project_.Data;
    using System.Threading.Tasks;
    using CarBookingApp.Models;
    using CarBookingApp.Data;
    using CarBookingApp.DTO;

namespace CarBookingApp.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
        {
            private readonly IConfiguration _configuration;
            private readonly ApplicationDbContext _context;

            public AuthenticationService(IConfiguration configuration, ApplicationDbContext context)
            {
                _configuration = configuration;
                _context = context;
            }

            public async Task<bool> RegisterAsync(RegisterDto model)
            {
                var user = new UserManagement
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    
                    Role = "Customer" // Default role
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<string> LoginAsync(string username, string password)
            {
                try
                {
                    var user = await _context.Users
                                              .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
                    if (user == null)
                    {
                        return "User not found";
                    }

                    var authClaims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        expires: DateTime.Now.AddHours(5),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in LoginAsync: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                    return "An error occurred during login.";
                }
            }

        }
    }


