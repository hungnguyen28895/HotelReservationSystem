using HotelReservationSystem.HotelReservation.Models.Dtos;
using HotelReservationSystem.HotelReservation.Models.Entities;

namespace HotelReservationSystem.HotelReservation.Services.Abstract
{
    public interface IReservationService
    {
        ReservationResponse MakeReservation(ReservationRequest resquest, List<Room>? allRooms = null);
    }
}
