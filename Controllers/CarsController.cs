using HotelBookingAPI.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private CarsService _carsService;

        public CarsController(CarsService carsService)
        {
            _carsService = carsService;
        }


        [HttpGet]
        public async Task<List<CarsDto>> GetAccommodation()
        {
            return await _carsService.GetAllCarsAsync();
        }
    }
}
