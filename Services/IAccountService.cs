using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
	public interface IAccountService
	{
		void CreateAccount(CredentialsDto credentials);

		string GetToken(CredentialsDto credentials);
	}
}
