using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.HotelReservation.Models.Dtos
{
    public class ReservationRequest
    {
        [Required(ErrorMessage = "Number Of Guests is required")]
        public int? NumberOfGuests { get; set; }
    }
}
