using System.ComponentModel;

namespace Odev2;

public partial class KrediHesaplama : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private double _aylikTaksit;
    public double AylikTaksit
    {
        get => _aylikTaksit;
        set { if (_aylikTaksit != value) { _aylikTaksit = value; OnPropertyChanged(nameof(AylikTaksit)); } }
    }

    private double _toplamOdeme;
    public double ToplamOdeme
    {
        get => _toplamOdeme;
        set { if (_toplamOdeme != value) { _toplamOdeme = value; OnPropertyChanged(nameof(ToplamOdeme)); } }
    }

    private double _toplamFaiz;
    public double ToplamFaiz
    {
        get => _toplamFaiz;
        set { if (_toplamFaiz != value) { _toplamFaiz = value; OnPropertyChanged(nameof(ToplamFaiz)); } }
    }

    public KrediHesaplama()
	{
		InitializeComponent();
        this.BindingContext = this;
	}

    private void KrediTipiDegisti(object sender, EventArgs e)
    {

    }

    private void HesaplamaBasýldý(object sender, EventArgs e)
    {
        if (!double.TryParse(entryKrediTutari.Text, out double tutar) || tutar <= 0 ||
                !double.TryParse(entryFaizOrani.Text, out double yillikFaizOrani) || yillikFaizOrani <= 0 ||
                KrediPicker.SelectedItem == null)
        {
            DisplayAlert("Hata", "Lütfen tüm giriþleri kontrol edin.", "Tamam");
            return;
        }
        int vade = (int)sliderVadeSuresi.Value;
        string secilenKrediTuru = KrediPicker.SelectedItem.ToString();

        
        double KKDF, BSMV;
        switch (secilenKrediTuru)
        {
            case "Ýhtiyaç Kredisi": KKDF = 0.15; BSMV = 0.10; break;
            case "Konut Kredisi": KKDF = 0.00; BSMV = 0.05; break;
            case "Taþýt Kredisi": KKDF = 0.15; BSMV = 0.05; break;
            case "Ticari Kredi": KKDF = 0.00; BSMV = 0.05; break;
            default: return;
        }

       
        double aylikFaizOrani = (yillikFaizOrani / 100.0) / 12.0;

        
        double brutFaiz = (aylikFaizOrani + (aylikFaizOrani * BSMV) + (aylikFaizOrani * KKDF));

        double powFactor = Math.Pow(1 + brutFaiz, vade);

        
        double taksit = (powFactor * brutFaiz / (powFactor - 1)) * tutar;

       
        double toplam = taksit * vade;

        
        double toplamFaizHesap = toplam - tutar;

        
        AylikTaksit = taksit;
        ToplamOdeme = toplam;
        ToplamFaiz = toplamFaizHesap;
    }
}
