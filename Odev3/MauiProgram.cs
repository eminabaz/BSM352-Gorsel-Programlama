using Microsoft.Extensions.Logging;
using Odev3.Services;
using Odev3.ViewModels;
using Odev3.Views;

namespace Odev3
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddSingleton<IAuthService, AuthService>();


            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            //Haberler için
            builder.Services.AddTransient<Odev3.Views.haberler>();
            builder.Services.AddTransient<Odev3.ViewModels.HaberlerViewModel>();

            // Detay sayfası (Parametreli olduğu için Transient olarak eklemek yeterli)
            builder.Services.AddTransient<Odev3.Views.HaberDetayPage>();

            //HavaDurumu
   
            builder.Services.AddTransient<Odev3.Views.havaDurumu>();
            builder.Services.AddTransient<Odev3.ViewModels.HavaDurumuViewModel>();

            //Todo için
            builder.Services.AddSingleton<Odev3.Services.TodoService>(); // Servis
            builder.Services.AddTransient<Odev3.ViewModels.TodoViewModel>(); // List VM
            builder.Services.AddTransient<Odev3.ViewModels.TodoDetayViewModel>(); // Detay VM
            builder.Services.AddTransient<Odev3.Views.todo>(); // List Page
            builder.Services.AddTransient<Odev3.Views.TodoDetayPage>(); // Detay Page

            //Ayarlar
            builder.Services.AddTransient<Odev3.Views.ayarlar>();
            builder.Services.AddTransient<Odev3.ViewModels.SettingsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
