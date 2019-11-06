using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class StepResultDto
    {
        public CoordinatesDto BotStep { get; set; }

        public PlayerType? Winner { get; set; }

		public bool IsFinished { get; set; }
    }
}
