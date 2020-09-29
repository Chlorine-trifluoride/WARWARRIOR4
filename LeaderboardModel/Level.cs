using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeaderboardModel
{
    public class Level
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
