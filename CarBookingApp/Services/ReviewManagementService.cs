
//using Car_booking_app.Model;
using CarBookingApp.Data;
using CarBookingApp.DTO;
using CarBookingApp.ExceptionHandling;
using CarBookingApp.IServices;
    using CarBookingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
    using System.Collections.Generic;
    using System.Linq;
using static CarBookingApp.Services.ReviewManagementService;
namespace CarBookingApp.Services
{
    public class ReviewManagementService : IReviewManagementService
    {
        private readonly ApplicationDbContext _context;

        public ReviewManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Add a new review for a car by a user
        public async Task<string> AddReview(ReviewDTO reviewDTO)

        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == reviewDTO.UserID);

            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            var review = new Review
            {
                CarID = reviewDTO.CarID,
                UserID = reviewDTO.UserID,
                Rating = reviewDTO.Rating,
                Comment = reviewDTO.Comment,

            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return "Review added successfully.";
        }



        //Update an existing review
        public async Task<string> UpdateReview(int reviewId, ReviewDTO updatedReview)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewID == reviewId);

            if (review == null)
            {
                throw new ReviewNotFoundException("Review not found.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == updatedReview.UserID);
            if (user == null || (user.UserID != review.UserID && user.Role != "Admin"))
            {
                return "Unauthorized action.";
            }

            review.Rating = updatedReview.Rating;
            review.Comment = updatedReview.Comment;
            

            await _context.SaveChangesAsync();

            return "Review updated successfully.";
        }

        // Delete a review by its ID
        public async Task<string> DeleteReview(int reviewId, int userId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewID == reviewId);
            if (review == null)
            {
                throw new ReviewNotFoundException("Review not found.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null || (user.UserID != review.UserID && user.Role != "Admin"))
            {
                return "Unauthorized action.";
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return "Review deleted successfully.";
        }

        // Get all reviews for a specific car
        public async Task<List<Review>> GetAllReviewsForCar(int carId)
        {
            return await _context.Reviews.Where(r => r.CarID == carId).ToListAsync();
        }

        // Get a review by its ID
        public async Task<Review> GetReviewById(int reviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewID == reviewId);
            if (review == null)
            {
                throw new ReviewNotFoundException("Review not found.");
            }
            return review;
        }
    }




}



