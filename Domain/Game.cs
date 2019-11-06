using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class Game : BaseEntity
	{
        public PlayerType UserPlayerType { get; set; }

        public PlayerType? Winner { get; set; }

		public ICollection<Step> Steps { get; set; }
	}
}
