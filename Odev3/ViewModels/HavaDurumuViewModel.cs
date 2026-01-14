using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Odev3.Models;
using System.Collections.ObjectModel;

namespace Odev3.ViewModels
{
    public partial class HavaDurumuViewModel : ObservableObject
    {
        // Şehirler Listesi
        public ObservableCollection<Sehir> Sehirler { get; set; } = new();

        // Kayıt Dosyasının Yolu (Telefonda özel bir klasöre kaydeder)
        private string _filePath = Path.Combine(FileSystem.AppDataDirectory, "saved_cities.json");

        public HavaDurumuViewModel()
        {
            // Uygulama açılınca kayıtlı şehirleri yükle
            LoadCities();
        }

        [RelayCommand]
        public async Task AddCityAsync()
        {
            // Kullanıcıdan Şehir Adı İste (Pop-up Input)
            string result = await Application.Current.MainPage.DisplayPromptAsync("Şehir Ekle", "Lütfen şehir adını giriniz:", "Ekle", "İptal");

            if (!string.IsNullOrWhiteSpace(result))
            {
                Sehir yeniSehir = new Sehir { Name = result.ToUpper() };
                Sehirler.Add(yeniSehir);

                // Her değişiklikte kaydet
                SaveCities();
            }
        }

        [RelayCommand]
        public void DeleteCity(Sehir sehir)
        {
            if (sehir != null)
            {
                Sehirler.Remove(sehir);
                SaveCities();
            }
        }

        // --- JSON İŞLEMLERİ (Ödev Kuralı: Dikkat-2) ---

        private void SaveCities()
        {
            try
            {
                // Listeyi JSON formatına çevir
                string json = JsonConvert.SerializeObject(Sehirler);
                // Dosyaya yaz
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kayıt Hatası: {ex.Message}");
            }
        }

        private void LoadCities()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    // Dosyadan oku
                    string json = File.ReadAllText(_filePath);
                    var savedList = JsonConvert.DeserializeObject<List<Sehir>>(json);

                    if (savedList != null)
                    {
                        Sehirler.Clear();
                        foreach (var item in savedList)
                        {
                            Sehirler.Add(item);
                        }
                    }
                }
                else
                {
                    // İlk açılışta boş gelmesin diye varsayılan şehirler ekleyelim
                    Sehirler.Add(new Sehir { Name = "BARTIN" });
                    Sehirler.Add(new Sehir { Name = "ANKARA" });
                    Sehirler.Add(new Sehir { Name = "ISTANBUL" });
                    SaveCities();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Yükleme Hatası: {ex.Message}");
            }
        }
    }
}