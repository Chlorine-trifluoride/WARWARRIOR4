using System;
using System.ComponentModel.DataAnnotations;

namespace LeaderboardModel
{
    public class Player
    {
        [Key]
        public int id { get; set; }
        public string user_name { get; set; }
        public int country_code { get; set; }
    }
}
