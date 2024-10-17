
using CarBookingApp.DTO;
using Microsoft.AspNetCore.Mvc;
//using Tourism_Management_System_API.Authentication;
//using Tourism_Management_System_API.DTO;
namespace CarBookingApp.AuthenticationServices
{

    public interface IAuthenticationService
    {
        Task<bool> RegisterAsync(RegisterDto model);
        Task<string> LoginAsync(string username, string password);
    }
}


