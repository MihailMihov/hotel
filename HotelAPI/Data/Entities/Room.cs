namespace HotelAPI.Models;

public class Room
{
    public int Id { get; set; }

    public int Floor { get; set; }

    public int KindId { get; set; }
    public virtual RoomKind Kind { get; set; } = null!;
    
    public int BuildingId { get; set; }
    public virtual Building Building { get; set; } = null!;
    
    public virtual ICollection<Client> Clients { get; } = new List<Client>();
    
    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}