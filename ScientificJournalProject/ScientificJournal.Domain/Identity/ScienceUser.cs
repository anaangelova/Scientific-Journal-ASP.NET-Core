using Microsoft.AspNetCore.Identity;
using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.Identity
{
    public class ScienceUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Biography { get; set; }
        public bool isAdmin { get; set; }

        //M:N relacija ScienceUser:Paper
        public virtual ICollection<PapersUsers> PapersFromUser { get; set; }

    }
}
