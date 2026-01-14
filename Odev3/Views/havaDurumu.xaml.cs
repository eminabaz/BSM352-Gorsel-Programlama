using Odev3.ViewModels;

namespace Odev3.Views;

public partial class havaDurumu : ContentPage
{
    public havaDurumu(HavaDurumuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}