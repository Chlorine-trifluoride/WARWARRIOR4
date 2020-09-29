using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeaderboardModel
{
    class Score
    {
        [Key]
        public int id { get; set; }
        public DateTime time_stamp { get; set; }
        public int time_in_seconds { get; set; }
        public int high_score { get; set; }
        public Player player_id { get; set; }
        public int level_id { get; set; }
    }
}
