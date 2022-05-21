﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.Logic.Registrations;

namespace Project.Interface
{
    public partial class PlayersPage : Page
    {
        private TeamPage _team;
        public PlayersPage(TeamPage team)
        {
            InitializeComponent();
            _team = team;
        }

        public Player[] getPlayers()
        {
            if (isEmpty()) throw new Exception("No players names");
            Player[] players = {
                new Player(addCaptain.Text, addCaptainS.Text),
                new Player(player1.Text, player1S.Text),
                new Player(player2.Text, player2S.Text),
                new Player(player3.Text, player3S.Text),
                new Player(player4.Text, player4S.Text),
            };
            return players;
        }

        public void clearBoxes()
        {
            addCaptain.Text = "";
            addCaptainS.Text = "";
            player1.Text = "";
            player1S.Text = "";
            player2.Text = "";
            player2S.Text = "";
            player3.Text = "";
            player3S.Text = "";
            player4.Text = "";
            player4S.Text = "";
        }

        private bool isEmpty()
        {
            if (addCaptain.Text.Trim() == String.Empty || addCaptainS.Text.Trim() == String.Empty || player1.Text.Trim() == String.Empty || player1S.Text.Trim() == String.Empty || player2.Text.Trim() == String.Empty || player2S.Text.Trim() == String.Empty || player3.Text.Trim() == String.Empty || player3S.Text.Trim() == String.Empty || player4.Text.Trim() == String.Empty || player4S.Text.Trim() == String.Empty)
            {
                return true;
            }
            else return false;
        }

        private void createTeamButton(object sender, RoutedEventArgs e)
        {
            if(isEmpty())
            {
                MessageBox.Show("Wprowadź dane");
            }
            else NavigationService.Navigate(_team);
        }
    }
}