using HotelReservationSystem.HotelReservation.Models.Constants;
using HotelReservationSystem.HotelReservation.Models.Dtos;
using HotelReservationSystem.HotelReservation.Models.Entities;
using HotelReservationSystem.HotelReservation.Models.Enums;
using HotelReservationSystem.HotelReservation.Services;
using Microsoft.AspNetCore.Http;

namespace ServicesTests
{
    public class ReservationServiceTests
    {
        private ReservationService reservationService;
        private List<Room> mockAllRoom;

        [SetUp]
        public void Setup()
        {
            reservationService = new ReservationService();
            mockAllRoom = AllRooms.GetAll();
        }

        [Test]
        public void NumberOfGuestsGreaterThanAvailableRooms_NoOption()
        {
            var request = new ReservationRequest()
            {
                NumberOfGuests = 15
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "No Option"
            };

            var result = reservationService.MakeReservation(request);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void NumberOfGuestsLessThanZero_NoOption()
        {
            var request = new ReservationRequest()
            {
                NumberOfGuests = -1
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "No Option"
            };

            var result = reservationService.MakeReservation(request);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void NumberOfGuestsEqualsZero_NoOption()
        {
            var request = new ReservationRequest()
            {
                NumberOfGuests = 0
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "No Option"
            };

            var result = reservationService.MakeReservation(request);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithNoGuests_BadRequest()
        {
            var request = new ReservationRequest() { };

            Assert.Throws<BadHttpRequestException>(() => reservationService.MakeReservation(request));
        }

        [Test]
        public void MakeReservationWithNull_BadRequest()
        {
            Assert.Throws<BadHttpRequestException>(() => reservationService.MakeReservation(null));
        }

        [Test]
        public void MakeReservationWithOneGuest()
        {
            var request = new ReservationRequest()
            {
                NumberOfGuests = 1
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Single - $30"
            };

            var result = reservationService.MakeReservation(request);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithTwoGuests()
        {
            var request = new ReservationRequest()
            {
                NumberOfGuests = 2
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Double - $50"
            };

            var result = reservationService.MakeReservation(request);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithThreeGuests()
        {
            var request = new ReservationRequest()
            {
                NumberOfGuests = 3
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Single Double - $80"
            };

            var result = reservationService.MakeReservation(request);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithSixGuests()
        {
            var request = new ReservationRequest()
            {
                NumberOfGuests = 6
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Double Family - $135"
            };

            var result = reservationService.MakeReservation(request);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithTwoGuests_WithChangedSingleRoomPrice()
        {
            //Change Single Room Price
            mockAllRoom.First(room => room.Type.Equals(RoomType.Single)).Price = 10;

            var request = new ReservationRequest()
            {
                NumberOfGuests = 2
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Single Single - $20"
            };

            var result = reservationService.MakeReservation(request, mockAllRoom);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithFourGuests_WithChangedSingleRoomPrice()
        {
            //Change Single Room Price
            mockAllRoom.First(room => room.Type.Equals(RoomType.Single)).Price = 10;

            var request = new ReservationRequest()
            {
                NumberOfGuests = 4
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Single Single Double - $70"
            };

            var result = reservationService.MakeReservation(request, mockAllRoom);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithTwoGuests_WithChangedDoubleRoomPrice()
        {
            //Change Double Room Price
            mockAllRoom.First(room => room.Type.Equals(RoomType.Double)).Price = 70;

            var request = new ReservationRequest()
            {
                NumberOfGuests = 2
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Single Single - $60"
            };

            var result = reservationService.MakeReservation(request, mockAllRoom);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithSixGuests_WithChangedFamilyRoomSleeps()
        {
            //Change Family Room Sleeps
            mockAllRoom.First(room => room.Type.Equals(RoomType.Family)).Sleeps = 6;

            var request = new ReservationRequest()
            {
                NumberOfGuests = 6
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Family - $85"
            };

            var result = reservationService.MakeReservation(request, mockAllRoom);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithEightGuests_WithChangedFamilyRoomAvailability()
        {
            //Change Family Room Availability
            mockAllRoom.First(room => room.Type.Equals(RoomType.Family)).AvailableCount = 2;

            var request = new ReservationRequest()
            {
                NumberOfGuests = 8
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Family Family - $170"
            };

            var result = reservationService.MakeReservation(request, mockAllRoom);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }

        [Test]
        public void MakeReservationWithEightGuests_WithChangedSingleRoomAvailabilityAndPrice()
        {
            //Change Single Room Availability And Price
            var singleRoom = mockAllRoom.First(room => room.Type.Equals(RoomType.Single));
            singleRoom.AvailableCount = 8;
            singleRoom.Price = 10;

            var request = new ReservationRequest()
            {
                NumberOfGuests = 8
            };

            var expectedResult = new ReservationResponse()
            {
                Result = "Single Single Single Single Single Single Single Single - $80"
            };

            var result = reservationService.MakeReservation(request, mockAllRoom);
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }
    }
}
