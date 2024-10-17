using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Car_booking_app.CarManagementService;
//using Car_booking_app.DTO;
using CarBookingApp.DTO;
using CarBookingApp.IServices;
//using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace CarBookingApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CarManagementController : ControllerBase
    {
        private readonly ICarManagementService _service;

        public CarManagementController(ICarManagementService service)
        {
            _service = service;
        }


        // GET /api/cars/{id} - Get details of a specific car.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = _service.GetCarById(id)
;
            if (car != null)
            {
                return Ok(car);
            }
            else
            {
                return Ok();
            }
        }
        // POST /api/cars - Add a new car
        [HttpPost]
        //[Authorize(Roles ="Admin")]
        
        public async Task<IActionResult> AddNewCar([FromBody] CarDTO carDto)
        {
            // Check if the carDto is null
            if (carDto == null)
            {
                return BadRequest("Car data is null.");
            }

            // ModelState checks for any validation issues based on data annotations in CarDTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Call the service method to add a new car (make sure the service method returns Car object)
                var newCar = await _service.AddNewCar(carDto); // Ensure AddNewCarAsync is async

                if (newCar == null)
                {
                    // If adding a car fails and returns null, respond with appropriate error
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error adding the car.");
                }

                // Return a 201 Created status code along with the newly added car data and URI
                return CreatedAtAction(nameof(GetCarById), new { id = newCar.CarID }, newCar);
            }
            catch (DbUpdateException dbEx) // Catch specific database-related exceptions
            {
                // Handle database-related exceptions like foreign key or unique constraint violations
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Catch any other general exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving the car: {ex.Message}");
            }
        }


        // PUT /api/cars/{id} - Update a car.
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCar(int id, CarDTO updatedCar)
        {
            if (updatedCar != null)
            {
                _service.UpdateCar(id, updatedCar);
                return Ok(updatedCar);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE /api/cars/{id} - Delete a car.
        [HttpDelete("{id}")]
         //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                if (id != 0)
                {
                    _service.DeleteCar(id);
                    ;
                    return Ok("Car removed");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        // GET /api/cars - List all cars.
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            return Ok(_service.GetCars());
        }

        // GET /api/cars/category/{category} - List cars by category.

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetCarsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Category must be provided.");
            }

            try
            {
                var carsByCategory = await _service.GetCarsByCategory(category);

                // Ensure carsByCategory is of type List<CarDTO>
                if (carsByCategory != null && carsByCategory.Count > 0)
                {
                    return Ok(carsByCategory);
                }
                else
                {
                    return NotFound("No cars found for the specified category.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }
        // GET /api/cars/search - Search cars based on various filters.

        [HttpGet("search")]
        public ActionResult<List<CarDTO>> SearchCars(string searchBy, string filterValue)
        {
            if (string.IsNullOrWhiteSpace(searchBy) || string.IsNullOrWhiteSpace(filterValue))
            {
                return BadRequest("Search criteria cannot be empty.");
            }

            try
            {
                var results = _service.SearchCars(searchBy, filterValue);
                if (results.Count == 0)
                {
                    return NotFound("No cars found matching the criteria.");
                }

                return Ok(results);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

