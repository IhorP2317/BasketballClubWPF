using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballClub.Exceptions {
    public class ApiErrorDetails {
        public string Title { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
