using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ScientificJournal.Domain.DTO
{
    public class PaperDTO
    {
        public Paper Paper { get; set; }

        [Required(ErrorMessage = "Keywords are required")]
        public String Keywords { get; set; }
        public String AuthorFirst { get; set; }
        public String AuthorSecond { get; set; }
        public String AuthorThird { get; set; }
        public String ConferenceName { get; set; }
        public Guid ConferenceId { get; set; }
        public List<Conference> Conferences { get; set; }
    }
}
