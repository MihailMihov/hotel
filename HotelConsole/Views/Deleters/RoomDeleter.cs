using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Deleters;

public class RoomDeleter : Deleter
{
    public List<int> RoomIds = new();

    public RoomDeleter()
    {
        DeleterType = DeleterType.Room;
    }

    public RoomDeleter(ICollection<Room> rooms) : this()
    {
        var targetRoomNames = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Which room(s) would you like to delete?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other rooms)[/]")
                .AddChoices(rooms.Select(b => b.Id.ToString())));

        if (!AnsiConsole.Confirm($"Are you sure that you want to delete {targetRoomNames.Count} rooms?")) return;

        foreach (var roomName in targetRoomNames) RoomIds.Add(rooms.First(b => b.Id.ToString() == roomName).Id);
    }
}