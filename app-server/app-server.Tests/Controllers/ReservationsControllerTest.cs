using app_server.Adapters;
using app_server.Application.DTOs.Reservations;
using app_server.Controllers;
using app_server.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace app_server.Tests.Controllers;

public class ReservationsControllerTest
{
    private readonly Mock<ReservationAdapter> _mockReservationAdapter;
    private readonly Reservations _reservationsController;
    
    public ReservationsControllerTest()
    {
        _mockReservationAdapter = new Mock<ReservationAdapter>();
        _reservationsController = new Reservations(_mockReservationAdapter.Object);
    }
    
    [Fact]
    public void GetReservations_ShouldReturnOkResult_WithReservations()
    {
        var reservationParams = new ReservationParams
        {
            startDate = DateTime.Now,
            endDate = DateTime.Now.AddDays(1),
            userId = 1,
            spaceId = 1
        };
        
        var reservations = new List<Reservation>
        {
            new Reservation
            {
                userId = 1,
                spaceId = 1,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddDays(1)
            }
        };
        
        _mockReservationAdapter.Setup(adapter => adapter.GetReservations(reservationParams)).Returns(reservations);
        
        //Act
        
        var result = _reservationsController.GetReservations(reservationParams) as OkObjectResult;
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var response = Assert.IsType<List<Reservation>>(result.Value);
        Assert.Equal(reservations, response);
    }
    
    [Fact]
    public void CreateReservation_ShouldReturnOkResult_WithReservation()
    {
        var createReservationRequest = new CreateReservationRequest
        {
            userId = 1,
            spaceId = 1,
            startDate = DateTime.Now,
            endDate = DateTime.Now.AddDays(1)
        };
        
        var reservation = new Reservation
        {
            userId = 1,
            spaceId = 1,
            startDate = DateTime.Now,
            endDate = DateTime.Now.AddDays(1)
        };
        
        _mockReservationAdapter.Setup(adapter => adapter.CreateReservation(createReservationRequest)).Returns(reservation);
        
        //Act
        
        var result = _reservationsController.CreateReservation(createReservationRequest) as OkObjectResult;
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var response = Assert.IsType<Reservation>(result.Value);
        Assert.Equal(reservation, response);
    }
    
    [Fact]
    public void DeleteReservation_ShouldReturnOkResult_WhenSuccessFul()
    {
        int id = 1;
        
        //Act
        
        var result = _reservationsController.DeleteReservation(id) as OkResult;
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
    
}