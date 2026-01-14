using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Odev3.Models;
using Odev3.Services;

namespace Odev3.ViewModels
{
    public partial class TodoDetayViewModel : ObservableObject
    {
        private readonly TodoService _todoService;

        [ObservableProperty]
        Gorev gorev;

        public TodoDetayViewModel(TodoService todoService)
        {
            _todoService = todoService;
            // Varsayılan yeni görev şablonu
            Gorev = new Gorev { Tarih = DateTime.Now, Saat = DateTime.Now.TimeOfDay };
        }

        // Eğer düzenleme modundaysak veriyi doldur
        public void Initialize(Gorev gorev)
        {
            if (gorev != null)
                Gorev = gorev;
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Gorev.Baslik))
            {
                await Application.Current.MainPage.DisplayAlert("Hata", "Başlık boş olamaz", "Tamam");
                return;
            }

            if (string.IsNullOrEmpty(Gorev.Id))
            {
                // ID yoksa YENİ KAYIT
                await _todoService.AddGorevAsync(Gorev);
            }
            else
            {
                // ID varsa GÜNCELLEME
                await _todoService.UpdateGorevAsync(Gorev);
            }

            await Shell.Current.GoToAsync(".."); // Geri dön
        }
    }
}