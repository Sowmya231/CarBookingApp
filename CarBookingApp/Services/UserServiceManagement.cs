using CarBookingApp.Data;
using CarBookingApp.DTO;
using CarBookingApp.ExceptionHandling;
using CarBookingApp.IServices;
using CarBookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Services
{
    public class UserServiceManagement : IUserServiceManagement
    {
        public readonly ApplicationDbContext Context;
        public UserServiceManagement(ApplicationDbContext _context)
        {
            Context = _context;
        }
        public async Task<string> LoginUser(string userName, string password)
        {
            // Retrieve the user by username
            var user = await Context.Users
                .FirstOrDefaultAsync(u => u.Username == userName);

            if (user == null)
            {
                // return "User not found.";
                throw new UserNotFoundException("User not found.");
            }

            // If the user's role is "Admin", return "Id not found"
            //if (user.Role == "Admin")
            //{
            //    return "Admin Cannot be accessed..";
            //}

            // Check if the provided password matches
            if (user.Password == password)
            {
                if (user.Role == "Admin")
                {
                    return "Admin Login Successfully..";
                }
                else
                {
                    return "Customer Login successfully.";

                }
            }
            else
            {
                return "Invalid password.";
            }
        }
        public async Task<string> DeleteUser(int userId)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
            {
                // return "User not found.";
                throw new UserNotFoundException("User not found.");
            }
            if (user.Role == "Admin")
            {
                return "Admin cannot be Deleted.";
            }

            // If not admin, delete the user
            Context.Users.Remove(user);
            await Context.SaveChangesAsync();

            return "User deleted successfully.";
        }

        
        public async Task<List<UserManagement>> GetAllUser()
        {
            return await Context.Users.Where(u => u.Role == "Customer").ToListAsync();
        }

        public UserManagement GetUserById(int userId)
        {
            try
            {
                // Retrieve the user by ID
                var user = Context.Users.FirstOrDefault(u => u.UserID == userId);

                if (user == null)
                {
                    return null; // User not found
                }

                if (user.Role == "Admin")
                {
                    throw new Exception("It's an admin ID."); // Throw an exception for Admin role
                }

                return user; // Return the user if it's not an admin
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); // Handle exceptions gracefully
            }
        }


        public async Task<string> UpdateUser(int userId, RegisterDto updatedUser)
        {
            // Retrieve the user from the database by userId
            var user = await Context.Users.FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
            {
                // return "User not found."; // Return if the user doesn't exist
                throw new UserNotFoundException("User not found.");
            }

            // If the user’s role is "Admin", return "Id not found"
            if (user.Role == "Admin")
            {
                return "Admin Id cannot be modify"; // Admin cannot be updated
            }

            // Update the user's details
            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.PhoneNumber = updatedUser.PhoneNumber;


            // Save the changes to the database
            await Context.SaveChangesAsync();

            return "User updated successfully.";
        }



        public async Task<bool> RegisterAdmin(UserManagement user)
        {
            //user.Role = "Admin";
            var existingAdmin = await Context.Users.FirstOrDefaultAsync(u => u.Role == "Admin");
            if (existingAdmin != null)
                return false;
            user.Role = "Admin";
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegisterCustomer(UserManagement user)
        {
            var existingUser = await Context.Users
            .FirstOrDefaultAsync(u => u.Username == user.Username || u.Email == user.Email);
            if (existingUser != null)
                return false;
            user.Role = "Customer";
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
