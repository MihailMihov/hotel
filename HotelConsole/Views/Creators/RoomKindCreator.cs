using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Creators;

public class RoomKindCreator : Creator
{
    public RoomKind RoomKind;

    public RoomKindCreator()
    {
        CreatorType = CreatorType.RoomKind;
        RoomKind = new RoomKind
        {
            Name = AnsiConsole.Ask<string>("Enter the [yellow]room kind's[/] [darkorange]name[/]:")
        };
    }
}