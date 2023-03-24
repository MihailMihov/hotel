using Newtonsoft.Json;

namespace HotelConsole.Models;

public class Client
{
    [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int Id { get; set; }

    [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("ucn", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string Ucn { get; set; }

    [JsonProperty("roomId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int RoomId { get; set; }

    [JsonProperty("room", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
    public Room Room { get; set; }

    [JsonProperty("vehicles", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public ICollection<Vehicle> Vehicles { get; set; }
}