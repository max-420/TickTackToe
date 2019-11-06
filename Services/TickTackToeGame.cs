using System;
using System.Collections.Generic;
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
            return new CoordinatesDto { };
        }

        public PlayerType? TryGetWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                bool xFilled = true;
                bool yFilled = true;

                for (int j = 1; j < 3; j++)
                {
                    xFilled = xFilled && Matrix[i, j] != null && Matrix[i, j] == Matrix[i, j - 1];
                    yFilled = yFilled && Matrix[j, i] != null && Matrix[j, i] == Matrix[j - 1, i];
                    if (j == 3)
                    {
                        if (xFilled)
                        {
                            return Matrix[i, j];
                        }
                        if (yFilled)
                        {
                            return Matrix[j, i];
                        }
                    }
                }
            }

            bool xDiagFilled = true;
            bool yDiagFilled = true;

            for (int i = 1; i < 3; i++)
            {
                xDiagFilled = xDiagFilled && Matrix[i, i] != null && Matrix[i, i] == Matrix[i, i - 1];
                yDiagFilled = yDiagFilled && Matrix[i, 3 - i] != null && Matrix[i, 3 - i] == Matrix[i, 2 - i];
                if (i == 3)
                {
                    if (xDiagFilled)
                    {
                        return Matrix[i, i];
                    }
                    if (yDiagFilled)
                    {
                        return Matrix[i, 3 - i];
                    }
                }
            }
            return null;
        }
    }
}
