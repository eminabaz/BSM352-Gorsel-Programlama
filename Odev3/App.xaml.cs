using Odev3.Views;

namespace Odev3;

public partial class App : Application
{

    public App(LoginPage loginPage)
    {
        InitializeComponent();
        MainPage = new AppShell();

        // MainPage = loginPage;
    }
}
