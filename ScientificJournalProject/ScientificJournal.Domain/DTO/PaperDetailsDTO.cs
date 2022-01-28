using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.DTO
{
    public class PaperDetailsDTO
    {
        public Paper Paper { get; set; }
        public String Keywords { get; set; }
        public List<ScienceUser> Authors { get; set; }
    }
}
