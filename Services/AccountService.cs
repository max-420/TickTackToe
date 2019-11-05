using System;
using System.Linq;
using System.Security;
using Domain;
using Dto;
using Microsoft.EntityFrameworkCore;

namespace Services
{
	public class AccountService : IAccountService
	{
		private readonly DbContext _dbContext;
		private readonly IJwtGenerator _jwtGenerator;
		private readonly IPasswordHasher _passwordHasher;

		public AccountService(DbContext dbContext,
			IJwtGenerator jwtGenerator,
			IPasswordHasher passwordHasher)
		{
			_dbContext = dbContext;
			_jwtGenerator = jwtGenerator;
			_passwordHasher = passwordHasher;
		}

		public void CreateAccount(CredentialsDto credentials)
		{
			if(_dbContext.Set<User>().Any(x => x.Name == credentials.Login))
			{
				throw new ArgumentException();
			}
			var salt = _passwordHasher.GenerateSalt();
			var hash = _passwordHasher.Hash(credentials.Password, salt);

			var user = new User
			{
				Name = credentials.Login,
				Salt = salt,
				PasswordHash = hash
			};

			_dbContext.Set<User>().Add(user);
			_dbContext.SaveChanges();
		}

		public string GetToken(CredentialsDto credentials)
		{
			var user = _dbContext.Set<User>().First(x => x.Name == credentials.Login);

			if (user == null || !_passwordHasher.Verify(credentials.Password, user.PasswordHash, user.Salt))
			{
				throw new SecurityException();
			}

			var jwt = _jwtGenerator.GetToken(user.Id);
			return jwt;
		}
	}
}
