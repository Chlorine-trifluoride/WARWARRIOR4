using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeaderboardModel
{
    public class Score
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime time_stamp { get; set; }
        [Required]
        public int time_in_seconds { get; set; }
        [Required]
        public int high_score { get; set; }
        [Required]
        public Player player { get; set; }
        public Level level { get; set; }
        [ForeignKey("levelid")]
        public int levelid { get; set; }
    }
}
