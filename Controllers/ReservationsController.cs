using HotelBookingAPI.Application;
using HotelBookingAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Presentation.Controllers;


[Route("api/reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _reservationService;

    public ReservationsController(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<List<Reservation>> GetReservations()
    {
        return await _reservationService.GetAllReservationsAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] Reservation reservation)
    {
        await _reservationService.AddReservationAsync(reservation);
        return Created("", reservation);
    }
}