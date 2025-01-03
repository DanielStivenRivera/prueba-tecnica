using app_server.Adapters;
using app_server.Application.DTOs.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app_server.Controllers;

[ApiController]
[Route("reservations")]
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
    
    [Authorize]
    [HttpPost()]
    public IActionResult CreateReservation([FromBody] CreateReservationRequest createReservationRequest)
    {
        var reservation = _reservationAdapter.CreateReservation(createReservationRequest);
        return Ok(reservation);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    
    public IActionResult DeleteReservation(int id)
    {
        _reservationAdapter.DeleteReservation(id);
        return Ok();
    }
}