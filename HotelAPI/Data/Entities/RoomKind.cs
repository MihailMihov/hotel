namespace HotelAPI.Data.Entities;

public class RoomKind
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; } = new List<Room>();
}