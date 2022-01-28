using ScientificJournal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.DomainModels
{
    public class PapersUsers
    {
        public Guid PaperId { get; set; }
        public Paper Paper { get; set; }
        public String ScienceUserId { get; set; }
        public ScienceUser ScienceUser { get; set; }
        
       
    }
}
