namespace HotelConsole.Views.Creators;

public abstract class Creator : View
{
    public CreatorType CreatorType;

    protected Creator()
    {
        ViewType = ViewType.Creator;
    }
}

public enum CreatorType
{
    Building,
    Client,
    Parking,
    Reservation,
    Room,
    RoomKind,
    Vehicle
}