using Odev3.ViewModels;


namespace Odev3.Views;


public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;

	}
}