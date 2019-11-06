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
            var winner = tickTackToeGame.TryGetWinner();

            CoordinatesDto newStepCoordinates = null;
            if (winner == null)
            {
                newStepCoordinates = tickTackToeGame.MakeBotStep(game.UserPlayerType == PlayerType.X ? PlayerType.O : PlayerType.X);
                winner = tickTackToeGame.TryGetWinner();

                var step = new Step()
                {
                    GameId = game.Id,
                    Player = PlayerType.X,
                    X = newStepCoordinates.X,
                    Y = newStepCoordinates.Y,
                };
                _dbContext.Add(step);
            }

            game.Winner = winner;

            _dbContext.SaveChanges();

            return new StepResultDto
            {
                Winner = winner,
                BotStep = newStepCoordinates,
            };
        }
    }
}
