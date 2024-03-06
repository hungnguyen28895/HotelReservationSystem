using HotelReservationSystem.HotelReservation.Models.Dtos;
using HotelReservationSystem.HotelReservation.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.HotelReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("MakeReservation")]
        public IActionResult MakeReservation([FromBody] ReservationRequest resquest)
        {
            var response = _reservationService.MakeReservation(resquest);

            return Ok(response);
        }
    }
}
