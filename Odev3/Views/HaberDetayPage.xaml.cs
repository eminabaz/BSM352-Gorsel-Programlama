using Odev3.Models;

namespace Odev3.Views;

public partial class HaberDetayPage : ContentPage
{
    private HaberItem _haber;

    public HaberDetayPage(HaberItem haber)
    {
        InitializeComponent();
        _haber = haber;
        BindingContext = _haber;

        // DÜZELTME:
        // RSS sadece özet veri gönderdiði için, haberin tamamýný görmek adýna
        // direkt web sitesinin linkini yüklüyoruz.
        if (!string.IsNullOrEmpty(_haber.Link))
        {
            NewsWebView.Source = _haber.Link;
        }
    }

    private async void ShareButton_Clicked(object sender, EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Uri = _haber.Link,
            Title = _haber.Title,
            Text = $"{_haber.Title} - Haberi Oku"
        });
    }
}