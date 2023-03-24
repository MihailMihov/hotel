using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Creators;

public class BuildingCreator : Creator
{
    public Building Building;

    public BuildingCreator()
    {
        CreatorType = CreatorType.Building;
        Building = new Building
        {
            Name = AnsiConsole.Ask<string>("Enter the [yellow]building's[/] [darkorange]name[/]:"),
            Floors = AnsiConsole.Ask<int>("Enter the [yellow]building's[/] [darkorange]floor count[/]:")
        };
    }
}