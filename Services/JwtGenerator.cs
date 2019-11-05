using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Services
{
	public class JwtGenerator : IJwtGenerator
	{
		private readonly AuthOptions _authOptions;

		public JwtGenerator(IOptions<AuthOptions> authOptions)
		{
			_authOptions = authOptions.Value;
		}

		public string GetToken(int userId)
		{
			var handler = new JwtSecurityTokenHandler();

			var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Key));
			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var identity = new ClaimsIdentity(new GenericIdentity(userId.ToString()));

			var token = handler.CreateJwtSecurityToken(subject: identity,
				signingCredentials: signingCredentials,
				audience: _authOptions.Audience,
				issuer: _authOptions.Issuer,
				expires: DateTime.UtcNow.Add(_authOptions.AccessTokenLifetime));

			return handler.WriteToken(token);
		}
	}
}
