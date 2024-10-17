
    //using Car_booking_app.DTO;
    using CarBookingApp.DTO;
using CarBookingApp.Models;
using System.Collections.Generic;
namespace CarBookingApp.IServices
{

    public interface ICarManagementService
    {
        List<CarDTO> GetCars();
        CarDTO GetCarById(int id);
        Task<List<CarDTO>> GetCarsByCategory(string category);
        List<CarDTO> SearchCars(string searchBy, string filterValue);
        Task<CarDTO> AddNewCar(CarDTO car);
        Task<bool> UpdateCar(int id, CarDTO car);
        Task<bool> DeleteCar(int id);
    }
}
        



