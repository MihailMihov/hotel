using Newtonsoft.Json;

namespace HotelConsole.Models;

public class RoomKind
{
    [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int Id { get; set; }

    [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("rooms", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public ICollection<Room> Rooms { get; set; }
}