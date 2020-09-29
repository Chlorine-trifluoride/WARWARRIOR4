using LeaderboardAPI.Contexts;
using LeaderboardModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace LeaderboardAPI.Repositories
{
    public class LeaderboardRepository
    {
        private readonly LeaderboardContext _leaderboardContext;

        public LeaderboardRepository(LeaderboardContext leaderboardContext)
        {
            _leaderboardContext = leaderboardContext;
        }

        public List<Score> GetScoresForLevelId(int levelId)
        {
            return _leaderboardContext.Scores
                .Include(x => x.level)
                .Include(x => x.player)
                .Where(x => x.level.id == levelId).ToList();
        }

        public List<Score> GetAllScores()
        {
            return _leaderboardContext.Scores
                .Include(x => x.level)
                .Include(x => x.player).ToList();
        }

        public List<Player> GetAllPlayers()
        {
            return _leaderboardContext.Players.ToList();
        }

        public bool AddNewScore(int levelId, Score score)
        {
            _leaderboardContext.Scores.Add(score);
            _leaderboardContext.SaveChanges();

            return true;
        }

        public Player AddNewPlayer(Player newPlayer)
        {
            Player p = _leaderboardContext.Players.Add(newPlayer).Entity;
            _leaderboardContext.SaveChanges();
            return p;
        }

        public bool DeleteScoreById(int scoreId)
        {
            Score score = _leaderboardContext.Scores.FirstOrDefault(x => x.id == scoreId);

            if (score is null)
                return false;

            _leaderboardContext.Scores.Remove(score);
            _leaderboardContext.SaveChanges();

            return true;
        }
    }
}
