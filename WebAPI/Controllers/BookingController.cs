using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Entities;
using WebAPI.Services;

namespace WebAPI.Controllers;



[Route("api/[controller]")]
[ApiController]
public class BookingController(BookingService bookingService) : Controller
{

    private readonly BookingService _bookingService = bookingService;





    [HttpPost("create")]
    public async Task<IActionResult> CreateBooking([FromBody] BookingEntity booking)
    {



        if (booking == null)
        {
            return BadRequest("Invalid booking data.");
        }
        var result = await _bookingService.CreateBooking(booking.EventId, booking.UserId, booking.TicketQuantity);
        if (result)
        {
            return Ok("Booking created successfully.");
        }
        else
        {
            return StatusCode(500, "An error occurred while creating the booking.");
        }
    }


    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUsersBookingById(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("Invalid user ID.");
        }
        var bookings = await _bookingService.GetUserById(userId);
        if (bookings == null || !bookings.Any())
        {
            return NotFound("No bookings found for this user.");
        }
        return Ok(bookings);

    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await _bookingService.GetAllBookings();
        if (bookings == null || !bookings.Any())
        {
            return NotFound("No bookings found.");
        }
        return Ok(bookings);
    }




}


