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
    public class ScoresController : ControllerBase
    {
        private readonly LeaderboardRepository _leaderboardRepository;

        public ScoresController(LeaderboardRepository leaderboardRepository)
        {
            _leaderboardRepository = leaderboardRepository;
        }

        [HttpGet]
        public ActionResult<List<Score>> GetAllScores()
        {
            return Ok(_leaderboardRepository.GetAllScores());
        }

        [HttpGet("level/{levelId:int}")]
        public ActionResult<List<Score>> GetScoresForLevelId(int levelId)
        {
            return Ok(_leaderboardRepository.GetScoresForLevelId(levelId));
        }

        [HttpPost("level/{levelId:int}")]
        public IActionResult PostNewScore(int levelId, [FromBody] Score score)
        {
            if (_leaderboardRepository.AddNewScore(levelId, score))
                return Created($"Request.Path", score);

            return BadRequest();
        }

        [HttpDelete("{scoreId:int}")]
        public IActionResult DeleteScore(int scoreId)
        {
            if (_leaderboardRepository.DeleteScoreById(scoreId))
                return NoContent();

            return NotFound();
        }
    }
}
