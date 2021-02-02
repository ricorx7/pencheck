using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PenCheck.Models
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SSN { get; set; }

        public string Email { get; set; }

        public int PlanId { get; set; }
    }
}
