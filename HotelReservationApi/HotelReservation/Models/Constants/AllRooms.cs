using HotelReservationSystem.HotelReservation.Models.Entities;
using HotelReservationSystem.HotelReservation.Models.Enums;

namespace HotelReservationSystem.HotelReservation.Models.Constants
{
    public class AllRooms
    {
        public static List<Room> GetAll()
        {
            return
             [
                new Room { Type = RoomType.Single, Sleeps = 1, Price = 30, AvailableCount = 2 },
                new Room { Type = RoomType.Double, Sleeps = 2, Price = 50, AvailableCount = 3 },
                new Room { Type = RoomType.Family, Sleeps = 4, Price = 85, AvailableCount = 1 },
            ];
        }
    }
}
