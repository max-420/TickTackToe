using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
	public interface IPasswordHasher
	{
		string GenerateSalt();

		string Hash(string password, string salt);

		bool Verify(string password, string passwordHash, string salt);
	}
}
