using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data.Entities;

public class BookingEntity
{

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string EventId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int TicketQuantity { get; set; } = 1;

    public DateTime BookingTime { get; set; } = DateTime.Now;

}




