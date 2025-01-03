using app_server.Application.services;
using app_server.Domain.Entities;
using app_server.Infrastructure.Persistence.Repositories;
using Moq;

namespace app_server.Tests.Application.Services
{
    
    public class ReservationServiceTests
    {
        private readonly Mock<ReservationRepository> _mockReservationRepository;
        private readonly ReservationService _reservationService;

        public ReservationServiceTests()
        {
            _mockReservationRepository = new Mock<ReservationRepository>();
            _reservationService = new ReservationService(_mockReservationRepository.Object);
        }

        [Fact]
        public void CreateReservation_ShouldThrowException_WhenDurationIsInvalid()
        {
            // Arrange
            var reservation = new Reservation
            {
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddHours(1), // Duration less than 2 hours
                userId = 1,
                spaceId = 1
            };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _reservationService.CreateReservation(reservation));
            Assert.Equal("Reservation duration must be between 2 and 5 hours", exception.Message);
        }

        [Fact]
        public void CreateReservation_ShouldThrowException_WhenUserHasExistingReservation()
        {
            // Arrange
            var reservation = new Reservation
            {
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddHours(3),
                userId = 1,
                spaceId = 1
            };

            _mockReservationRepository.Setup(repo => repo.Search(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), reservation.userId, null, null))
                .Returns(new List<Reservation> { new Reservation() });

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _reservationService.CreateReservation(reservation));
            Assert.Equal("User cant have more than one reservation at the same time", exception.Message);
        }

        [Fact]
        public void CreateReservation_ShouldThrowException_WhenSpaceIsAlreadyReserved()
        {
            // Arrange
            var reservation = new Reservation
            {
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddHours(3),
                userId = 1,
                spaceId = 1
            };

            _mockReservationRepository.Setup(repo => repo.Search(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), null, reservation.spaceId, null))
                .Returns(new List<Reservation> { new Reservation() });

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _reservationService.CreateReservation(reservation));
            Assert.Equal("Space already reserved for this time", exception.Message);
        }

        [Fact]
        public void CreateReservation_ShouldSaveReservation_WhenValid()
        {
            // Arrange
            var reservation = new Reservation
            {
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddHours(3),
                userId = 1,
                spaceId = 1
            };

            _mockReservationRepository.Setup(repo => repo.Search(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), reservation.userId, null, null))
                .Returns(Enumerable.Empty<Reservation>());
            _mockReservationRepository.Setup(repo => repo.Search(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), null, reservation.spaceId, null))
                .Returns(Enumerable.Empty<Reservation>());
            _mockReservationRepository.Setup(repo => repo.Save(It.IsAny<Reservation>())).Returns(reservation);

            // Act
            var result = _reservationService.CreateReservation(reservation);

            // Assert
            Assert.NotNull(result);
            _mockReservationRepository.Verify(repo => repo.Save(It.IsAny<Reservation>()), Times.Once);
        }
        
        [Fact]
        public void DeleteReservation_ShouldThrowException_WhenReservationNotFound()
        {
            // Arrange
            var reservationId = 1;
            _mockReservationRepository.Setup(repo => repo.GetById(reservationId)).Returns((Reservation)null);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _reservationService.DeleteReservation(reservationId));
            Assert.Equal("Reservation not found", exception.Message);
        }

        [Fact]
        public void DeleteReservation_ShouldCallDeleteMethod_WhenReservationExists()
        {
            // Arrange
            var reservationId = 1;
            var reservation = new Reservation { id = reservationId };
            _mockReservationRepository.Setup(repo => repo.GetById(reservationId)).Returns(reservation);

            // Act
            _reservationService.DeleteReservation(reservationId);

            // Assert
            _mockReservationRepository.Verify(repo => repo.Delete(reservationId), Times.Once);
        }
        
        [Fact]
        public void GetReservations_ShouldReturnReservations_WhenValidParams()
        {
            // Arrange
            var reservationParams = new { startDate = DateTime.Now, endDate = DateTime.Now.AddDays(1), userId = 1, spaceId = 2 };
            var mockReservations = new List<Reservation>
            {
                new Reservation { userId = 1, spaceId = 2, startDate = DateTime.Now, endDate = DateTime.Now.AddHours(2) },
                new Reservation { userId = 1, spaceId = 2, startDate = DateTime.Now.AddHours(3), endDate = DateTime.Now.AddHours(5) }
            };

            _mockReservationRepository.Setup(repo => repo.Search(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int?>(), It.IsAny<int?>(), false))
                .Returns(mockReservations);

            // Act
            var result = _reservationService.GetReservations(reservationParams.startDate, reservationParams.endDate, reservationParams.userId, reservationParams.spaceId, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockReservations.Count, result.Count());
            _mockReservationRepository.Verify(repo => repo.Search(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int?>(), It.IsAny<int?>(), null), Times.Once);
        }

    }
}
