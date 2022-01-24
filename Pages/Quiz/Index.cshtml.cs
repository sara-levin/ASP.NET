using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewStart.Models;

namespace NewStart.Pages.Quiz
{
    public class IndexModel : PageModel
    {
        private readonly NewStart.Data.NewStartDbContext database;

        public IndexModel(NewStart.Data.NewStartDbContext context)
        {
            database = context;
        }

        [FromQuery]
        public string SortColumn { get; set; }
        public IList<Unit> Units { get; set; }
        public IList<Mythology> Mythologies { get; set; }

        public List<AverageChosen> averageList = new List<AverageChosen>();

        public AverageChosen AverageVariable { get; set; }

        public class AverageChosen 
        {
            public int MythologyID;
            public int AverageValue;
            public string MythologyName;
            public string Commentary;
        }


        public void OnGet()
        {

            if (SortColumn == null)
            {
                AverageChosen ifNull = new AverageChosen
                {
                    MythologyName = "",
                    Commentary = ""
                };
                AverageVariable = ifNull;

            }
            else if (SortColumn == "Attack")
            {
                averageList.Clear();

                foreach (var myth in database.Mythologies)
                {
                    var units = database.Units.Where(u => u.MythologyID == myth.ID);
                    var totalAttack = units.Sum(u => u.Attack);
                    var unitCount = units.Count();
                    var averageAttack = totalAttack / unitCount;

                    AverageChosen average = new AverageChosen
                    {
                        MythologyID = myth.ID,
                        AverageValue = averageAttack,
                        MythologyName = myth.Name,
                        Commentary = "The Mythology with the higest average Attack is:"
                    };
                    averageList.Add(average);
                }

                AverageVariable = averageList.OrderByDescending(m => m.AverageValue).First();
            }
            else if (SortColumn == "HP")
            {
                averageList.Clear();

                foreach (var myth in database.Mythologies)
                {
                    var units = database.Units.Where(u => u.MythologyID == myth.ID);
                    var totalHP = units.Sum(u => u.Hitpoints);
                    var unitCount = units.Count();
                    var averageHP = totalHP / unitCount;

                    AverageChosen average = new AverageChosen
                    {
                        MythologyID = myth.ID,
                        AverageValue = averageHP,
                        MythologyName = myth.Name,
                        Commentary = "The Mythology with the higest average HP is:"
                    };
                    averageList.Add(average);
                }
               AverageVariable =  averageList.OrderByDescending(m => m.AverageValue).First();
            }
            else if (SortColumn == "Cost")
            {
                averageList.Clear();

                foreach (var myth in database.Mythologies)
                {
                    var units = database.Units.Where(u => u.MythologyID == myth.ID);
                    var totalCost = units.Sum(u => u.Cost);
                    var unitCount = units.Count();
                    var averageCost = totalCost / unitCount;

                    AverageChosen average = new AverageChosen
                    {
                        MythologyID = myth.ID,
                        AverageValue = averageCost,
                        MythologyName = myth.Name,
                        Commentary = "The Mythology with the lowest average Cost is:"
                    };
                    averageList.Add(average);
                }

                AverageVariable = averageList.OrderBy(m => m.AverageValue).First();
            }
            else
            {
                AverageChosen ifCoolest = new AverageChosen
                {
                    MythologyName = "Greek",
                    Commentary = "The Coolest Mythology (by far) is:"
                };
                AverageVariable = ifCoolest;
            }
        }
    }
}
