using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderboardAPI.Repositories;
using LeaderboardModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly LeaderboardRepository _leaderboardRepository;

        public PlayersController(LeaderboardRepository leaderboardRepository)
        {
            _leaderboardRepository = leaderboardRepository;
        }

        [HttpGet]
        public ActionResult<List<Player>> GetAllPlayers()
        {
            return Ok(_leaderboardRepository.GetAllPlayers());
        }

        [HttpPost]
        public ActionResult<Player> PostNewPlayer([FromBody] Player newPlayer)
        {
            Player createdPlayer = _leaderboardRepository.AddNewPlayer(newPlayer);
            return Created($"{Request.Path}/{createdPlayer.id}", createdPlayer);
        }
    }
}
