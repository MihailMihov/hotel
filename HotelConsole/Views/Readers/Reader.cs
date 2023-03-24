namespace HotelConsole.Views.Readers;

public class Reader : View
{
    public ReaderType ReaderType;
    
    protected Reader()
    {
        ViewType = ViewType.Reader;
    }
}

public enum ReaderType
{
    Building,
    Client,
    Parking,
    Reservation,
    Room,
    RoomKind,
    Vehicle
}