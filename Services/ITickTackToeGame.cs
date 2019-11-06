using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Dto;

namespace Services
{
    public interface ITickTackToeGame
    {
        PlayerType? MakeStep(StepDto newStep);
    }
}
