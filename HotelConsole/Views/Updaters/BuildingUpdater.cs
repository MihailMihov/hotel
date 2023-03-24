using HotelConsole.Models;
using HotelConsole.Views.Menus;
using Spectre.Console;

namespace HotelConsole.Views.Updaters;

public class BuildingUpdater : Updater
{
    public readonly Building Building;

    public BuildingUpdater()
    {
        UpdaterType = UpdaterType.Building;
    }
    public BuildingUpdater(ICollection<Building> buildings) : this()
    {
        var targetBuildingName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which building would you like to update?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other buildings)[/]")
                .AddChoices(buildings.Select(b => b.Name)));

        Building = buildings.First(b => b.Name == targetBuildingName);
        
        while (true)
        {
            var targetField = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to update?")
                    .AddChoices("Name", "Floors", "Back to Buildings Menu"));
            
            if(targetField == "Back to Buildings Menu") break;

            switch (targetField)
            {
                case "Name":
                    Building.Name = AnsiConsole.Ask<string>($"Enter [yellow]{Building.Name}'s[/] new [darkorange]name[/]:");
                    break;
                case "Floors":
                    Building.Floors =
                        AnsiConsole.Ask<int>($"Enter [yellow]{Building.Name}'s[/] new [darkorange]floor count[/]:");
                    break;
            }
        }
    }
}