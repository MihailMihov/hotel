using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Readers;

public class RoomKindReader : Reader
{
    public RoomKindReader()
    {
        ReaderType = ReaderType.RoomKind;
    }

    public RoomKindReader(IEnumerable<RoomKind> roomKinds) : this()
    {
        var table = new Table();
        table.Border(TableBorder.HeavyHead);
        table.Expand();
        table.AddColumn("ID");
        table.AddColumn("Name");
        foreach (var roomKind in roomKinds) table.AddRow(roomKind.Id.ToString(), roomKind.Name);
        AnsiConsole.Write(table);
        AnsiConsole.Write(new Rule("[yellow]Press any key to go back[/]"));
        Console.Read();
    }
}