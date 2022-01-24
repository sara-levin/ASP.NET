using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStart.Models
{
    public class Mythology
    {
        public List<Unit> Units { get; set; }
        public List<God> Gods { get; set; }
        public int ID { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
  //      [Required, Column(TypeName = "nvarchar(255)"), DefaultValue("xlocation")]
        public string OrginCountry { get; set; }
  //      [Required, Column(TypeName = "nvarchar(255)"), DefaultValue("xdate")]
        public string Date { get; set; }
    }
}
