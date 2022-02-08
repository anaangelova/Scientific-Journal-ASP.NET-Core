using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.DTO
{
    public class PapersConferencesDTO
    {
        public List<Paper> Papers { get; set; }
        public List<Conference> Conferences { get; set; }
    }
}
