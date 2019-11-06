using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Dto;

namespace Services
{
	public class TickTackToeGame
	{
		private readonly PlayerType?[,] Matrix;

		public TickTackToeGame()
		{
			Matrix = new PlayerType?[3, 3];
		}

		public TickTackToeGame(IEnumerable<Step> steps)
		{
			Matrix = ConvertToMatrix(steps);
		}

		private PlayerType?[,] ConvertToMatrix(IEnumerable<Step> steps)
		{
			var matrix = new PlayerType?[3, 3];
			foreach (var step in steps)
			{
				matrix[step.X, step.Y] = step.Player;
			}
			return matrix;
		}

		public void MakeStep(int x, int y, PlayerType player)
		{
			if (Matrix[x, y] != null)
			{
				throw new ArgumentException("This cell is filled");
			}

			Matrix[x, y] = player;
		}

		public CoordinatesDto MakeBotStep(PlayerType botPlayerType)
		{
			var winCoordsBot = GetWinCoords(botPlayerType);
			if (winCoordsBot != null)
			{
				Matrix[winCoordsBot.X, winCoordsBot.Y] = botPlayerType;
				return winCoordsBot;
			}

			var winCoordsUser = GetWinCoords(botPlayerType == PlayerType.X ? PlayerType.O : PlayerType.X);
			if (winCoordsUser != null)
			{
				Matrix[winCoordsUser.X, winCoordsUser.Y] = botPlayerType;
				return winCoordsUser;
			}

			var emptyCells = GetEmptyCells();
			var randCell = emptyCells[new Random().Next(0, emptyCells.Count)];
			Matrix[randCell.X, randCell.Y] = botPlayerType;

			return randCell;
		}

		private CoordinatesDto GetWinCoords(PlayerType player)
		{
			for (int i = 0; i < 3; i++)
			{
				bool xFilled = true;
				bool yFilled = true;
				CoordinatesDto xEmptyCell = null;
				CoordinatesDto yEmptyCell = null;

				for (int j = 0; j < 3; j++)
				{
					xFilled = xFilled && ((xEmptyCell == null && Matrix[i, j] == null) || Matrix[i, j] == player);
					if (Matrix[i, j] == null)
					{
						xEmptyCell = new CoordinatesDto
						{
							X = i,
							Y = j
						};
					}

					yFilled = yFilled && ((yEmptyCell == null && Matrix[j, i] == null) || Matrix[j, i] == player);
					if (Matrix[j, i] == null)
					{
						yEmptyCell = new CoordinatesDto
						{
							X = j,
							Y = i
						};
					}

					if (j == 2)
					{
						if (xFilled)
						{
							return xEmptyCell;
						}
						if (yFilled)
						{
							return yEmptyCell;
						}
					}
				}
			}

			bool xDiagFilled = true;
			bool yDiagFilled = true;
			CoordinatesDto xDiagEmptyCell = null;
			CoordinatesDto yDiagEmptyCell = null;

			for (int i = 0; i < 3; i++)
			{
				xDiagFilled = xDiagFilled && ((xDiagEmptyCell == null && Matrix[i, i] == null) || Matrix[i, i] == player);
				if (Matrix[i, i] == null)
				{
					xDiagEmptyCell = new CoordinatesDto
					{
						X = i,
						Y = i
					};
				}

				yDiagFilled = yDiagFilled && ((yDiagEmptyCell == null && Matrix[i, 2 - i] == null) || Matrix[i, 2 - i] == player);
				if (Matrix[i, 2 - i] == null)
				{
					yDiagEmptyCell = new CoordinatesDto
					{
						X = i,
						Y = 2 - i
					};
				}

				if (i == 2)
				{
					if (xDiagFilled)
					{
						return xDiagEmptyCell;
					}
					if (yDiagFilled)
					{
						return yDiagEmptyCell;
					}
				}
			}
			return null;
		}

		private IList<CoordinatesDto> GetEmptyCells()
		{
			var coordsArray = new List<CoordinatesDto>();
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (Matrix[i, j] == null)
					{
						coordsArray.Add(new CoordinatesDto
						{
							X = i,
							Y = j,
						});
					}
				}
			}
			return coordsArray;
		}

		public WinnerDto TryGetWinner()
		{
			for (int i = 0; i < 3; i++)
			{
				bool xFilled = true;
				bool yFilled = true;

				for (int j = 1; j < 3; j++)
				{
					xFilled = xFilled && Matrix[i, j] != null && Matrix[i, j] == Matrix[i, j - 1];
					yFilled = yFilled && Matrix[j, i] != null && Matrix[j, i] == Matrix[j - 1, i];
					if (j == 2)
					{
						if (xFilled)
						{
							return new WinnerDto { Winner = Matrix[i, j] };
						}
						if (yFilled)
						{
							return new WinnerDto { Winner = Matrix[j, i] };
						}
					}
				}
			}

			bool xDiagFilled = true;
			bool yDiagFilled = true;

			for (int i = 1; i < 3; i++)
			{
				xDiagFilled = xDiagFilled && Matrix[i, i] != null && Matrix[i, i] == Matrix[i - 1, i - 1];
				yDiagFilled = yDiagFilled && Matrix[i, 2 - i] != null && Matrix[i, 2 - i] == Matrix[i - 1, 3 - i];
				if (i == 2)
				{
					if (xDiagFilled)
					{
						return new WinnerDto { Winner = Matrix[i, i] };
					}
					if (yDiagFilled)
					{
						return new WinnerDto { Winner = Matrix[i - 1, 3 - i] };
					}
				}
			}
			return new WinnerDto { IsDraw = !GetEmptyCells().Any() };
		}
	}
}
