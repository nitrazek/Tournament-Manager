﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Win32;
using Project.RegistrationPages;
using Project.Registrations;
using Project.Games;
using Project.GamePages;

namespace Project
{
    public partial class MenuPage : Page
    {
        private Window mainWindow;
        private TeamPage teamPage;
        private LoadPage loadPage;
        private VolleyballPage volleyballPage;
        private TugOfWarPage tugOfWarPage;

        public Volleyball volleyball = new Volleyball();
        public Tug_of_war tugOfWar = new Tug_of_war();
        public MenuPage(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            teamPage = new TeamPage(this);
            loadPage = new LoadPage(this);
            volleyballPage = new VolleyballPage(this);
            tugOfWarPage = new TugOfWarPage(this);
        }

        public static void load(string? fname, Volleyball volleyball, Tug_of_war tugOfWar)
        {
            //Najpierw sprawdzamy czy nazwa pliku jest dobra i czy taki plik istnieje
            if (fname == null || fname.Length == 0 || !File.Exists($@"..\..\..\saved\{fname}.txt"))
            {
                Console.WriteLine("Niepoprawna nazwa pliku");
                return;
            }

            //Usuwamy to co było wcześniej w listach
            volleyball.clearTeams();
            volleyball.clearJudges();
            tugOfWar.clearTeams();
            tugOfWar.clearJudges();

            //Póki co tylko dla siatkówki, sprawdzamy czy dana linijka to drużyna czy sędzia, potem jaki sport, i dodajemy do odpowiedniej listy
            StreamReader loadStream = new StreamReader($@"..\..\..\saved\{fname}.txt");
            while (!loadStream.EndOfStream)
            {
                string[] dane = loadStream.ReadLine().Split(',');
                if (dane[0].Equals("T"))
                {
                    if (dane[1].Equals("V"))
                    {
                        volleyball.addTeam(new Team(dane[2]));
                    }
                    else if (dane[1].Equals("T"))
                    {
                        tugOfWar.addTeam(new Team(dane[2]));
                    }
                }
                else if (dane[0].Equals("J"))
                {
                    if (dane[1].Equals("V"))
                    {
                        volleyball.addJudge(new Judge(dane[2], dane[3]));
                    }
                    else if (dane[1].Equals("T"))
                    {
                        tugOfWar.addJudge(new Judge(dane[2], dane[3]));
                    }
                }
            }
            loadStream.Close();
        }

        private void TeamButton_Clicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(teamPage);
        }

        private void JudgeButton_Clicked(object sender, RoutedEventArgs e)
        {
            
        }

        private void LoadButton_Clicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(loadPage);
        }

        private void VolleyballButton_Clicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(volleyballPage);
        }

        private void TugOfWarButton_Clicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(tugOfWarPage);
        }

        private void DodgeballButton_Clicked(object sender, RoutedEventArgs e)
        {

        }
        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy zapisać stan programu?", "Wyjście", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            { 
                //Ręcznie tworzę ścieżkę do folderu saved
                string[] folders = AppDomain.CurrentDomain.BaseDirectory.Split('\\');
                string folderPath = "";
                for(int i=0; i<folders.Length-4; i++)
                {
                    folderPath += $@"{folders[i]}\";
                }
                folderPath += "saved";

                //Tworzę okno do zapisywania plików i zapisuje do pliku txt o podanej nazwie
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.InitialDirectory = folderPath;
                saveFile.Filter = "txt files (*.txt)|*.txt";

                if (saveFile.ShowDialog() == false) return;
                StreamWriter saveStream = new StreamWriter(saveFile.FileName);
                volleyball.getTeams().ForEach(team =>
                {
                    saveStream.WriteLine($"T,V,{team.getName()}");
                });
                volleyball.getJudges().ForEach(judge =>
                {
                    saveStream.WriteLine($"J,V,{judge.getSurname()},{judge.getName()}");
                });
                tugOfWar.getTeams().ForEach(team =>
                {
                    saveStream.WriteLine($"T,T,{team.getName()}");
                });
                tugOfWar.getJudges().ForEach(judge =>
                {
                    saveStream.WriteLine($"J,T,{judge.getSurname()}, {judge.getName()}");
                });
                saveStream.Close();                   
            }
            mainWindow.Close();
        }
    }
}
