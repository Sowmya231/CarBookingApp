using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using AutoMapper;
    using CarBookingApp.DTO;
    using CarBookingApp.IServices;
    using CarBookingApp.Models;
    using Microsoft.AspNetCore.Http;
using CarBookingApp.ExceptionHandling;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.DTO;
//using WebApplication1.IBookServices;
//using WebApplication1.Model;
using CarBookingApp.IServices;

namespace CarBookingApp.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookService _bookingService;
        private readonly IMapper _mapper;

        public BookingsController(IBookService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        // POST /api/bookings - Book a car
        [HttpPost]
        public async Task<ActionResult<Booking>> BookCar([FromBody] BookingDTO bookingDto)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid data provided. Please check the input and try again.");
            }

            var createdBooking = await _bookingService.CreateBookingAsync(bookingDto);
            return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.BookingID }, createdBooking);
        }

        // PUT /api/bookings/{id}/update - Update a booking
        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDTO bookingDto)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid booking data provided. Please verify and try again.");
            }

            var updatedBooking = await _bookingService.UpdateBookingAsync(id, bookingDto);
            if (updatedBooking == null)
            {
                throw new Exception($"Booking with ID {id} not found.");
            }

            return Ok(new { message = "Booking updated successfully." });
        }

        // DELETE /api/bookings/{id} - Cancel a booking
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            if (!result)
            {
                throw new Exception($"Booking with ID {id} does not exist.");
            }

            return Ok(new { message = "Booking cancelled successfully." });
        }

        // GET /api/bookings - View all bookings
        [HttpGet]
        public async Task<ActionResult<List<Booking>>> GetBookings()
        {
            var bookings = await _bookingService.GetBookingsAsync();
            if (bookings == null || bookings.Count == 0)
            {
                throw new Exception("No bookings found.");
            }

            return Ok(bookings);
        }

        // GET /api/bookings/{id} - View details of a specific booking
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                throw new Exception($"Booking with ID {id} not found.");
            }

            return Ok(booking);
        }

        // GET /api/bookings/user/{userId} - View bookings made by a specific user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Booking>>> GetBookingsByUser(int userId)
        {
            var bookings = await _bookingService.GetBookingsByUserAsync(userId);
            if (bookings == null || bookings.Count == 0)
            {
                throw new Exception($"No bookings found for user with ID {userId}.");
            }

            return Ok(bookings);
        }

        // GET /api/bookings/car/{carId} - View bookings for a specific car
        [HttpGet("car/{carId}")]
        public async Task<ActionResult<List<Booking>>> GetBookingsByCar(int carId)
        {
            var bookings = await _bookingService.GetBookingsByCarAsync(carId);
            if (bookings == null || bookings.Count == 0)
            {
                throw new Exception($"No bookings found for car with ID {carId}.");
            }

            return Ok(bookings);
        }

        // GET /api/bookings/date/{date} - View bookings by date
        [HttpGet("date/{date}")]
        public async Task<ActionResult<List<Booking>>> GetBookingsByDate(DateTime date)
        {
            var bookings = await _bookingService.GetBookingsByDateAsync(date);
            if (bookings == null || bookings.Count == 0)
            {
                throw new Exception($"No bookings found on {date:yyyy-MM-dd}.");
            }

            return Ok(bookings);
        }
    }
}

