using CarBookingApp.DTO;
using CarBookingApp.Models;

namespace CarBookingApp.IServices
{
    public interface IUserServiceManagement
    {
            Task<bool> RegisterAdmin(UserManagement user);
            Task<bool> RegisterCustomer(UserManagement user);
            Task<string> LoginUser(string UserName, string Password);
        Task<string> UpdateUser(int userId, RegisterDto updatedUser);
            Task<String> DeleteUser(int userId);
            Task<List<UserManagement>> GetAllUser();
            UserManagement GetUserById(int userId);
        }
    }


