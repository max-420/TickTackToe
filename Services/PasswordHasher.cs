using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
	public class PasswordHasher : IPasswordHasher
	{
		public string GenerateSalt()
		{
			byte[] salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			return Convert.ToBase64String(salt);
		}

		public string Hash(string password, string salt)
		{
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: Convert.FromBase64String(salt),
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));

			return hashed;
		}

		public bool Verify(string password, string passwordHash, string salt)
		{
			return passwordHash == Hash(password, salt);
		}
	}
}
