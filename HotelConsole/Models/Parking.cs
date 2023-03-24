using Newtonsoft.Json;

namespace HotelConsole.Models;

public class Parking
{
    [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int Id { get; set; }

    [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("capacity", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public int? Capacity { get; set; }

    [JsonProperty("vehicles", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public ICollection<Vehicle> Vehicles { get; set; }
}