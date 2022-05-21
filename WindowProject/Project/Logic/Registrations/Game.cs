﻿using System;
using System.Collections.Generic;

namespace Project.Logic.Registrations
{
    public class Game
    {
        private Team firstTeam, secondTeam, winner;
        private Random random = new Random();

        public Game(Team firstTeam, Team secondTeam)
        {
            this.firstTeam = firstTeam;
            this.secondTeam = secondTeam;
        }

        public Team getFirstTeam()
        {
            return firstTeam;
        }
        public Team getSecondTeam()
        {
            return secondTeam;
        }
        public Team getWinner()
        {
            return winner;
        }

        public Team playManual(int chosen)
        {
            if (chosen == 1) winner = firstTeam;
            else if (chosen == 2) winner = secondTeam;
            else throw new Exception("Wrong number");
            return winner;
        }

        public Team playRandom()
        {
            if (random.NextDouble() >= 0.5) winner = firstTeam;
            else winner = secondTeam;
            return winner;
        }
    }
}
