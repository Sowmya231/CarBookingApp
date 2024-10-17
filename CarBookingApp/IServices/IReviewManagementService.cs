using CarBookingApp.DTO;
using CarBookingApp.Models;

namespace CarBookingApp.IServices
{

    public interface IReviewManagementService
    {


        Task<string> AddReview(ReviewDTO reviewDto);
        Task<string> UpdateReview(int reviewId, ReviewDTO updatedReview);
        Task<string> DeleteReview(int reviewId, int userId);
        Task<List<Review>> GetAllReviewsForCar(int carId);
        Task<Review> GetReviewById(int reviewId);
    }



}








