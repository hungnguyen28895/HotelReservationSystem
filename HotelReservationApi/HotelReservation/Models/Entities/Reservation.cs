using HotelReservationSystem.HotelReservation.Models.Entities;
using HotelReservationSystem.HotelReservation.Models.Enums;

namespace HotelReservationApi.HotelReservation.Models.Entities
{
    public class Reservation
    {
        public List<Room> Rooms { get; set; }

        public Reservation()
        {
            Rooms = new List<Room>();
        }
    }
}
