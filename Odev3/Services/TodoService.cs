using Firebase.Database;
using Firebase.Database.Query;
using Odev3.Models;

namespace Odev3.Services
{
    public class TodoService
    {
        // BURAYA KENDİ REALTIME DATABASE LİNKİNİ YAPIŞTIR:
        private const string FirebaseUrl = "https://myfirstapp-api-default-rtdb.firebaseio.com/";

        private readonly FirebaseClient _firebaseClient;

        public TodoService()
        {
            _firebaseClient = new FirebaseClient(FirebaseUrl);
        }

        // Tüm Görevleri Getir
        public async Task<List<Gorev>> GetGorevlerAsync()
        {
            var items = await _firebaseClient
                .Child("Gorevler")
                .OnceAsync<Gorev>();

            return items.Select(item => new Gorev
            {
                Id = item.Key,
                Baslik = item.Object.Baslik,
                Detay = item.Object.Detay,
                Tarih = item.Object.Tarih,
                Saat = item.Object.Saat,
                YapildiMi = item.Object.YapildiMi
            }).ToList();
        }

        // Ekle
        public async Task AddGorevAsync(Gorev gorev)
        {
            await _firebaseClient
                .Child("Gorevler")
                .PostAsync(gorev);
        }

        // Güncelle
        public async Task UpdateGorevAsync(Gorev gorev)
        {
            await _firebaseClient
                .Child("Gorevler")
                .Child(gorev.Id)
                .PutAsync(gorev);
        }

        // Sil
        public async Task DeleteGorevAsync(string id)
        {
            await _firebaseClient
                .Child("Gorevler")
                .Child(id)
                .DeleteAsync();
        }
    }
}