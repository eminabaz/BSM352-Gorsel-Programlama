using Odev3.ViewModels;

namespace Odev3.Views;

public partial class todo : ContentPage
{
    // Constructor (Yapýcý Metot)
    // Dependency Injection ile ViewModel'i içeri alýyoruz
    public todo(TodoViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    // Sayfa ekrana her geldiðinde çalýþýr
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // ViewModel'deki yükleme komutunu tetikle
        // Bu sayede yeni görev ekleyip geri döndüðünde liste güncellenir.
        if (BindingContext is TodoViewModel vm)
        {
            // Eðer zaten yükleniyorsa tekrar tetiklememek için kontrol edebilirsin
            // ama LoadDataAsync içinde IsBusy kontrolü olduðu için direkt çaðýrmak güvenlidir.
            vm.LoadDataCommand.Execute(null);
        }
    }
}