namespace HotelAPI.Data.Entities;

public class Building
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Floors { get; set; }

    public virtual ICollection<Room> Rooms { get; } = new List<Room>();
}