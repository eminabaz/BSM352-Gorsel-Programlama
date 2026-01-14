using System;
using System.Windows.Input;
using Odev3.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev3.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        private string _email;
        private string _username;
        private string _password;
        private string _loginMessage;
        private string _pageTitle;
        private string _buttonText;
        private string _toggleLinkText;

        private bool _isBusy;
        private bool _isRegisterMode;
        public bool IsLoginMode => !IsRegisterMode;


        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;

            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await RegisterAsync());
            ToggleModeCommand = new Command(ToggleMode);

            
            UpdateUI();
        }


        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                    _email = value;
                OnPropertyChanged();
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)

                    _username = value;
                OnPropertyChanged();
            }
        }
        

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                    _password = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                    _isBusy = value;
                OnPropertyChanged();
            }
        }

        public string LoginMessage
        {
            get => _loginMessage;
            set
            {
                if (_loginMessage != value)
                    _loginMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsRegisterMode
        {
            get => _isRegisterMode;
            set
            {
                if (_isRegisterMode != value)
                {
                    _isRegisterMode = value;
                    OnPropertyChanged();

                    OnPropertyChanged(nameof(IsLoginMode));
                    UpdateUI(); // Mod değişince yazıları da güncelle
                }
            }
        }

        // Başlık: "Oturum Aç" veya "Kaydol"
        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                if (_pageTitle != value)
                {
                    _pageTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        // Buton Yazısı: "OTURUM AÇ" veya "KAYDOL"
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged();
                }
            }
        }

        // Alt Link Yazısı: "Hesabım Yok" veya "Zaten bir hesabım var"
        public string ToggleLinkText
        {
            get => _toggleLinkText;
            set
            {
                if (_toggleLinkText != value)
                {
                    _toggleLinkText = value;
                    OnPropertyChanged();
                }
            }
        }

        // Komut Tanımı
        public ICommand ToggleModeCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private void ToggleMode()
        {
            // True ise False, False ise True yapar (Tersi)
            IsRegisterMode = !IsRegisterMode;
        }

        // Yazıları moda göre güncelleyen yardımcı fonksiyon
        private void UpdateUI()
        {
            if (IsRegisterMode)
            {
                // KAYIT MODU
                PageTitle = "Kaydol";
                ButtonText = "Kaydol";
                ToggleLinkText = "Zaten bir hesabım var"; // Giriş ekranına dönmek için
            }
            else
            {
                // GİRİŞ MODU
                PageTitle = "Oturum Aç";
                ButtonText = "Oturum Aç";
                ToggleLinkText = "Hesabım Yok"; // Kayıt ekranına gitmek için
            }
        }

        private async Task LoginAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            LoginMessage = string.Empty;
            try
            {
                var result = await _authService.LoginAsync(Email, Password);
                LoginMessage = result;
            }
            catch (Exception ex)
            {
                LoginMessage = $"Login failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RegisterAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            LoginMessage = string.Empty;
            try
            {
                var result = await _authService.RegisterAsync( Username, Email, Password);
                LoginMessage = result;
            }
            catch (Exception ex)
            {
                LoginMessage = $"Registration failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }












    }
}