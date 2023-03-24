using Spectre.Console;

namespace HotelConsole;

internal class Utility
{
    public static void WriteFiglet()
    {
        var figlet = new Padder(
            new FigletText("Hotel-Console")
                .Centered()
                .Color(Color.DarkOrange)
        );

        AnsiConsole.Write(figlet);
    }

    public static HttpClient CreateHttpClient()
    {
        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;

        return new HttpClient(handler);
    }
}