using HotelConsole.Models;
using HotelConsole.Views.Menus;
using Spectre.Console;

namespace HotelConsole.Views.Deleters;

public class BuildingDeleter : Deleter
{
    public List<int> BuildingIds = new();
    public BuildingDeleter()
    {
        DeleterType = DeleterType.Building;
    }
    public BuildingDeleter(ICollection<Building> buildings) : this()
    {
        var targetBuildingNames = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Which building(s) would you like to delete?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other buildings)[/]")
                .AddChoices(buildings.Select(b => b.Name)));

        if (!AnsiConsole.Confirm($"Are you sure that you want to delete {targetBuildingNames.Count} buildings?"))
        {
            return;
        }
        
        foreach (var buildingName in targetBuildingNames)
        {
            BuildingIds.Add(buildings.First(b => b.Name == buildingName).Id);
        }
    }
}