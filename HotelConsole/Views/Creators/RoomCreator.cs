using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Creators;

public class RoomCreator : Creator
{
    public Room Room;
    
    public RoomCreator()
    {
        CreatorType = CreatorType.Room;
        Room = new Room
        {
            BuildingId = AnsiConsole.Ask<int>("Enter the [yellow]room's[/] [darkorange]building id[/]:"),
            Floor = AnsiConsole.Ask<int>("Enter the [yellow]room's[/] [darkorange]floor[/]:"),
            KindId = AnsiConsole.Ask<int>("Enter the [yellow]room's[/] [darkorange]kind id[/]:"),
        };
    }
}