using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballClub.Models {
    public class Player {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
       
        public string Country { get; set; }
     
        public double Height { get; set; }
        
        public double Weight { get; set; }
      
        public string Position { get; set; }
        
        public int? TeamId { get; set; }
    }
}
