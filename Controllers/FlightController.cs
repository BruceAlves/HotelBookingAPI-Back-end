using HotelBookingAPI.Application;
using HotelBookingAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private FlightSevice _flightSevice;

        public FlightController(FlightSevice flightSevice)
        {
            _flightSevice = flightSevice;
        }

        [HttpGet]
        public async Task<List<FlightDto>> GetFlight()
        {
            return await _flightSevice.GetAllFlightAsync();
        }

        [HttpGet("ofertas")]
        public async Task<List<FlightDto>> GetFlightOfferAsync()
        {
            return await _flightSevice.GetFlightOfferAsync();
        }

    }
}
