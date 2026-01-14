using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Newtonsoft.Json;


/*API-KEY:   AIzaSyAvdvkOLelFb3DlikeWV7r2Q6Kj-Po8Whs  */

namespace Odev3.Services
{
    public class AuthService : IAuthService
    {
        private const string ApiKey = "AIzaSyAvdvkOLelFb3DlikeWV7r2Q6Kj-Po8Whs";

       
        private const string AuthDomain = "myfirstapp-api.firebaseapp.com";

        private readonly FirebaseAuthClient _authClient;

        public AuthService()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = ApiKey,
                AuthDomain = AuthDomain,
                Providers = new FirebaseAuthProvider[]
                {
                    
                    new EmailProvider()
                }
            };

            _authClient = new FirebaseAuthClient(config);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);

                
                if (userCredential.User != null)

                {
                    Application.Current.MainPage = new AppShell();

                    return "Success";
   
                }
                return "Giriş başarısız.";
            }
            catch (FirebaseAuthHttpException ex) 
            {
                // Hata detayını buradan alıyoruz
                var errorJson = ex.ResponseData;

                if (errorJson.Contains("INVALID_PASSWORD")) return "Hatalı şifre girdiniz.";
                if (errorJson.Contains("INVALID_EMAIL")) return "Böyle bir kullanıcı kayıtlı değil.";
                if (errorJson.Contains("USER_DISABLED")) return "Bu hesap engellenmiş.";
                if (errorJson.Contains("TOO_MANY_ATTEMPTS")) return "Çok fazla deneme yaptınız, biraz bekleyin.";

                return $"Sunucu Hatası: {errorJson}";
            }
            catch (Exception ex) 
            {
                return $"Bilinmeyen Hata: {ex.Message}";
            }
        }

        public async Task<string> RegisterAsync(string username, string email, string password)
        {

            try
            {
                if (password == null || password.Length < 6)
                {
                    return "Şifre en az 6 karakter olmalıdır.";
                }
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password);

                if (userCredential.User != null && !string.IsNullOrEmpty(username))
                {
                    await userCredential.User.ChangeDisplayNameAsync(username);
                }

                return "Kayıt Başarılı!";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS")) return "Bu email zaten kayıtlı.";
                if (ex.Message.Contains("WEAK_PASSWORD")) return "Şifre çok zayıf.";

                
                if (Clipboard.Default != null)
                {
                    await Clipboard.Default.SetTextAsync(ex.Message);
                }
                return $"Kayıt Hatası. Hata Kopyalandı!";
            }
        }
    }
}