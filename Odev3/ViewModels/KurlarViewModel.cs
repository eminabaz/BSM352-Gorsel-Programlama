using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Odev3.Models;
using System.Net.Http;  



namespace Odev3.ViewModels
{
    public class KurlarViewModel : BaseViewModel
    {
        
        private List<CurrencyItem> _kurlar;
        public List<CurrencyItem> Kurlar
        {
            get => _kurlar;
            set
            {
                _kurlar = value;
                OnPropertyChanged(); 
            }
        }

        public ICommand YenileCommand { get; set; }

        public KurlarViewModel()
        {
            
            YenileCommand = new Command(async () => await VerileriGetir());
            VerileriGetir();
        }

        private async Task VerileriGetir()
        {
            if (IsBusy) return; 

            IsBusy = true; 
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var json = await client.GetStringAsync("https://finans.truncgil.com/today.json");

                    // JSON parse işlemi
                    var rawData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                    // Geçici liste
                    var geciciListe = new List<CurrencyItem>();
                    string[] istenenKurlar = { "USD", "EUR", "GBP", "gram-altin", "ceyrek-altin", "gumus" };

                    foreach (var kod in istenenKurlar)
                    {
                        if (rawData.ContainsKey(kod))
                        {
                            var itemJson = rawData[kod].ToString();
                            var apiItem = JsonConvert.DeserializeObject<ApiModel>(itemJson);

                            if (apiItem != null)
                            {
                                bool yukari = !apiItem.Degisim.Replace("%","").Trim().StartsWith("-");

                                geciciListe.Add(new CurrencyItem
                                {
                                    Tur = IsimCevir(kod),
                                    Alis = apiItem.Alis,
                                    Satis = apiItem.Satis,
                                    Fark = apiItem.Degisim,
                                    Yon = yukari ? "▲" : "▼",
                                    Renk = yukari ? "Green" : "Red"
                                });
                            }
                        }
                    }

                    
                    Kurlar = geciciListe;
                }
            }
            catch (Exception)
            {
                // Hata yönetimi
            }
            finally
            {
                IsBusy = false; // ActivityIndicator dursun
            }
        }

        private string IsimCevir(string kod)
        {
            return kod switch
            {
                "USD" => "Dolar",
                "EUR" => "Euro",
                "GBP" => "Sterlin",
                "gram-altin" => "Gram Altın",
                "ceyrek-altin" => "Çeyrek Altın",
                "gumus" => "Gümüş", 
                _ => kod
            };
        }
    }

    // JSON'u karşılamak için yardımcı sınıf (Sadece burada kullanılıyor)
    public class ApiModel
    {
        [JsonProperty("Alış")] public string Alis { get; set; }
        [JsonProperty("Satış")] public string Satis { get; set; }
        [JsonProperty("Değişim")] public string Degisim { get; set; }
    }
}