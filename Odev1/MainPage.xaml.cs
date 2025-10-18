using System.Globalization;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    
    private double _ilkSayi = 0;
    private bool _yeniSayiGirisi = true; 
    private string _bekleyenIslem = null; 

    public MainPage()
    {
        InitializeComponent();

        SonucAlani.Text = "0";
    }

    //---------------------------------------------------------
    // 1. SAYI VE VİRGÜL İŞLEMLERİ
    //---------------------------------------------------------

    // Rakam Tuşları (0-9) için kullanılan metot
    private void SayiBasildi(object sender, EventArgs e)
    {
        var button = sender as Button;
        string basilanSayi = button.Text; 

        if (_yeniSayiGirisi)
        {
            // Yeni sayı girişi veya işlem sonrası
            SonucAlani.Text = basilanSayi;
            _yeniSayiGirisi = false;
        }
        else
        {
            // Mevcut sayının sonuna ekle
            if (SonucAlani.Text == "0")
            {
                SonucAlani.Text = basilanSayi;
            }
            else if (SonucAlani.Text.Length < 15) // Ekran karakter sınırı
            {
                SonucAlani.Text += basilanSayi;
            }
        }
    }

    // Virgül (,) Tuşu için kullanılan metot
    private void VirgulBasildi(object sender, EventArgs e)
    {
        // Eğer ekranda zaten virgül yoksa
        if (!SonucAlani.Text.Contains(','))
        {
            if (_yeniSayiGirisi)
            {
                SonucAlani.Text = "0,";
                _yeniSayiGirisi = false;
            }
            else
            {
                SonucAlani.Text += ",";
            }
        }
    }

    //---------------------------------------------------------
    // 2. KONTROL İŞLEMLERİ
    //---------------------------------------------------------

    // 'C' (Ekranı Temizle) Tuşu
    private void EkraniTemizle(object sender, EventArgs e)
    {
        _ilkSayi = 0;
        _bekleyenIslem = null;
        _yeniSayiGirisi = true;
        SonucAlani.Text = "0";
    }

    //---------------------------------------------------------
    // 3. MATEMATİKSEL İŞLEMLER
    //---------------------------------------------------------

    // Operatör Tuşları (+, -, x, ÷) için kullanılan metot
    private void İslemYap(object sender, EventArgs e)
    {
        var button = sender as Button;
        string yeniIslem = button.Text;

        if (double.TryParse(SonucAlani.Text.Replace(",", "."), CultureInfo.InvariantCulture, out double mevcutSayi))
        {
            if (!_yeniSayiGirisi && _bekleyenIslem != null)
            {
                // Zincirleme işlem: (Önceki Sayı) + (Mevcut Sayı) gibi
                EsittirBasildi(sender, e);

                // Yeni sonucu ilk sayı olarak kaydet
                _ilkSayi = double.Parse(SonucAlani.Text.Replace(",", "."));
            }
            else
            {
                // Yeni işlemin ilk sayısını kaydet
                _ilkSayi = mevcutSayi;
            }
        }

        _bekleyenIslem = yeniIslem;
        _yeniSayiGirisi = true; // Bir sonraki rakam girişi yeni sayı başlatsın
    }

    // '=' (Eşittir) Tuşu
    private void EsittirBasildi(object sender, EventArgs e)
    {
        if (_bekleyenIslem == null) return; // Bekleyen işlem yoksa bir şey yapma

        if (double.TryParse(SonucAlani.Text.Replace(",", "."), CultureInfo.InvariantCulture, out double ikinciSayi))
        {
            double sonuc = 0;

            // Yapılacak işlemi belirle
            switch (_bekleyenIslem)
            {
                case "+":
                    sonuc = _ilkSayi + ikinciSayi;
                    break;
                case "-":
                    sonuc = _ilkSayi - ikinciSayi;
                    break;
                case "x": // XAML'deki 'x' karakterine dikkat et
                    sonuc = _ilkSayi * ikinciSayi;
                    break;
                case "÷":
                    if (ikinciSayi != 0)
                    {
                        sonuc = _ilkSayi / ikinciSayi;
                    }
                    else
                    {
                        SonucAlani.Text = "Sıfıra Bölme!";
                        _ilkSayi = 0;
                        _bekleyenIslem = null;
                        _yeniSayiGirisi = true;
                        return;
                    }
                    break;
            }

            // Sonucu ekranda göster
            SonucAlani.Text = sonuc.ToString("N");

            // Sonraki işlem için hazırlık
            _ilkSayi = sonuc;
            _bekleyenIslem = null;
            _yeniSayiGirisi = true;
        }
    }

    private void SilBasildi(object sender, EventArgs e)
    {
        var text = SonucAlani.Text;
        if (text.Length > 1)
        {
            text = text.Substring(0, text.Length - 1);
            SonucAlani.Text = text;
        }
        else
        {
            SonucAlani.Text = "0";
            _yeniSayiGirisi = true;


        }
    }
}