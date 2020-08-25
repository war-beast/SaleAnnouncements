using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SaleAnnouncements
{
	public class AuthOptions
	{
		public const string ISSUER = "SaleAnnouncementsAuthServer"; // издатель токена
		public const string AUDIENCE = "SaleAnnouncementsAuthClient"; // потребитель токена
		const string KEY = "zUhcDPQy-J6tmLHz ~Ul:KjYt ~UK@Tyidp9s^GHo-|";   // ключ для шифрации
		public const int LIFETIME = 7; // время жизни токена - 7 дней
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}
