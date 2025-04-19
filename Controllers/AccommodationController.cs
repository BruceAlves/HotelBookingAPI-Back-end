using AutoMapper;
using HotelBookingAPI.Application;
using HotelBookingAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Presentation.Controllers
{
    [Route("api/Accommodation")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private AccommodationService _accommodationServices;


        public AccommodationController(AccommodationService hospedagenServices)
        {
            _accommodationServices = hospedagenServices;
        }

        [HttpGet]
        public async Task<List<AccommodationDto>> GetAccommodation()
        {
            return await _accommodationServices.GetAllAccommodationAsync();
        }

        [HttpGet("ofertas")]
        public async Task<List<AccommodationDto>> GetFlightOfferAsync()
        {
            return await _accommodationServices.GetAccommodationOfferAsync();
        }
    }
}
