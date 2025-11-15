using System.ComponentModel;

namespace Odev2;

public partial class renkSecici : ContentPage, INotifyPropertyChanged
{
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Rastgele renk üretimi için
    private readonly Random _random = new Random();

   
    private double _red = 128; 
    public double Red
    {
        get => _red;
        set
        {
            if (_red != value)
            {
                _red = value;
                OnPropertyChanged(nameof(Red));
                UpdateColor(); 
            }
        }
    }

   
    private double _green = 128; 
    public double Green
    {
        get => _green;
        set
        {
            if (_green != value)
            {
                _green = value;
                OnPropertyChanged(nameof(Green));
                UpdateColor(); 
            }
        }
    }

    
    private double _blue = 128;
    public double Blue
    {
        get => _blue;
        set
        {
            if (_blue != value)
            {
                _blue = value;
                OnPropertyChanged(nameof(Blue));
                UpdateColor();
            }
        }
    }

    
    private string _hexColor = "#808080";
    public string HexColor
    {
        get => _hexColor;
        set
        {
            if (_hexColor != value)
            {
                _hexColor = value;
                OnPropertyChanged(nameof(HexColor));
            }
        }
    }

  
    private Color _computedColor = Colors.Gray;
    public Color ComputedColor
    {
        get => _computedColor;
        set
        {
            if (_computedColor != value)
            {
                _computedColor = value;
                OnPropertyChanged(nameof(ComputedColor));
            }
        }
    }

    
    public renkSecici()
    {
        InitializeComponent();
        this.BindingContext = this;
        UpdateColor(); 
    }

    private void UpdateColor()
    {
        
        Color newColor = Color.FromRgb((int)Red, (int)Green, (int)Blue);

       
        HexColor = newColor.ToHex(); 

       
        ComputedColor = newColor;
    }

   
    private async void OnKopyalaClicked(object sender, EventArgs e)
    {
        
        await Clipboard.SetTextAsync(HexColor);

        
        await DisplayAlert("Kopyalandý", $"Renk kodu panoya kopyalandý: {HexColor}", "Tamam");
    }

   
    private void OnRastgeleRenkClicked(object sender, EventArgs e)
    {
       
        Red = _random.Next(0, 256);
        Green = _random.Next(0, 256);
        Blue = _random.Next(0, 256);

       
    }
}