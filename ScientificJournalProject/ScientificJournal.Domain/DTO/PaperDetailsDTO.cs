using Microsoft.AspNetCore.Http;
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
        public List<String> Keywords { get; set; }
        public List<ScienceUser> Authors { get; set; }

        public Guid DocumentId { get; set; }
        public bool NoEdit { get; set; }
        public string PreviousAction { get; set; }
        public string ConferenceName { get; set; }

    }
}
