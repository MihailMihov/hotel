using Newtonsoft.Json;

namespace HotelConsole.Models;

public class Reservation
{
    [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int Id { get; set; }

    [JsonProperty("clientEmail", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string ClientEmail { get; set; }

    [JsonProperty("startDate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public string StartDate { get; set; }

    [JsonProperty("endDate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public string EndDate { get; set; }

    [JsonProperty("lateCheckout", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public bool LateCheckout { get; set; }

    [JsonProperty("roomId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int RoomId { get; set; }

    [JsonProperty("room", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
    public Room Room { get; set; }
}