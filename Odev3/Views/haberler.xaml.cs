using Odev3.ViewModels;

namespace Odev3.Views;

public partial class haberler : ContentPage
{
    public haberler(HaberlerViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}