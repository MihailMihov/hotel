using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Deleters;

public class RoomKindDeleter : Deleter
{
    public List<int> RoomKindIds = new();
    public RoomKindDeleter()
    {
        DeleterType = DeleterType.RoomKind;
    }
    public RoomKindDeleter(ICollection<RoomKind> roomKinds) : this()
    {
        var targetRoomKindNames = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Which room kind(s) would you like to delete?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other room kinds)[/]")
                .AddChoices(roomKinds.Select(b => b.Name)));

        if (!AnsiConsole.Confirm($"Are you sure that you want to delete {targetRoomKindNames.Count} room kinds?"))
        {
            return;
        }
        
        foreach (var roomKindName in targetRoomKindNames)
        {
            RoomKindIds.Add(roomKinds.First(b => b.Name == roomKindName).Id);
        }
    }
}