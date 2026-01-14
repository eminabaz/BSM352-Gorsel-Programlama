using CommunityToolkit.Mvvm.ComponentModel;

namespace Odev3.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isDarkTheme;

        public SettingsViewModel()
        {
            // Sayfa açıldığında, mevcut tema neyse onu algıla
            // Eğer sistem zaten Dark ise anahtar açık (True) gelsin.
            IsDarkTheme = Application.Current.UserAppTheme == AppTheme.Dark;
        }

        // IsDarkTheme değişkeni değiştiğinde (Anahtara basıldığında) bu metot otomatik çalışır
        partial void OnIsDarkThemeChanged(bool value)
        {
            // Value = True ise Koyu, False ise Aydınlık yap
            Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
        }
    }
}