using Newtonsoft.Json;

namespace HotelConsole.Models;

public class Vehicle
{
    [JsonProperty("registration", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string Registration { get; set; }

    [JsonProperty("clientId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int ClientId { get; set; }

    [JsonProperty("client", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
    public Client Client { get; set; }

    [JsonProperty("parkingId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int ParkingId { get; set; }

    [JsonProperty("parking", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
    public Parking Parking { get; set; }
}