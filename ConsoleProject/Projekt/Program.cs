﻿using System;

namespace Projekt
{
    public class Projekt
    {
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
            while(!loadStream.EndOfStream)
            {
                string[] dane = loadStream.ReadLine().Split(',');
                if(dane[0].Equals("T"))
                {
                    if(dane[1].Equals("V"))
                    {
                        volleyball.addTeam(new Team(dane[2]));
                    }
                    else if(dane[1].Equals("T"))
                    {
                        tugOfWar.addTeam(new Team(dane[2]));
                    }
                }
                else if(dane[0].Equals("J"))
                {
                    if (dane[1].Equals("V"))
                    {
                        volleyball.addJudge(new Judge(dane[2], dane[3]));
                    }
                    else if(dane[1].Equals("T"))
                    {
                        tugOfWar.addJudge(new Judge(dane[2], dane[3]));
                    }
                }
            }
            loadStream.Close();
        }
        public static void save(string? fname, Volleyball volleyball, Tug_of_war tugOfWar)
        {
            //Najpierw sprawdzamy czy nazwa pliku jest dobra
            if (fname == null || fname.Length == 0)
            {
                Console.WriteLine("Niepoprawna nazwa pliku");
                return;
            }

            //Zapisujemy w kodzie T,[sport],[nazwa] dla drużyn i J,[sport],[imie],[nazwisko] dla sędziów
            StreamWriter saveStream = new StreamWriter($@"..\..\..\saved\{fname}.txt");
            volleyball.getTeams().ForEach(team =>
            {
                saveStream.WriteLine($"T,V,{team.getName()}");
            });
            volleyball.getJudges().ForEach(judge =>
            {
                saveStream.WriteLine($"J,V,{judge.getName()},{judge.getSurname()}");
            });
            tugOfWar.getTeams().ForEach(team =>
            {
                saveStream.WriteLine($"T,T.{team.getName()}");
            });
            tugOfWar.getJudges().ForEach(judge =>
            {
                saveStream.WriteLine($"J,T,{judge.getName()}, {judge.getSurname()}");
            });
            saveStream.Close();
        }
        public static void Main()
        {
            
            Volleyball volleyball = new Volleyball();
            Tug_of_war tugOfWar = new Tug_of_war();
            
            //Rozbudowany main, dodałem proste menu tekstowe, żeby łatwiej mi się testowało
            int wybor = -1;
            bool end = false;
            string? fname = "", sport = "";
            while(end == false)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("1. Wczytaj z pliku");
                Console.WriteLine("2. Zapisz do pliku");
                Console.WriteLine("3. Przegląd drużyn");
                Console.WriteLine("4. Zagraj");
                Console.WriteLine("0. Zakończ działanie programu");
                Console.WriteLine("------------------------------");
                wybor = Convert.ToInt16(Console.ReadLine());

                switch (wybor)
                {
                    case 1:
                        //Bierze nazwę pliku i wczytuje z niego dane
                        Console.Write("Nazwa pliku: ");
                        fname = Console.ReadLine();                        
                        load(fname, volleyball, tugOfWar);
                        break;
                    case 2:
                        //Bierze nazwę pliku i zapisuje dane do pliku pod tą nazwą
                        Console.Write("Nazwa pliku: ");
                        fname = Console.ReadLine();                        
                        save(fname, volleyball, tugOfWar);
                        break;
                    case 3:
                        //Wypisuje nazwy wszystkich drużyn które obecnie istnieją
                        Console.Write("Wybierz sport:");
                        sport = Console.ReadLine();
                        if(sport == "volleyball")
                        {
                            if (volleyball.getTeams().Count() == 0) Console.WriteLine("Brak drużyn");
                            else volleyball.getTeams().ForEach(team => { Console.WriteLine(team.getName()); });
                        }
                        else if(sport == "tug of war")
                        {
                            if (tugOfWar.getTeams().Count() == 0) Console.WriteLine("Brak drużyn");
                            else tugOfWar.getTeams().ForEach(team => { Console.WriteLine(team.getName()); });
                        }
                        break;
                    case 4:
                        //Gramy B)
                        Console.Write("Wybierz sport:");
                        sport = Console.ReadLine();
                        if (sport == "volleyball") volleyball.playElimination();
                        else if (sport == "tug of war") tugOfWar.playElimination();
                        break;
                    case 0:
                        //Kończy program
                        end = true;
                        break;
                    default:
                        Console.WriteLine("Zła komenda, spróbuj ponownie");
                        break;
                }
            }
            
            /*List<Team> teams = volleyball.getTeams();
            teams.ForEach(team => Console.WriteLine(team.getName() + " " + team.getScore().ToString()));*/
        }
    }
}
