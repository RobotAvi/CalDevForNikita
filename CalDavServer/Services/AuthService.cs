using System.Linq;
using CalDavServer.Data;
using CalDavServer.Models;

namespace CalDavServer.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _db;
        public AuthService(ApplicationDbContext db)
        {
            _db = db;
        }
        public string Authenticate(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return null;
            // В реальном проекте сравнивать хеш пароля
            if (user.PasswordHash != password) return null;
            // Здесь можно генерировать JWT или другой токен
            return "dummy-token";
        }
    }
}