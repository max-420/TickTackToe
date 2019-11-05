using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
	public interface IJwtGenerator
	{
		string GetToken(int userId);
	}
}
