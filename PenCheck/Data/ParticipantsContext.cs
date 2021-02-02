using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PenCheck.Models;

namespace PenCheck.Data
{
    public class ParticipantsContext : DbContext
    {
        public ParticipantsContext (DbContextOptions<ParticipantsContext> options)
            : base(options)
        {
        }

        public DbSet<PenCheck.Models.Participant> Participant { get; set; }
    }
}
