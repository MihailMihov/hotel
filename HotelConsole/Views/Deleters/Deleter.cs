namespace HotelConsole.Views.Deleters;

public class Deleter : View
{
    public DeleterType DeleterType;

    protected Deleter()
    {
        ViewType = ViewType.Deleter;
    }
}

public enum DeleterType
{
    Building,
    Client,
    Parking,
    Reservation,
    Room,
    RoomKind,
    Vehicle
}