using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WebAPI.Data;

namespace WebAPI.Services;

public class BookingService(DataContext context, HttpClient httpClient)
{

    private readonly DataContext _context = context;
    private readonly HttpClient _httpClient = httpClient;


    public async Task<bool> CreateBooking(string eventId, string userId, int ticketQuantity)
    {
        var amount = new
        {
            Quantity = ticketQuantity
        };

        var reservationResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7138/api/ticket/updateticket/{eventId}", amount);


        var booking = new Data.Entities.BookingEntity
        {
            EventId = eventId,
            UserId = userId,
            TicketQuantity = ticketQuantity = 1
        };
        

        await _context.Bookings.AddAsync(booking);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<Data.Entities.BookingEntity>> GetUserById(string userId)
    {
        return await _context.Bookings
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Data.Entities.BookingEntity>> GetAllBookings()
    {
        return await _context.Bookings.ToListAsync();
    }



}
