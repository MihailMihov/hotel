using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Updaters;

public class RoomUpdater : Updater
{
    public readonly Room Room;

    public RoomUpdater()
    {
        UpdaterType = UpdaterType.Room;
    }
    public RoomUpdater(ICollection<Room> rooms) : this()
    {
        var targetRoomId = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which room would you like to update?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other rooms)[/]")
                .AddChoices(rooms.Select(b => b.Id.ToString())));

        Room = rooms.First(b => b.Id.ToString() == targetRoomId);
        
        while (true)
        {
            var targetField = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to update?")
                    .AddChoices("Building ID", "Floor", "Room Kind ID", "Back to Rooms Menu"));
            
            if(targetField == "Back to Rooms Menu") break;

            switch (targetField)
            {
                case "Building ID":
                    Room.BuildingId = AnsiConsole.Ask<int>($"Enter [yellow]{Room.Id}'s[/] new [darkorange]building id[/]:");
                    break;
                case "Floor":
                    Room.Floor =
                        AnsiConsole.Ask<int>($"Enter [yellow]{Room.Id}'s[/] new [darkorange]floor[/]:");
                    break;
                case "Room ID":
                    Room.KindId =
                        AnsiConsole.Ask<int>($"Enter [yellow]{Room.Id}'s[/] new [darkorange]room kind id[/]:");
                    break;
            }
        }
    }
}