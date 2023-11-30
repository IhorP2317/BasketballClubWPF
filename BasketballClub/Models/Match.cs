using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballClub.Models {
    public class Match {
        public int Id { get; set; }
        public string Location { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
