using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenCheck.Data
{
    public class PencheckContext : DbContext
    {

        public PencheckContext(DbContextOptions<PencheckContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Create a SQLite file locally
            optionsBuilder.UseSqlite(@"DataSource = Pencheck.db;");
        }

        // Participant database connection
        public DbSet<PenCheck.Models.Participant> Participant { get; set; }

        // Plan database connection
        public DbSet<PenCheck.Models.Plan> Plan { get; set; }

    }
}
