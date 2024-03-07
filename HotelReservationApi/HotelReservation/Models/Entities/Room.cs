using HotelReservationSystem.HotelReservation.Models.Enums;
using System.Diagnostics.CodeAnalysis;

namespace HotelReservationSystem.HotelReservation.Models.Entities
{
    public class Room
    {
        [NotNull]
        public RoomType Type { get; set; }

        public int Sleeps { get; set; }

        [NotNull]
        public int Price { get; set; }

        public int AvailableCount { get; set; }
    }
}
