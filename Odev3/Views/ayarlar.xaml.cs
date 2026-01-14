using Odev3.ViewModels;

namespace Odev3.Views;

public partial class ayarlar : ContentPage
{
    public ayarlar(SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}