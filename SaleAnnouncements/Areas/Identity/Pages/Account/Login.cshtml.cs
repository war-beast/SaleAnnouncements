using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SaleAnnouncements.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class LoginModel : PageModel
	{
		public void OnGet()
		{

		}
	}
}
