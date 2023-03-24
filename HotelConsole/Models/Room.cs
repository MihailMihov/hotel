using Newtonsoft.Json;

namespace HotelConsole.Models;

public class Room
{
    [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int Id { get; set; }

    [JsonProperty("floor", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int Floor { get; set; }

    [JsonProperty("kindId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int KindId { get; set; }

    [JsonProperty("kind", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
    public RoomKind Kind { get; set; }

    [JsonProperty("buildingId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int BuildingId { get; set; }

    [JsonProperty("building", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
    public Building Building { get; set; }

    [JsonProperty("clients", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public ICollection<Client> Clients { get; set; }

    [JsonProperty("reservations", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public ICollection<Reservation> Reservations { get; set; }
}