using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NewStart.Data;
using NewStart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewStart.Pages
{
   

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NewStartDbContext database;

        public IndexModel(ILogger<IndexModel> logger, NewStartDbContext database)
        {
            _logger = logger;
            this.database = database;
        }





        public void OnGet()
        {
            

        }

        public void OnPost()
        {
            var myths = database.Mythologies.Any();
            if (myths == false)
            {
                //unit = await LoadSingleColumnCSV(@"Data\Units.csv");
                //god = await LoadSingleColumnCSV(@"Data\Gods.csv");
                //mythology = await LoadSingleColumnCSV(@"Data\Mythologies.csv");

                string[] mythologyLines = System.IO.File.ReadAllLines(@"Data\Mythologies.csv");
                foreach (string mline in mythologyLines)
                {
                    string[] mparts = mline.Split(';');
                    string mname = mparts[0];
                    string orgincountry = mparts[1];
                    string date = mparts[2];

                    Mythology mythology = new Mythology
                    {
                        Name = mname,
                        OrginCountry = orgincountry,
                        Date = date
                    };
                    database.Mythologies.Add(mythology);
                    database.SaveChanges();

                }

                string[] unitLines = System.IO.File.ReadAllLines(@"Data\Units.csv");
                foreach (string uline in unitLines)
                {
                    string[] uparts = uline.Split(';');
                    string uname = uparts[0];
                    string unitsclass = uparts[1];
                    string hitpointstring = uparts[2];
                    string attackstring = uparts[3];
                    string coststring = uparts[4];
                    string umythologystring = uparts[5];

                    int hitpoints = int.Parse(hitpointstring);
                    int attack = int.Parse(attackstring);
                    int cost = int.Parse(coststring);

                    var umythology = database.Mythologies.Single(m => m.Name == umythologystring);


                    Unit unit = new Unit
                    {
                        Name = uname,
                        Class = unitsclass,
                        Hitpoints = hitpoints,
                        Attack = attack,
                        Cost = cost,
                        Mythology = umythology,
                        MythologyID = umythology.ID
                    };
                    database.Units.Add(unit);
                    database.SaveChanges();
                }

                string[] godLines = System.IO.File.ReadAllLines(@"Data\Gods.csv");
                foreach (string gline in godLines)
                {
                    string[] gparts = gline.Split(';');
                    string gname = gparts[0];
                    string trivia = gparts[1];
                    string age = gparts[2];
                    string focus = gparts[3];
                    string gmythologystring = gparts[4];

                    var gmythology = database.Mythologies.Where(m => m.Name == gmythologystring).First();

                    God god = new God
                    {
                        Name = gname,
                        Trivia = trivia,
                        Age = age,
                        Focus = focus,
                        MythologyID = gmythology.ID
                    };
                    database.Gods.Add(god);
                    database.SaveChanges();
                }
            }
        }
    }
}
