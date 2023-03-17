namespace HotelAPI.Data.Entities;

public class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Ucn { get; set; } = null!;

    public int RoomId { get; set; }
    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();
}