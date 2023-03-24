namespace HotelConsole.Views;

public abstract class View
{
    public View? Next = null;
    public ViewType ViewType = ViewType.View;
}

public enum ViewType
{
    View,
    Menu,
    Creator,
    Reader,
    Updater,
    Deleter
}