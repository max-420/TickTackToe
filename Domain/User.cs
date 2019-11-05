using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class User : BaseEntity
	{
		public string Name { get; set; }

		public string PasswordHash { get; set; }

		public string Salt { get; set; }
	}
}
