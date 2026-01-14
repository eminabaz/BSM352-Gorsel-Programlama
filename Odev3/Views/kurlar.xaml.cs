using Odev3.ViewModels;

namespace Odev3.Views;

public partial class kurlar : ContentPage
{
	public kurlar()
	{
		InitializeComponent();

		this.BindingContext = new KurlarViewModel();	
    }
}