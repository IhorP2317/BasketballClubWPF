using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BasketballClub.Controls.Dialogs;
using BasketballClub.Exceptions;
using BasketballClub.Models;
using BasketballClub.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BasketballClub.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public ICommand DeletePlayerCommand { get; private set; }
        public ICommand OpenUpdatePlayerCommand { get; private set; }
        private readonly INavigationService navigationService;
        private readonly IPlayerService playerService;
        private readonly ITeamService teamService;

        private ObservableCollection<PlayerWithTeam> players = new ObservableCollection<PlayerWithTeam>();

        public ObservableCollection<PlayerWithTeam> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        private ObservableCollection<Team> teams = new ObservableCollection<Team>();

        public ObservableCollection<Team> Teams
        {
            get => teams;
            set => SetProperty(ref teams, value);
        }

        public MainViewModel(INavigationService navigationService, IPlayerService playerService,
            ITeamService teamService)
        {
            this.navigationService = navigationService;
            this.playerService = playerService;
            this.teamService = teamService;
            InitializeCommands();
        }

        public async Task InitializeAsync()
        {
            await LoadPlayersWithTeamsAsync();
        }

        private async Task LoadPlayersWithTeamsAsync()
        {
            try
            {
                var fetchedPlayers = await playerService.GetAllPlayersAsync();
                var fetchedTeams = await teamService.GetAllTeamsAsync();

                var playersWithTeams = fetchedPlayers.Select(player =>
                {
                    var team = fetchedTeams.FirstOrDefault(t => t.Id == player.TeamId);
                    return new PlayerWithTeam { Player = player, Team = team };
                });

                Players = new ObservableCollection<PlayerWithTeam>(playersWithTeams);
                Teams = new ObservableCollection<Team>(fetchedTeams);
            }
            catch (ApiException ex)
            {
                // Handle the ApiException (e.g., show an error message)
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void InitializeCommands()
        {
            DeletePlayerCommand = new AsyncRelayCommand<int>(DeletePlayerAsync);
            OpenUpdatePlayerCommand = new AsyncRelayCommand< Player>(ShowUpdatePlayerDialogAsync);
        }

        private async Task DeletePlayerAsync(int id)
        {
            int index = Players.Select(P => P.Player).ToList().FindIndex(s => s.Id == id);
            if (index == -1) return;

            var confirmationDialog = new ConfirmationDialog();
            var result = await confirmationDialog.ShowAsync(
                $"Are you sure you want to delete  {Players[index].Player.FirstName} {Players[index].Player.LastName} ?");

            if (result == ConfirmationDialogResult.Confirmed)
            {
                Players.RemoveAt(index);

                try
                {
                    await playerService.DeletePlayerAsync(id);
                }
                catch (ApiException ex)
                {
                    // Handle the ApiException (e.g., show an error message)
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    // Handle other unexpected exceptions
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        public async Task ShowUpdatePlayerDialogAsync(Player player) {
            var updateDialog = new UpdatePlayerDialog();
            var result = await updateDialog.ShowAsync(player);

            if (result.IsConfirmed) {
                try
                {
                    await playerService.UpdatePlayerAsync(result.UpdatedPlayer.Id, result.UpdatedPlayer);
                    int index = Players.Select(P => P.Player).ToList().FindIndex(s => s.Id == result.UpdatedPlayer.Id);
                if (index == -1) return;
              
                
                Team teamOfUpdatedPlayer =  await teamService.GetTeamByIdAsync((int)(result.UpdatedPlayer.TeamId));

                PlayerWithTeam updatedPlayerWithTeam = new PlayerWithTeam();
                updatedPlayerWithTeam.Player = result.UpdatedPlayer;
                updatedPlayerWithTeam.Team = teamOfUpdatedPlayer;

                Players.RemoveAt(index);
                Players.Insert(index, updatedPlayerWithTeam);

                   
                    } catch (ApiException ex) {
                        // Handle the ApiException (e.g., show an error message)
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    } catch (Exception ex) {
                        // Handle other unexpected exceptions
                        MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
        }
    }

