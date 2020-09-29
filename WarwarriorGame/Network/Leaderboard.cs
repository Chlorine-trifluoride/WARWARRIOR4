using LeaderboardModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WarwarriorGame.Network
{
    static class Leaderboard
    {
        private const string API_PATH = "https://localhost:5001/api";

        static HttpClient httpClient = new HttpClient();

        public static async Task<List<Score>> LoadAllScores()
        {
            string path = $"{API_PATH}/scores";

            using (var response = await httpClient.GetAsync(path))
            {
                if (!response.IsSuccessStatusCode)
                    return null;

                string apiResponse = await response.Content.ReadAsStringAsync();
                List<Score> scores = JsonSerializer.Deserialize<List<Score>>(apiResponse);

                return scores;
            }
        }

        public static async Task SendScore(int seconds, int highScore, int levelId, string name)
        {
            string path = $"{API_PATH}/scores/level/{levelId}";

            Score score = new Score
            {
                time_stamp = DateTime.Now,
                high_score = highScore,
                levelid = levelId,

                player = new LeaderboardModel.Player
                {
                    country_code = 1,
                    user_name = name
                },

                time_in_seconds = seconds
            };

            string data = JsonSerializer.Serialize<Score>(score);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(path, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"ApiResponse: {apiResponse}");
            }
        }
    }
}
