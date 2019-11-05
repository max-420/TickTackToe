using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class Game : BaseEntity
	{
		public bool IsFinished { get; set; }

		public ICollection<Step> Steps { get; set; }
	}
}
