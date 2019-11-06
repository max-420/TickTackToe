using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Dto;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class GameService : IGameService
    {
        private readonly DbContext _dbContext;

        public GameService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public StepDto StartGame()
        {
			var openGamesCount = _dbContext
				.Set<Game>()
				.Where(x => !x.IsFinished)
				.Count();

			if (openGamesCount >= 3)
			{
				throw new Exception("Too many game sessions");
			}

            var userPlayerType = (PlayerType)new Random().Next(0, 2);
            var game = new Game
            {
                UserPlayerType = userPlayerType,
            };

            CoordinatesDto newStepCoordinates = null;
            if (userPlayerType == PlayerType.O)
            {
                var tickTackToeGame = new TickTackToeGame();
                newStepCoordinates = tickTackToeGame.MakeBotStep(PlayerType.X);
                var step = new Step()
                {
                    Player = PlayerType.X,
                    X = newStepCoordinates.X,
                    Y = newStepCoordinates.Y,
                };
                game.Steps = new List<Step>() { step };
            }

            _dbContext.Add(game);
            _dbContext.SaveChanges();

            return new StepDto
            {
                GameId = game.Id,
                Coordinates = newStepCoordinates
            };
        }

        public StepResultDto MakeStep(StepDto newStep)
        {
            var game = _dbContext.Set<Game>()
                .Where(x => x.Id == newStep.GameId)
                .Include(x => x.Steps)
                .First();

            var tickTackToeGame = new TickTackToeGame(game.Steps);
            tickTackToeGame.MakeStep(newStep.Coordinates.X, newStep.Coordinates.Y, game.UserPlayerType);
            var winnerDto = tickTackToeGame.TryGetWinner();
			var step = new Step()
			{
				GameId = game.Id,
				Player = game.UserPlayerType,
				X = newStep.Coordinates.X,
				Y = newStep.Coordinates.Y,
			};
			_dbContext.Add(step);

			CoordinatesDto newStepCoordinates = null;
            if (winnerDto.Winner == null && !winnerDto.IsDraw)
            {
				var botPlayerType = game.UserPlayerType == PlayerType.X ? PlayerType.O : PlayerType.X;

				newStepCoordinates = tickTackToeGame.MakeBotStep(botPlayerType);
                winnerDto = tickTackToeGame.TryGetWinner();

                var botStep = new Step()
                {
                    GameId = game.Id,
                    Player = botPlayerType,
                    X = newStepCoordinates.X,
                    Y = newStepCoordinates.Y,
                };
                _dbContext.Add(botStep);
            }

            game.Winner = winnerDto.Winner;
			game.IsFinished = winnerDto.Winner != null || winnerDto.IsDraw;


			_dbContext.SaveChanges();

            return new StepResultDto
            {
                Winner = winnerDto.Winner,
                BotStep = newStepCoordinates,
				IsFinished = game.IsFinished
			};
        }

		public void EndGame(int gameId)
		{
			var game = _dbContext.Set<Game>()
				.Where(x => x.Id == gameId)
				.First();

			game.IsFinished = true;

			_dbContext.SaveChanges();
		}
    }
}
