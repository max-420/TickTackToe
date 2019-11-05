using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    public class UserController : ControllerBase
    {
		private readonly IAccountService _accountService;

		public UserController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[Route("api/users"), HttpPost]
		[AllowAnonymous]
		public IActionResult Create(CredentialsDto credentials)
        {
			_accountService.CreateAccount(credentials);
			return Ok();
		}

		[Route("api/users/login"), HttpPost]
		[AllowAnonymous]
		public IActionResult Login(CredentialsDto credentials)
		{
			var token = _accountService.GetToken(credentials);
			return Ok(token);
		}
	}
}