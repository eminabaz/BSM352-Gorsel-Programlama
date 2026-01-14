using System.Collections.ObjectModel;
using System.Text.RegularExpressions; // Regex için gerekli
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Odev3.Models;
using Odev3.Views;

namespace Odev3.ViewModels
{
    public partial class HaberlerViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy;

        // Haberler Listesi
        public ObservableCollection<HaberItem> HaberlerListesi { get; set; } = new();

        // Kategoriler Listesi
        public ObservableCollection<HaberKategori> Kategoriler { get; set; } = new();

        public HaberlerViewModel()
        {
            // Kategorileri Oluştur (TRT Linkleri)
            Kategoriler.Add(new HaberKategori { Baslik = "GÜNDEM", RssUrl = "https://www.trthaber.com/gundem_articles.rss", IsSelected = true });
            Kategoriler.Add(new HaberKategori { Baslik = "EKONOMİ", RssUrl = "https://www.trthaber.com/ekonomi_articles.rss" });
            Kategoriler.Add(new HaberKategori { Baslik = "SPOR", RssUrl = "https://www.trthaber.com/spor_articles.rss" });
            Kategoriler.Add(new HaberKategori { Baslik = "BİLİM", RssUrl = "https://www.trthaber.com/bilim_teknoloji_articles.rss" });
            Kategoriler.Add(new HaberKategori { Baslik = "GÜNCEL", RssUrl = "https://www.trthaber.com/guncel_articles.rss" });

            // İlk kategoriyi yükle
            if (Kategoriler.Count > 0)
            {
                LoadNewsAsync(Kategoriler[0]);
            }
        }

        [RelayCommand]
        public async Task LoadNewsAsync(HaberKategori secilenKategori)
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                // 1. Kategori Seçim Görselliğini Güncelle
                foreach (var kat in Kategoriler)
                {
                    kat.IsSelected = (kat == secilenKategori);
                    kat.BackgroundColor = kat.IsSelected ? Color.FromArgb("#512BD4") : Colors.Transparent;
                    kat.TextColor = kat.IsSelected ? Colors.White : Colors.Gray;
                }

                // Listeyi tetikleyerek UI güncellemesi yaptırıyoruz (Renk değişimi için)
                // ObservableCollection içindeki property değişimi bazen UI'ı tetiklemez, bu yüzden listeyi yeniliyoruz.
                var temp = new List<HaberKategori>(Kategoriler);
                Kategoriler.Clear();
                foreach (var item in temp) Kategoriler.Add(item);


                // 2. API ÇAĞRISI
                string apiUrl = $"https://api.rss2json.com/v1/api.json?rss_url={secilenKategori.RssUrl}";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(apiUrl);

                    // JsonProperty ayarları sayesinde artık veriler düzgün eşleşecek
                    var data = JsonConvert.DeserializeObject<RssRoot>(response);

                    HaberlerListesi.Clear();
                    if (data?.Items != null)
                    {
                        foreach (var item in data.Items)
                        {
                            // --- RESİM BULMA MANTIĞI ---

                            // 1. Öncelik: Thumbnail dolu mu? (Genelde boştur ama kontrol edelim)
                            if (string.IsNullOrEmpty(item.Thumbnail))
                            {
                                // 2. Öncelik: Enclosure (Eklenti) var mı?
                                if (item.Enclosure != null && !string.IsNullOrEmpty(item.Enclosure.Link))
                                {
                                    item.Thumbnail = item.Enclosure.Link;
                                }
                            }

                            // 3. Öncelik: Hala resim yoksa Description içindeki HTML'den ara
                            if (string.IsNullOrEmpty(item.Thumbnail) && !string.IsNullOrEmpty(item.Description))
                            {
                                // Regex ile <img src="..."> yapısını arıyoruz
                                var match = Regex.Match(item.Description, "<img.+?src=[\"'](.+?)[\"']");
                                if (match.Success)
                                {
                                    item.Thumbnail = match.Groups[1].Value;
                                }
                            }

                            // 4. Öncelik: Hiçbir yerde resim yoksa varsayılan resmi koy
                            if (string.IsNullOrEmpty(item.Thumbnail))
                            {
                                item.Thumbnail = "dotnet_bot.png";
                            }

                            HaberlerListesi.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Hata", $"Haberler yüklenemedi: {ex.Message}", "Tamam");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task OpenDetailAsync(HaberItem haber)
        {
            if (haber == null) return;

            // Detay sayfasına git ve haber verisini taşı
            await Application.Current.MainPage.Navigation.PushAsync(new HaberDetayPage(haber));
        }
    }
}