using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Updaters;

public class RoomKindUpdater : Updater
{
    public readonly RoomKind RoomKind;

    public RoomKindUpdater()
    {
        UpdaterType = UpdaterType.RoomKind;
    }
    public RoomKindUpdater(ICollection<RoomKind> roomKinds) : this()
    {
        var targetRoomKindName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which room kind would you like to update?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other room kinds)[/]")
                .AddChoices(roomKinds.Select(b => b.Name)));

        RoomKind = roomKinds.First(b => b.Name == targetRoomKindName);
        
        while (true)
        {
            var targetField = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to update?")
                    .AddChoices("Name", "Back to Room Kinds Menu"));
            
            if(targetField == "Back to Room Kinds Menu") break;

            switch (targetField)
            {
                case "Name":
                    RoomKind.Name = AnsiConsole.Ask<string>($"Enter [yellow]{RoomKind.Name}'s[/] new [darkorange]name[/]:");
                    break;
            }
        }
    }
}