using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeaderboardModel
{
    class Level
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
