using HotelConsole.Models;
using HotelConsole.Views.Menus;
using Spectre.Console;

namespace HotelConsole.Views.Readers;

public class BuildingReader : Reader
{
    public BuildingReader()
    {
        ReaderType = ReaderType.Building;
    }
    public BuildingReader(IEnumerable<Building> buildings) : this()
    {
        var table = new Table();
        table.Border(TableBorder.HeavyHead);
        table.Expand();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Floors");
        table.AddColumn("Room Count");
        foreach (var building in buildings)
        {
            table.AddRow(building.Id.ToString(), building.Name, building.Floors.ToString(), building.Rooms.Count.ToString());
        }
        AnsiConsole.Write(table);
        AnsiConsole.Write(new Rule("[yellow]Press any key to go back[/]"));
        Console.Read();
    }
}