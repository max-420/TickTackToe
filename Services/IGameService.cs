using System;
using System.Collections.Generic;
using System.Text;
using Dto;

namespace Services
{
	public interface IGameService
    {
        StepDto StartGame();

        StepResultDto MakeStep(StepDto newStep);

		void EndGame(int gameId);
	}
}
