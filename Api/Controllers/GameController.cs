using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Route("api/game"), HttpPost]
        [AllowAnonymous]
        public IActionResult Create()
        {
            var result = _gameService.StartGame();

            return Ok(result);
        }

        [Route("api/game"), HttpPut]
        [AllowAnonymous]
        public IActionResult MakeStep(StepDto newStep)
        {
            var result = _gameService.MakeStep(newStep);

            return Ok(result);
        }

		[Route("api/game/end"), HttpPut]
		[AllowAnonymous]
		public IActionResult EndGame([FromBody]int gameId)
		{
			_gameService.EndGame(gameId);

			return Ok();
		}
	}
}