namespace HotelConsole.Views.Menus;

public abstract class Menu : View
{
    protected Menu()
    {
        ViewType = ViewType.Menu;
    }
}