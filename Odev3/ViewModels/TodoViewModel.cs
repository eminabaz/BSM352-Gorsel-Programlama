using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Odev3.Models;
using Odev3.Services;
using Odev3.Views;

namespace Odev3.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        private readonly TodoService _todoService;

        [ObservableProperty]
        bool isBusy;

        public ObservableCollection<Gorev> Gorevler { get; set; } = new();

        public TodoViewModel(TodoService todoService)
        {
            _todoService = todoService;
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var list = await _todoService.GetGorevlerAsync();
                Gorevler.Clear();
                foreach (var item in list) Gorevler.Add(item);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task AddAsync()
        {
            // Yeni ekleme sayfasına git
            await Application.Current.MainPage.Navigation.PushAsync(new TodoDetayPage(new TodoDetayViewModel(_todoService)));
        }

        [RelayCommand]
        async Task EditAsync(Gorev gorev)
        {
            if (gorev == null) return;
            // Düzenleme sayfasına git (Görevi parametre olarak gönder)
            await Application.Current.MainPage.Navigation.PushAsync(new TodoDetayPage(new TodoDetayViewModel(_todoService), gorev));
        }

        [RelayCommand]
        async Task DeleteAsync(Gorev gorev)
        {
            if (gorev == null) return;

            // ONAY KUTUSU (PDF İsteri)
            bool answer = await Application.Current.MainPage.DisplayAlert("Silinsin mi?", "Bu görevi silmek istediğinize emin misiniz?", "Evet", "Hayır");

            if (answer)
            {
                await _todoService.DeleteGorevAsync(gorev.Id);
                Gorevler.Remove(gorev);
            }
        }
    }
}