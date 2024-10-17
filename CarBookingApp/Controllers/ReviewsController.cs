using CarBookingApp.DTO;
using CarBookingApp.IServices;
using CarBookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewManagementService _reviewService;

    public ReviewController(IReviewManagementService reviewService)
    {
        _reviewService = reviewService;
    }

    // Add a new review
    [HttpPost("add")]
   // [Authorize(Roles ="Customer")]
    public async Task<IActionResult> AddReview([FromBody] ReviewDTO reviewDTO)
    {
        var result = await _reviewService.AddReview(reviewDTO);
        return Ok(result);
    }

    // Update an existing review
    [HttpPut("update/{reviewId}")]
    //[Authorize(Roles ="Customer")]
    public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewDTO updatedReview)
    {
        var result = await _reviewService.UpdateReview(reviewId, updatedReview);
        return Ok(result);
    }

    // Delete a review
    [HttpDelete("delete/{reviewId}")]
    //[Authorize(Roles ="Admin")]
    public async Task<IActionResult> DeleteReview(int reviewId, [FromQuery] int userId)
    {
        var result = await _reviewService.DeleteReview(reviewId, userId);
        return Ok(result);
    }

    // Get all reviews for a specific car
    [HttpGet("car/{carId}")]
    public async Task<IActionResult> GetAllReviewsForCar(int carId)
    {
        var reviews = await _reviewService.GetAllReviewsForCar(carId);
        return Ok(reviews);
    }

    // Get a review by its ID
    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetReviewById(int reviewId)
    {
        var review = await _reviewService.GetReviewById(reviewId);
        return Ok(review);
    }
}



