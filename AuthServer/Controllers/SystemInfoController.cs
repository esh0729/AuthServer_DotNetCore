using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthServer
{
	[Route("auth/[controller]")]
	[ApiController]
	public class SystemInfoController : Controller
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member variables

		private readonly UserDbContext m_userDbContext;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructors

		public SystemInfoController(UserDbContext userDbContext)
		{
			m_userDbContext = userDbContext;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		[HttpGet]
		public async Task<ResponseTemplate> Get()
		{
			string sResponse = string.Empty;

			try
			{
				sResponse = await Task.Run<string>(() => m_userDbContext.SystemInfo());
			}
			catch (Exception ex)
			{
				return new ResponseTemplate { returnCode = 1, error = ex.Message };
			}

			return new ResponseTemplate { returnCode = 0, response = sResponse };
		}
	}
}
