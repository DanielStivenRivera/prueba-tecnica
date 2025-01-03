using app_server.Adapters;
using app_server.Application.DTOs.Reservations;
using app_server.Application.services;
using app_server.Domain.Entities;
using Moq;
using Xunit;

namespace app_server.Tests.Adapters
{
    public class ReservationAdapterTests
    {
        private readonly Mock<ReservationService> _mockReservationService;
        private readonly ReservationAdapter _reservationAdapter;

        public ReservationAdapterTests()
        {
            _mockReservationService = new Mock<ReservationService>();
            _reservationAdapter = new ReservationAdapter(_mockReservationService.Object);
        }

        [Fact]
        public void CreateReservation_ShouldCallCreateReservationMethod()
        {
            // Arrange
            var createReservationRequest = new CreateReservationRequest
            {
                userId = 1,
                spaceId = 2,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddHours(2)
            };

            var mockReservation = new Reservation
            {
                userId = createReservationRequest.userId,
                spaceId = createReservationRequest.spaceId,
                startDate = createReservationRequest.startDate,
                endDate = createReservationRequest.endDate
            };

            _mockReservationService.Setup(service => service.CreateReservation(It.IsAny<Reservation>())).Returns(mockReservation);

            // Act
            var result = _reservationAdapter.CreateReservation(createReservationRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockReservation.userId, result.userId);
            Assert.Equal(mockReservation.spaceId, result.spaceId);
            Assert.Equal(mockReservation.startDate, result.startDate);
            Assert.Equal(mockReservation.endDate, result.endDate);
            _mockReservationService.Verify(service => service.CreateReservation(It.IsAny<Reservation>()), Times.Once);
        }
        
        [Fact]
        public void DeleteReservation_ShouldCallDeleteReservationMethod()
        {
            // Arrange
            var reservationId = 1;
    
            // Act
            _reservationAdapter.DeleteReservation(reservationId);

            // Assert
            _mockReservationService.Verify(service => service.DeleteReservation(reservationId), Times.Once);
        }
        
        [Fact]
        public void GetReservations_ShouldReturnReservations()
        {
            // Arrange
            var reservationParams = new ReservationParams
            {
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddDays(1),
                userId = 1,
                spaceId = 2
            };

            var mockReservations = new List<Reservation>
            {
                new Reservation { userId = 1, spaceId = 2, startDate = DateTime.Now, endDate = DateTime.Now.AddHours(2) },
                new Reservation { userId = 1, spaceId = 2, startDate = DateTime.Now.AddHours(3), endDate = DateTime.Now.AddHours(5) }
            };

            _mockReservationService.Setup(service => service.GetReservations(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int?>(), It.IsAny<int?>(), false))
                .Returns(mockReservations);

            // Act
            var result = _reservationAdapter.GetReservations(reservationParams);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockReservations.Count, result.Count());
            _mockReservationService.Verify(service => service.GetReservations(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int?>(), It.IsAny<int?>(), false), Times.Once);
        }

        
    }
}
