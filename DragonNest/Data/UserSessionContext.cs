using DragonNest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonNest.Data
{
    public class UserSessionContext : DbContext
    {
        public UserSessionContext(DbContextOptions<UserSessionContext> options)
            : base(options)
        {
        }

        public DbSet<UserSession> UserSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<UserSession>().ToTable("UserSession");
            model.Entity<UserSession>()
            .HasKey(u => new { u.UserID, u.SessionID });
        }
    }
}
