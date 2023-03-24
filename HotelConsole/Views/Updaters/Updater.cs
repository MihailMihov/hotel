using HotelConsole.Views.Menus;
using Spectre.Console;

namespace HotelConsole.Views.Updaters;

public class Updater : View
{
    public UpdaterType UpdaterType;

    protected Updater()
    {
        ViewType = ViewType.Updater;
    }
}

public enum UpdaterType
{
    Building,
    Client,
    Parking,
    Reservation,
    Room,
    RoomKind,
    Vehicle
}