using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthServer
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : Controller
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member variables

		private readonly UserDbContext m_userDbContext = null;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructors

		public LoginController(UserDbContext userDbContext)
		{
			m_userDbContext = userDbContext;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		[HttpPost]
		public async Task<ResponseTemplate> Post(LoginTemplate template)
		{
			string sAccessToken = string.Empty; ;

			try
			{
				sAccessToken = await Task.Run<string>(() => m_userDbContext.Login(template.id));
			}
			catch
			{
				return new ResponseTemplate { isSuccess = false };
			}

			return new ResponseTemplate { isSuccess = true, response = sAccessToken };
		}
	}
}
