//using Car_booking_app.CarManagementService;
//using Car_booking_app.DTO;
//using Car_booking_app.Model;
using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.DTO;
using CarBookingApp.ExceptionHandling;
using CarBookingApp.IServices;
using CarBookingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
    using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace CarBookingApp.Services
{

    public class CarManagementService : ICarManagementService

    {
        private readonly ApplicationDbContext _context; // Assuming your DbContext is named CarDbContext
        private readonly IMapper _mapper; // AutoMapper for mapping DTOs to entities

        public CarManagementService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CarDTO> GetCars()
        {
            return _context.Cars.Select(c => new CarDTO
            {
                CarID = c.CarID,
                CarName = c.CarName,
                Description = c.Description,
                Price = c.Price,
                Category = c.Category,

                StockQuantity = c.StockQuantity,
                ImageURL = c.ImageURL,
                Rating = c.Rating
            }).ToList();
        }

        // GET A SPECIFIC CAR BY ID
        public CarDTO GetCarById(int id)
        {
            var car = _context.Cars.Find(id);
            if (car == null) return null;

            return new CarDTO
            {
                CarID = car.CarID,
                CarName = car.CarName,
                Description = car.Description,
                Price = car.Price,
                Category = car.Category,

                StockQuantity = car.StockQuantity,
                ImageURL = car.ImageURL,
                Rating = car.Rating
            };
        }

        public async Task<List<CarDTO>> GetCarsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return new List<CarDTO>(); // Return empty list if category is not provided.
            }

            return await _context.Cars
                .Where(c => c.Category != null && c.Category.ToLower() == category.ToLower()) // Handle null category values and ensure case-insensitivity.
                .Select(c => new CarDTO
                {
                    CarID = c.CarID,
                    CarName = c.CarName,
                    Description = c.Description,
                    Price = c.Price,
                    Category = c.Category,
                    StockQuantity = c.StockQuantity,
                    ImageURL = c.ImageURL,
                    Rating = c.Rating
                })
                .ToListAsync();
        }


        //public async Task<List<CarDTO>> GetCarsByBrandAsync(string brand)
        //{
        //    return await _context.Cars
        //        .Where(c => c.Brand.ToLowerInvariant() == brand.ToLowerInvariant())
        //        .Select(c => _mapper.Map<CarDTO>(c))
        //        .ToListAsync();
        //}

        public List<CarDTO> SearchCars(string searchBy, string filterValue)
        {
            IQueryable<Car> query = _context.Cars;

            switch (searchBy.ToLower())
            {
                case "category":
                    query = query.Where(c => c.Category.ToLower() == filterValue.ToLower());
                    break;
                case "carname":
                    query = query.Where(c => c.CarName.ToLower().Contains(filterValue.ToLower()));
                    break;
                case "description":
                    query = query.Where(c => c.Description.ToLower().Contains(filterValue.ToLower()));
                    break;
                case "price":
                    if (decimal.TryParse(filterValue, out decimal price))
                    {
                        query = query.Where(c => c.Price == price);
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid search criteria");
            }

            return query.Select(c => new CarDTO
            {
                CarID = c.CarID,
                CarName = c.CarName,
                Description = c.Description,
                Price = c.Price,
                Category = c.Category,
                StockQuantity = c.StockQuantity,
                ImageURL = c.ImageURL,
                Rating = c.Rating
            }).ToList();
        }


        public async Task<CarDTO> AddNewCar(CarDTO carDto)
        {
            try
            {
                var car = _mapper.Map<Car>(carDto);
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return _mapper.Map<CarDTO>(car);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<bool> UpdateCar(int id, CarDTO carDto)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return false;

            _mapper.Map(carDto, car);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return false;

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
