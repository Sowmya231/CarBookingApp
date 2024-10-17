using AutoMapper;
using CarBookingApp.DTO;
using CarBookingApp.Models;

namespace CarBookingApp.Profiles
{
    public class MappingProfiles
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {

                CreateMap<Review, ReviewDTO>().ReverseMap();
                CreateMap<Booking, BookingDTO>().ReverseMap();
                CreateMap<Car, CarDTO>().ReverseMap();
            }
        }
    }
}
