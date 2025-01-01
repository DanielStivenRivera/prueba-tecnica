using app_server.Adapters;
using app_server.Application.DTOs.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app_server.Controllers;

[ApiController]
[Route("[Controller]")]
public class Reservations : ControllerBase
{

    private readonly ReservationAdapter _reservationAdapter;
    
    public Reservations(ReservationAdapter reservationAdapter)
    {
        _reservationAdapter = reservationAdapter;
    }
    
    [HttpGet()]
    public IActionResult GetReservations([FromQuery] ReservationParams reservationParams)
    {
        var reservations = _reservationAdapter.GetReservations(reservationParams);
        return Ok(reservations);
    }
    [HttpPost()]
    [Authorize]
    public IActionResult CreateReservation([FromBody] CreateReservationRequest createReservationRequest)
    {
        var reservation = _reservationAdapter.CreateReservation(createReservationRequest);
        return Ok(reservation);
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteReservation(int id)
    {
        _reservationAdapter.DeleteReservation(id);
        return Ok();
    }
}