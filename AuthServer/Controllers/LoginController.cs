﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthServer
{
	[Route("auth/[controller]")]
	[ApiController]
	public class LoginController : Controller
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member variables

		private readonly UserDbContext m_userDbContext;

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
			string sResponse = string.Empty; ;

			try
			{
				sResponse = await Task.Run<string>(() => m_userDbContext.Login(template.id));
			}
			catch
			{
				return new ResponseTemplate { isSuccess = false };
			}

			return new ResponseTemplate { isSuccess = true, response = sResponse };
		}
	}
}
