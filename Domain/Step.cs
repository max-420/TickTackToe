using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class Step : BaseEntity
	{
		public PlayerType Player { get; set; }

		public int X { get; set; }

		public int Y { get; set; }
	}
}
