using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev3.Services
{
    public interface IAuthService
    {
        Task<String> LoginAsync(string username, string password);
        Task<String> RegisterAsync(string email , string username, string password);
    }
}
