namespace HotelAPI.Data.Entities;

public class Reservation
{
    public int Id { get; set; }

    public string ClientEmail { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool LateCheckout { get; set; }

    public int RoomId { get; set; }
    public virtual Room Room { get; set; } = null!;
}