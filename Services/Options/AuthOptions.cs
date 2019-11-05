using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Options
{
	public class AuthOptions
	{
		public string Issuer { get; set; }

		public string Audience { get; set; }

		public string Key { get; set; }

		public TimeSpan AccessTokenLifetime { get; set; }
	}
}
