using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
	public class WinnerDto
	{
		public PlayerType? Winner { get; set; }

		public bool IsDraw { get; set; }
	}
}
