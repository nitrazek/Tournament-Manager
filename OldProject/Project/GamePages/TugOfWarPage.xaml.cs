﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Collections.Generic;
using Project.Registrations;

namespace Project.GamePages
{
    public partial class TugOfWarPage : Page
    {
        private MenuPage _menu;
        private Content content;
        public TugOfWarPage(MenuPage menu)
        {
            InitializeComponent();
            _menu = menu;
        }

        //Odświeżanie tablicy drużyn i wyników
        //Odświeżanie tablicy drużyn i wyników
        public void refreshPoints()
        {
            int lp = 1;
            string place = "";
            string names = "";
            string points = "";
            List<Team> teams = _menu.tugOfWar.getTeams();
            teams.ForEach(team => { names += team.getName() + "\n"; points += team.getScore() + "\n"; place += lp + "\n"; lp += 1; });
            teamPlace.Text = place;
            teamName.Text = names;
            teamPoints.Text = points;
        }

        //Przycisk powrotu do menu
        private void GoBackButton(object sender, RoutedEventArgs e)
        {
            if (content != null) content.Close();
            NavigationService.Navigate(_menu);
        }

        //Przyciski do wywoływania metod przeprowadzających turniej
        private void ShowTeams_Button(object sender, RoutedEventArgs e)
        {
            if (content != null) content.Close();
            if (_menu.tugOfWar.getTeams().Count <= 4)
            {
                MessageBox.Show("Za mało drużyn minimalna liczba to 5", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            HEl.IsEnabled = true;
            HRo.IsEnabled = false;
            _menu.refreshTables();
        }
        private void Elimination_Button(object sender, RoutedEventArgs e)
        {
            HCw.IsEnabled = true;
            HEl.IsEnabled = false;

            string score = _menu.tugOfWar.playElimination();
            content = new Content(score);
            content.Show();
            _menu.refreshTables();
        }
        private void Semi_Button(object sender, RoutedEventArgs e)
        {
            HFi.IsEnabled = true;
            HCw.IsEnabled = false;

            string score = _menu.tugOfWar.playSemiFinal();
            content.Close();
            content = new Content(score);
            content.Show();
            _menu.refreshTables();

        }
        private void Final_Button(object sender, RoutedEventArgs e)
        {
            HFi.IsEnabled = false;
            HRo.IsEnabled = true;

            string score = _menu.tugOfWar.playFinal();
            content.Close();
            content = new Content(score);
            content.Show();
            _menu.refreshTables();
        }
    }
}
