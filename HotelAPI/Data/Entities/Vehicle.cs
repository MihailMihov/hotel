namespace HotelAPI.Data.Entities;

public class Vehicle
{
    public string Registration { get; set; } = null!;

    public int ClientId { get; set; }
    public virtual Client Client { get; set; } = null!;

    public int ParkingId { get; set; }
    public virtual Parking Parking { get; set; } = null!;
}