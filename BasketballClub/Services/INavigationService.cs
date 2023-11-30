using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BasketballClub.Services
{
     public interface INavigationService {
         Frame? Frame { get; set; }
         void NavigateToPlayerCreationView();
         void NavigateToMainView();

    }
}
