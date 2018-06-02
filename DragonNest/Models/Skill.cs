using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DragonNest.Models
{
    public class Skill
    {
        public int ID { get; set; }
        public string DNClassID { get; set; }
        public string Name { get; set; }
        public int DMG { get; set; }
        public string Special { get; set; }
        public DNClass DNClass { get; set; }
    }
}
