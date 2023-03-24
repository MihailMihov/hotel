using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Readers;

public class RoomReader : Reader
{
    public RoomReader()
    {
        ReaderType = ReaderType.Room;
    }

    public RoomReader(IEnumerable<Room> rooms) : this()
    {
        var table = new Table();
        table.Border(TableBorder.HeavyHead);
        table.Expand();
        table.AddColumn("ID");
        table.AddColumn("Building ID");
        table.AddColumn("Floor");
        table.AddColumn("Kind ID");
        foreach (var room in rooms)
            table.AddRow(room.Id.ToString(), room.BuildingId.ToString(), room.Floor.ToString(), room.KindId.ToString());
        AnsiConsole.Write(table);
        AnsiConsole.Write(new Rule("[yellow]Press any key to go back[/]"));
        Console.Read();
    }
}