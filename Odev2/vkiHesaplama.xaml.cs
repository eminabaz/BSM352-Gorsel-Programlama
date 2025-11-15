using System.ComponentModel;

namespace Odev2;

public partial class vkiHesaplama : ContentPage, INotifyPropertyChanged
{
   
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    
    private double _kilo = 70; 
    public double Kilo
    {
        get => _kilo;
        set
        {
            if (_kilo != value)
            {
                _kilo = value;
                OnPropertyChanged(nameof(Kilo));
                HesaplaVki();
            }
        }
    }

    private double _boy = 170; 
    public double Boy
    {
        get => _boy;
        set
        {
            if (_boy != value)
            {
                _boy = value;
                OnPropertyChanged(nameof(Boy));
                HesaplaVki();
            }
        }
    }

    private double _vkiDegeri;
    public double VkiDegeri
    {
        get => _vkiDegeri;
        set
        {
            if (_vkiDegeri != value)
            {
                _vkiDegeri = value;
                OnPropertyChanged(nameof(VkiDegeri));
            }
        }
    }

    private string _vkiYorumu = "vücut deðerlerinizi Girin!";
    public string VkiYorumu
    {
        get => _vkiYorumu;
        set
        {
            if (_vkiYorumu != value)
            {
                _vkiYorumu = value;
                OnPropertyChanged(nameof(VkiYorumu));
            }
        }
    }

    public vkiHesaplama()
    {
        InitializeComponent();
        this.BindingContext = this;
        HesaplaVki();
    }

    private void HesaplaVki()
    {
        if (Kilo == 0 || Boy < 100)
        {
            VkiDegeri = 0;
            VkiYorumu = "Geçerli Boy ve Kilo Giriniz";
            return;
        }

        double boyMetre = Boy / 100.0;

        
        double vki = Kilo / (boyMetre * boyMetre);

       
        VkiDegeri = vki;


        if (vki < 16)
        {
            VkiYorumu = "Ýleri Düzeyde Zayýf";
        }
        else if (vki >= 16 && vki <= 16.99)
        {
            VkiYorumu = "Orta Düzeyde Zayýf";
        }
        else if (vki >= 17 && vki <= 18.49)
        {
            VkiYorumu = "Hafif Düzeyde Zayýf";
        }
        else if (vki >= 18.50 && vki <= 24.9)
        {
            VkiYorumu = "Normal Kilolu";
        }
        else if (vki >= 25 && vki <= 29.99)
        {
            VkiYorumu = "Hafif Þiþman / Fazla Kilolu";
        }
        else if (vki >= 30 && vki <= 34.99)
        {
            VkiYorumu = "1. Derecede Obez";
        }
        else if (vki >= 35 && vki <= 39.99)
        {
            VkiYorumu = "2. Derecede Obez";
        }
        else // vki >= 40
        {
            VkiYorumu = "3. Derecede Obez / Morbid Obez";
        }
    }
}
