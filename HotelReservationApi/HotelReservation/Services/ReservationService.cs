using HotelReservationApi.HotelReservation.Models.Entities;
using HotelReservationSystem.HotelReservation.Models.Constants;
using HotelReservationSystem.HotelReservation.Models.Dtos;
using HotelReservationSystem.HotelReservation.Models.Entities;
using HotelReservationSystem.HotelReservation.Services.Abstract;

namespace HotelReservationSystem.HotelReservation.Services
{
    public class ReservationService : IReservationService
    {
        public ReservationService() { }

        public ReservationResponse MakeReservation(ReservationRequest request, List<Room>? allRooms = null)
        {
            var response = new ReservationResponse
            {
                Result = "No Option"
            };

            try
            {
                allRooms ??= AllRooms.GetAll();

                if (!ValidateInputData(allRooms, request.NumberOfGuests))
                {
                    return response;
                }

                var possibleReservations = FindAllPossibleReservations(allRooms, request.NumberOfGuests);
                var cheapestReservation = FindCheapestReservation(possibleReservations);

                response.Result = GetReservationDescription(cheapestReservation);

                return response;
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }

        private static bool ValidateInputData(List<Room>? allRooms, int? numberOfGuests)
        {
            if (numberOfGuests == null || allRooms == null)
            {
                throw new BadHttpRequestException("Bad Request");
            }

            if (numberOfGuests <= 0)
            {
                return false;
            }

            var maxSleepsOfHotel = allRooms.Sum(room => room.Sleeps * room.AvailableCount);

            return numberOfGuests <= maxSleepsOfHotel;
        }

        private static List<Reservation> FindAllPossibleReservations(List<Room> rooms, int? numberOfGuests)
        {
            var result = new List<Reservation>();
            var currentReservation = new Reservation();

            FindReservationsRecursive(rooms, numberOfGuests, 0, currentReservation, result);

            return result;
        }

        private static void FindReservationsRecursive(List<Room> rooms, int? remainingGuests, int startIndex, Reservation currentReservation, List<Reservation> result)
        {
            if (remainingGuests == 0)
            {
                var reservation = new Reservation();
                reservation.Rooms.AddRange(currentReservation.Rooms);

                result.Add(reservation);
                return;
            }

            for (int i = startIndex; i < rooms.Count; i++)
            {
                int sleepValue = rooms[i].Sleeps;
                int availableCountValue = rooms[i].AvailableCount;

                if (remainingGuests >= sleepValue && availableCountValue > 0)
                {
                    currentReservation.Rooms.Add(rooms[i]);
                    rooms[i].AvailableCount--;

                    FindReservationsRecursive(rooms, remainingGuests - sleepValue, i, currentReservation, result);

                    currentReservation.Rooms.Remove(rooms[i]);
                    rooms[i].AvailableCount++;
                }
            }
        }

        private static Reservation FindCheapestReservation(List<Reservation> allReservations)
        {
            var result = allReservations
                    .OrderBy(reservation => reservation.Rooms.Sum(room => room.Price))
                    .First();

            return result;
        }

        private static string GetReservationDescription(Reservation reservations)
        {
            var bestPrice = reservations.Rooms.Sum(room => room.Price);
            var roomTypes = reservations.Rooms
                .Select(room => room.Type)
            .OrderBy(type => type);

            return $"{string.Join(" ", roomTypes)} - ${bestPrice}";
        }
    }
}
