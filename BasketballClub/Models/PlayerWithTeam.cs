using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BasketballClub.Models
{
    public class PlayerWithTeam : INotifyPropertyChanged {
        private Player _player;
        public Player Player {
            get => _player;
            set {
                if (_player != value) {
                    _player = value;
                    OnPropertyChanged();
                }
            }
        }

        private Team _team;
        public Team Team {
            get => _team;
            set {
                if (_team != value) {
                    _team = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
