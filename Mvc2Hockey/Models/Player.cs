using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc2Hockey.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int JerseyNumber { get; set; }
        public int Age { get; set; }

        public int NrOfCars { get; set; }
        public List<HistoryRecords> HistoryRecords { get; set; } = new List<HistoryRecords>();

        public Team Team { get; set; } 
    }


    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }



    public class EmailSubscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }


    public class HistoryRecords
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Scores { get; set; }
        public int Assists { get; set; }

        public int PenaltyMinutes { get; set; }
    }

}