using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PenCheck.Models;

namespace PenCheck.Data
{
    public class PlansContext : DbContext
    {
        public PlansContext (DbContextOptions<PlansContext> options)
            : base(options)
        {
        }

        public DbSet<PenCheck.Models.Plan> Plan { get; set; }
    }
}
