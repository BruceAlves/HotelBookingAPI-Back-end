using HotelBookingAPI.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelPackagesController : ControllerBase
    {
        private readonly TravelPackagesService _travelPackagesService;

        public TravelPackagesController(TravelPackagesService travelPackagesService)
        {
            _travelPackagesService = travelPackagesService;
        }

        [HttpGet]
        public async Task<List<TravelPackagesDto>> GetTravelPackages()
        {
            return await _travelPackagesService.GetAllTravelPackagesAsync();
        }


        [HttpGet("ofertas")]
        public async Task<List<TravelPackagesDto>> GetTravelPackagesOfferAsync()
        {
            return await _travelPackagesService.GetTravelPackagesOfferAsync();
        }
    }
}
