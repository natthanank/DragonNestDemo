using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DragonNest.Models
{
    public class DragonNestContext : DbContext
    {
        public DragonNestContext (DbContextOptions<DragonNestContext> options)
            : base(options)
        {
        }

        public DbSet<DNClass> DNClasses { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<DNClass>().ToTable("DNClass");
            model.Entity<Skill>().ToTable("Skill");
        }
    }
}
