using System.Globalization;

namespace MauiApp1;

public partial class Bilimsel : ContentPage
{

    private double _ilkSayi = 0;
    private bool _yeniSayiGirisi = true;
    private string _bekleyenIslem = null;

    public Bilimsel()
    {
        InitializeComponent();
    }

    private void SayiBasildi(object sender, EventArgs e)
    {
        var button = sender as Button;
        string basilanSayi = button.Text;


        if (_yeniSayiGirisi)
        {
            SonucAlani.Text = basilanSayi;
            _yeniSayiGirisi = false;
        }

        else
        {
            if (SonucAlani.Text == "0")
            {
                SonucAlani.Text = basilanSayi;
            }

            else
            {
                SonucAlani.Text += basilanSayi;

            }

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

    private void ÝslemBasildi(object sender, EventArgs e)
    {
        var button = sender as Button;
        string islem = button.Text;

        if (double.TryParse(SonucAlani.Text.Replace(",", "."), CultureInfo.InvariantCulture, out double mevcutSayi))
        {
            if (!_yeniSayiGirisi && _bekleyenIslem != null)
            {
                // (Önceki Sayý) + (Mevcut Sayý)
                EsittirBasildi(sender, e);

                
                _ilkSayi = double.Parse(SonucAlani.Text.Replace(",", "."));
            }
            else
            {
                // Yeni iþlemin ilk sayýsýný kaydet
                _ilkSayi = mevcutSayi;
                SonucAlani.Text = mevcutSayi + " " + islem;
            }
        }

        _bekleyenIslem = islem;
        _yeniSayiGirisi = true;

    }


    private void EsittirBasildi(object sender, EventArgs e)
    {
        if (_bekleyenIslem == null) return; // Bekleyen iþlem yoksa bir þey yapma

        if (double.TryParse(SonucAlani.Text.Replace(",", "."), CultureInfo.InvariantCulture, out double ikinciSayi))
        {
            double sonuc = 0;

            // Yapýlacak iþlemi belirle
            switch (_bekleyenIslem)
            {
                case "+": //toplama
                    sonuc = _ilkSayi + ikinciSayi;
                    break;
                case "-": //çýkarma
                    sonuc = _ilkSayi - ikinciSayi;
                    break;
                case "x": //çarpma
                    sonuc = _ilkSayi * ikinciSayi;
                    break;
                case "÷": //bölme
                    if (ikinciSayi != 0)
                    {
                        sonuc = _ilkSayi / ikinciSayi;
                    }
                    else
                    {
                        SonucAlani.Text = "Sýfýra Bölme!";
                        _ilkSayi = 0;
                        _bekleyenIslem = null;
                        _yeniSayiGirisi = true;
                        return;
                    }
                    break;
            }

            // Sonucu ekranda göster
            SonucAlani.Text = sonuc.ToString("N");

            // Sonraki iþlem için hazýrlýk
            _ilkSayi = sonuc;
            _bekleyenIslem = null;
            _yeniSayiGirisi = true;
        }
    }

    private void VirgulBasildi(object sender, EventArgs e)
    {
        // Eðer ekranda zaten virgül yoksa
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

    private void AdvÝslemBasildi(object sender, EventArgs e)
    {
        var button = sender as Button;
        string islem = button.Text;
        double sonuc = 0.0;
        string sonucStr = null;
        double sayi = double.Parse(SonucAlani.Text.Replace(",", "."));


        switch (islem)
        {
            case "sin":
                sayi = sayi * Math.PI / 180;
                sonuc = Math.Sin(sayi);
                break;

            case "cos":

                sayi = sayi * Math.PI / 180;
                sonuc = Math.Cos(sayi);
                break;
      

            case "tan":
                sayi = sayi * Math.PI / 180;
                sonuc = Math.Tan(sayi);
                break;

            case "sec":
                sayi = sayi * Math.PI / 180;
                sonuc = 1/(Math.Cos(sayi));
                break;

            case "cosec":
                sayi = sayi * Math.PI / 180;
                sonuc = 1 / (Math.Sin(sayi));
                break;

            case "cot":
                sayi = sayi * Math.PI / 180;
                sonuc = 1/(Math.Tan(sayi));
                break;

            case "log":

                sonuc = Math.Log10(sayi);
                break;

            case "ln":
                sonuc = Math.Log(sayi);
                break;

            case "x²":
                sonuc = Math.Pow(sayi, 2);
                break;

            case "\u221A\u00AF":
                sonuc = Math.Sqrt(sayi);
                break;

        }

           //Sonuc ekranýnda kaç hane ondalýk deðerin gösterileceði
            if (Math.Abs(sonuc % 1) < 1e-12)
            {
                // Tam sayý gibi göster (örnek: 5)
                sonucStr = ((long)Math.Round(sonuc)).ToString();
            }
            else
            {
                // Ondalýklýysa gereksiz sýfýrlarý at
                sonucStr = sonuc.ToString("G15", System.Globalization.CultureInfo.InvariantCulture);
            } 

        SonucAlani.Text = sonucStr;
        _yeniSayiGirisi = true;


    }

    private void SayfaTemizleBasildi(object sender, EventArgs e)
    {
        _ilkSayi = 0;
        _bekleyenIslem = null;
        SonucAlani.Text = "0";
        _yeniSayiGirisi = true;


    }

}
