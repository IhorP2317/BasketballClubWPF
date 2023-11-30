using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballClub.Models {
    public class UpdateConfirmationDialogResult {
        public bool IsConfirmed { get; private set; }
        public Player UpdatedPlayer { get; private set; }

        private UpdateConfirmationDialogResult(bool isConfirmed, Player updatedPlayer = null) {
            IsConfirmed = isConfirmed;
            UpdatedPlayer = updatedPlayer;
        }

        public static UpdateConfirmationDialogResult Confirmed(Player updatedPlayer) {
            return new UpdateConfirmationDialogResult(true, updatedPlayer);
        }

        public static UpdateConfirmationDialogResult Cancelled() {
            return new UpdateConfirmationDialogResult(false);
        }
    }
}
