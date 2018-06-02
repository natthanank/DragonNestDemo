using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DragonNest.Models
{
    public class DNClass
    {
        [Key]
        [Column("Name")]
        public string ClassName { get; set; }
        public MainATK ATKTYPE { get; set; }

        public void Speak()
        {
            Console.WriteLine("My Class is %s", this.ClassName);
        }
    }

    public enum MainATK
    {
        PHYSICAL, MAGICAL
    }
}
