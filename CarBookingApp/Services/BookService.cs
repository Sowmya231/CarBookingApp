
using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.DTO;
using CarBookingApp.ExceptionHandling;
using CarBookingApp.IServices;
using CarBookingApp.Models;
using Microsoft.EntityFrameworkCore;
//using WebApplication1.Data;
//using WebApplication1.DTO;
//using WebApplication1.IBookServices;
//using WebApplication1.Model;

namespace CarBookingApp.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; // Inject AutoMapper
        }

        /// <summary>
        /// Creates a new booking based on the provided BookingDTO.
        /// </summary>
        public async Task<Booking> CreateBookingAsync(BookingDTO bookingDto)
        {
            // Ensure the booking date is valid (future or current)
            if (bookingDto.BookingDate.Date < DateTime.Now.Date)
            {
                throw new Exception("Booking date must be today or a future date.");
            }
            // Use AutoMapper to map BookingDTO to Booking model
            var booking = _mapper.Map<Booking>(bookingDto);

            booking.Distance = CalculateDistance(booking.Source, booking.Destination);
            booking.Price = CalculatePrice(booking.Distance, 10.0M); // Assuming 10.0 as price per km

            // Add booking to the database0
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        /// <summary>
        /// Updates an existing booking identified by the given ID.
        /// </summary>
        public async Task<Booking> UpdateBookingAsync(int id, BookingDTO bookingDto)
        {
            var existingBooking = await _context.Bookings.FindAsync(id);
            if (existingBooking == null)
            {
                return null; // Booking not found
            }

            // Map updated properties from DTO to the existing booking
            _mapper.Map(bookingDto, existingBooking);

            _context.Entry(existingBooking).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingBooking;
        }

        /// <summary>
        /// Deletes a booking identified by the given ID.
        /// </summary>
        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return false; // Booking not found
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retrieves all bookings.
        /// </summary>
        public async Task<List<Booking>> GetBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific booking identified by the given ID.
        /// </summary>
        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all bookings made by a specific user.
        /// </summary>
        public async Task<List<Booking>> GetBookingsByUserAsync(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserID == userId)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all bookings for a specific car.
        /// </summary>
        public async Task<List<Booking>> GetBookingsByCarAsync(int carId)
        {
            return await _context.Bookings
                .Where(b => b.CarID == carId)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all bookings made on a specific date.
        /// </summary>
        public async Task<List<Booking>> GetBookingsByDateAsync(DateTime date)
        {
            return await _context.Bookings
                .Where(b => b.BookingDate.Date == date.Date)
                .ToListAsync();
        }

        // Price calculation method
        public decimal CalculatePrice(double distance, decimal carPricePerKm)
        {
            return (decimal)distance * carPricePerKm;
        }

        // Dummy method for calculating distance between source and destination
        private double CalculateDistance(string source, string destination)
        {
            return new Random().Next(10, 100); // distance in between 10 to 100

        }
    }
}



